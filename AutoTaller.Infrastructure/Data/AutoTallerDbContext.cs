using AutoTaller.Domain.Entities;
using AutoTaller.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoTaller.Infrastructure.Data;

public class AutoTallerDbContext : DbContext
{
    public AutoTallerDbContext(DbContextOptions<AutoTallerDbContext> options) 
        : base(options)
    {
    }

    // DbSets
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Repuesto> Repuestos { get; set; }
    public DbSet<OrdenServicio> OrdenesServicio { get; set; }
    public DbSet<DetalleOrden> DetallesOrden { get; set; }
    public DbSet<Factura> Facturas { get; set; }
    public DbSet<Auditoria> Auditorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ========== CLIENTE ==========
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Clientes");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.HasIndex(e => e.Email)
                .IsUnique();
            
            entity.Property(e => e.Telefono)
                .HasMaxLength(20);
            
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relación uno a muchos con Vehículos
            entity.HasMany(e => e.Vehiculos)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); // No eliminar cliente si tiene vehículos
        });

        // ========== VEHICULO ==========
        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.ToTable("Vehiculos");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Marca)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Modelo)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.VIN)
                .IsRequired()
                .HasMaxLength(17); // VIN estándar tiene 17 caracteres
            
            entity.HasIndex(e => e.VIN)
                .IsUnique();
            
            entity.Property(e => e.Anio)
                .IsRequired();
            
            entity.Property(e => e.Kilometraje)
                .HasDefaultValue(0);
        });

        // ========== USUARIO ==========
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.HasIndex(e => e.Email)
                .IsUnique();
            
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);
            
            entity.Property(e => e.Rol)
                .IsRequired()
                .HasConversion<int>(); // Guardar enum como int
            
            entity.Property(e => e.Activo)
                .HasDefaultValue(true);
            
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relación uno a muchos con OrdenesServicio (como mecánico)
            entity.HasMany(e => e.OrdenesAsignadas)
                .WithOne(o => o.Mecanico)
                .HasForeignKey(o => o.MecanicoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ========== REPUESTO ==========
        modelBuilder.Entity<Repuesto>(entity =>
        {
            entity.ToTable("Repuestos");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.HasIndex(e => e.Codigo)
                .IsUnique();
            
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.Stock)
                .IsRequired()
                .HasDefaultValue(0);
            
            entity.Property(e => e.PrecioUnitario)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
            
            entity.Property(e => e.StockMinimo)
                .HasDefaultValue(5);
        });

         // ========== ORDEN SERVICIO ==========
        modelBuilder.Entity<OrdenServicio>(entity =>
        {
            entity.ToTable("OrdenesServicio");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.TipoServicio)
                .IsRequired()
                .HasConversion<int>();
            
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(EstadoOrden.Pendiente);
            
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.Property(e => e.FechaEstimada)
                .IsRequired();
            
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500);
            
            entity.Property(e => e.ManoObra)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);

            // Relación con Vehículo
            entity.HasOne(e => e.Vehiculo)
                .WithMany(v => v.OrdenesServicio)
                .HasForeignKey(e => e.VehiculoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ========== DETALLE ORDEN ==========
        modelBuilder.Entity<DetalleOrden>(entity =>
        {
            entity.ToTable("DetallesOrden");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Cantidad)
                .IsRequired();
            
            entity.Property(e => e.PrecioUnitario)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
            
            // Relación con OrdenServicio
            entity.HasOne(e => e.OrdenServicio)
                .WithMany(o => o.DetallesOrden)
                .HasForeignKey(e => e.OrdenServicioId)
                .OnDelete(DeleteBehavior.Cascade); // Si se elimina orden, eliminar detalles
            
            // Relación con Repuesto
            entity.HasOne(e => e.Repuesto)
                .WithMany(r => r.DetallesOrden)
                .HasForeignKey(e => e.RepuestoId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Ignorar propiedad calculada
            entity.Ignore(e => e.Subtotal);
        });

        // ========== FACTURA ==========
        modelBuilder.Entity<Factura>(entity =>
        {
            entity.ToTable("Facturas");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.NumeroFactura)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.HasIndex(e => e.NumeroFactura)
                .IsUnique();
            
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.Property(e => e.SubtotalRepuestos)
                .HasColumnType("decimal(10,2)");
            
            entity.Property(e => e.ManoObra)
                .HasColumnType("decimal(10,2)");
            
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10,2)");

            // Relación uno a uno con OrdenServicio
            entity.HasOne(e => e.OrdenServicio)
                .WithOne(o => o.Factura)
                .HasForeignKey<Factura>(e => e.OrdenServicioId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ========== AUDITORIA ==========
        modelBuilder.Entity<Auditoria>(entity =>
        {
            entity.ToTable("Auditorias");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Entidad)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Accion)
                .IsRequired()
                .HasMaxLength(20);
            
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.Property(e => e.DatosAnteriores)
                .HasColumnType("json");
            
            entity.Property(e => e.DatosNuevos)
                .HasColumnType("json");
        });
    }
}