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
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

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
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

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
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

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
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

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
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(e => e.DatosAnteriores)
                .HasColumnType("json");

            entity.Property(e => e.DatosNuevos)
                .HasColumnType("json");
        });
        
        // ===== USUARIOS =====
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                Id = 1,
                Nombre = "Admin Principal",
                Email = "admin@autotaller.com",
                PasswordHash = "$2a$11$6p3L0hKfxJ8vQ9xMzN3YPO3L8hKfxJ8vQ9xMzN3", // Password: Admin123
                Rol = RolUsuario.Admin,
                Activo = true,
                FechaCreacion = new DateTime(2024, 1, 1)
            },
            new Usuario
            {
                Id = 2,
                Nombre = "Carlos Méndez",
                Email = "mecanico1@autotaller.com",
                PasswordHash = "$2a$11$6p3L0hKfxJ8vQ9xMzN3YPO3L8hKfxJ8vQ9xMzN3", // Password: Mec123
                Rol = RolUsuario.Mecanico,
                Activo = true,
                FechaCreacion = new DateTime(2024, 1, 15)
            },
            new Usuario
            {
                Id = 3,
                Nombre = "Laura Gómez",
                Email = "mecanico2@autotaller.com",
                PasswordHash = "$2a$11$6p3L0hKfxJ8vQ9xMzN3YPO3L8hKfxJ8vQ9xMzN3", // Password: Mec123
                Rol = RolUsuario.Mecanico,
                Activo = true,
                FechaCreacion = new DateTime(2024, 2, 1)
            },
            new Usuario
            {
                Id = 4,
                Nombre = "Ana Torres",
                Email = "recepcion1@autotaller.com",
                PasswordHash = "$2a$11$6p3L0hKfxJ8vQ9xMzN3YPO3L8hKfxJ8vQ9xMzN3", // Password: Rec123
                Rol = RolUsuario.Recepcionista,
                Activo = true,
                FechaCreacion = new DateTime(2024, 1, 20)
            },
            new Usuario
            {
                Id = 5,
                Nombre = "Pedro Ramírez",
                Email = "recepcion2@autotaller.com",
                PasswordHash = "$2a$11$6p3L0hKfxJ8vQ9xMzN3YPO3L8hKfxJ8vQ9xMzN3", // Password: Rec123
                Rol = RolUsuario.Recepcionista,
                Activo = true,
                FechaCreacion = new DateTime(2024, 2, 10)
            }
        );

        // ===== CLIENTES =====
        modelBuilder.Entity<Cliente>().HasData(
            new Cliente { Id = 1, Nombre = "Juan Pérez", Email = "juan.perez@email.com", Telefono = "3001234567", FechaRegistro = new DateTime(2024, 3, 1) },
            new Cliente { Id = 2, Nombre = "María López", Email = "maria.lopez@email.com", Telefono = "3009876543", FechaRegistro = new DateTime(2024, 3, 5) },
            new Cliente { Id = 3, Nombre = "Carlos Rodríguez", Email = "carlos.rodriguez@email.com", Telefono = "3005551234", FechaRegistro = new DateTime(2024, 3, 10) },
            new Cliente { Id = 4, Nombre = "Ana Martínez", Email = "ana.martinez@email.com", Telefono = "3007778888", FechaRegistro = new DateTime(2024, 3, 15) },
            new Cliente { Id = 5, Nombre = "Luis Hernández", Email = "luis.hernandez@email.com", Telefono = "3003334444", FechaRegistro = new DateTime(2024, 3, 20) },
            new Cliente { Id = 6, Nombre = "Carmen Silva", Email = "carmen.silva@email.com", Telefono = "3006665555", FechaRegistro = new DateTime(2024, 4, 1) },
            new Cliente { Id = 7, Nombre = "Roberto Díaz", Email = "roberto.diaz@email.com", Telefono = "3002223333", FechaRegistro = new DateTime(2024, 4, 5) },
            new Cliente { Id = 8, Nombre = "Patricia Ruiz", Email = "patricia.ruiz@email.com", Telefono = "3008889999", FechaRegistro = new DateTime(2024, 4, 10) },
            new Cliente { Id = 9, Nombre = "Jorge Castro", Email = "jorge.castro@email.com", Telefono = "3004445555", FechaRegistro = new DateTime(2024, 4, 15) },
            new Cliente { Id = 10, Nombre = "Sofía Morales", Email = "sofia.morales@email.com", Telefono = "3001112222", FechaRegistro = new DateTime(2024, 4, 20) }
        );

        // ===== VEHÍCULOS =====
        modelBuilder.Entity<Vehiculo>().HasData(
            new Vehiculo { Id = 1, ClienteId = 1, Marca = "Toyota", Modelo = "Corolla", Anio = 2020, VIN = "1HGBH41JXMN109186", Kilometraje = 45000 },
            new Vehiculo { Id = 2, ClienteId = 1, Marca = "Honda", Modelo = "Civic", Anio = 2019, VIN = "2HGFC2F59HH123456", Kilometraje = 52000 },
            new Vehiculo { Id = 3, ClienteId = 2, Marca = "Chevrolet", Modelo = "Spark", Anio = 2021, VIN = "3GCPKSE78HG234567", Kilometraje = 30000 },
            new Vehiculo { Id = 4, ClienteId = 3, Marca = "Mazda", Modelo = "3", Anio = 2020, VIN = "4M2CU87198J345678", Kilometraje = 40000 },
            new Vehiculo { Id = 5, ClienteId = 3, Marca = "Nissan", Modelo = "Sentra", Anio = 2018, VIN = "5N1DR2MM8FC456789", Kilometraje = 75000 },
            new Vehiculo { Id = 6, ClienteId = 4, Marca = "Hyundai", Modelo = "Accent", Anio = 2022, VIN = "6KMHM81BXMU567890", Kilometraje = 15000 },
            new Vehiculo { Id = 7, ClienteId = 5, Marca = "Kia", Modelo = "Rio", Anio = 2021, VIN = "7KNDJ23C08K678901", Kilometraje = 25000 },
            new Vehiculo { Id = 8, ClienteId = 6, Marca = "Renault", Modelo = "Logan", Anio = 2019, VIN = "8LRBG0RB0KN789012", Kilometraje = 60000 },
            new Vehiculo { Id = 9, ClienteId = 7, Marca = "Volkswagen", Modelo = "Gol", Anio = 2020, VIN = "9WVWZZZ1KZW890123", Kilometraje = 48000 },
            new Vehiculo { Id = 10, ClienteId = 8, Marca = "Ford", Modelo = "Fiesta", Anio = 2021, VIN = "1FADP3K28JL901234", Kilometraje = 32000 },
            new Vehiculo { Id = 11, ClienteId = 9, Marca = "Suzuki", Modelo = "Swift", Anio = 2020, VIN = "2SUZUKI96MN012345", Kilometraje = 38000 },
            new Vehiculo { Id = 12, ClienteId = 10, Marca = "Mitsubishi", Modelo = "Mirage", Anio = 2019, VIN = "3MIAGE92LK123456", Kilometraje = 55000 }
        );

        // ===== REPUESTOS =====
        modelBuilder.Entity<Repuesto>().HasData(
            new Repuesto { Id = 1, Codigo = "FIL-001", Descripcion = "Filtro de Aceite", Stock = 50, PrecioUnitario = 15000m, StockMinimo = 10 },
            new Repuesto { Id = 2, Codigo = "FIL-002", Descripcion = "Filtro de Aire", Stock = 40, PrecioUnitario = 25000m, StockMinimo = 8 },
            new Repuesto { Id = 3, Codigo = "FIL-003", Descripcion = "Filtro de Combustible", Stock = 35, PrecioUnitario = 30000m, StockMinimo = 7 },
            new Repuesto { Id = 4, Codigo = "BUJ-001", Descripcion = "Bujías (set 4)", Stock = 30, PrecioUnitario = 45000m, StockMinimo = 6 },
            new Repuesto { Id = 5, Codigo = "PAST-001", Descripcion = "Pastillas de Freno Delanteras", Stock = 25, PrecioUnitario = 80000m, StockMinimo = 5 },
            new Repuesto { Id = 6, Codigo = "PAST-002", Descripcion = "Pastillas de Freno Traseras", Stock = 25, PrecioUnitario = 70000m, StockMinimo = 5 },
            new Repuesto { Id = 7, Codigo = "DISCO-001", Descripcion = "Discos de Freno Delanteros (par)", Stock = 20, PrecioUnitario = 150000m, StockMinimo = 4 },
            new Repuesto { Id = 8, Codigo = "ACEIT-001", Descripcion = "Aceite Sintético 5W30 (garrafa)", Stock = 60, PrecioUnitario = 85000m, StockMinimo = 15 },
            new Repuesto { Id = 9, Codigo = "ACEIT-002", Descripcion = "Aceite Mineral 20W50 (garrafa)", Stock = 45, PrecioUnitario = 55000m, StockMinimo = 10 },
            new Repuesto { Id = 10, Codigo = "BANDA-001", Descripcion = "Banda de Distribución", Stock = 18, PrecioUnitario = 120000m, StockMinimo = 4 },
            new Repuesto { Id = 11, Codigo = "CORREA-001", Descripcion = "Correa de Accesorios", Stock = 22, PrecioUnitario = 35000m, StockMinimo = 5 },
            new Repuesto { Id = 12, Codigo = "BAT-001", Descripcion = "Batería 12V 45Ah", Stock = 15, PrecioUnitario = 280000m, StockMinimo = 3 },
            new Repuesto { Id = 13, Codigo = "LIQ-001", Descripcion = "Líquido Refrigerante (1L)", Stock = 50, PrecioUnitario = 18000m, StockMinimo = 10 },
            new Repuesto { Id = 14, Codigo = "LIQ-002", Descripcion = "Líquido de Frenos (500ml)", Stock = 40, PrecioUnitario = 22000m, StockMinimo = 8 },
            new Repuesto { Id = 15, Codigo = "LLANTA-001", Descripcion = "Llanta 185/65 R15", Stock = 16, PrecioUnitario = 250000m, StockMinimo = 4 },
            new Repuesto { Id = 16, Codigo = "AMOR-001", Descripcion = "Amortiguador Delantero", Stock = 12, PrecioUnitario = 180000m, StockMinimo = 3 },
            new Repuesto { Id = 17, Codigo = "AMOR-002", Descripcion = "Amortiguador Trasero", Stock = 12, PrecioUnitario = 160000m, StockMinimo = 3 },
            new Repuesto { Id = 18, Codigo = "TERM-001", Descripcion = "Termostato", Stock = 20, PrecioUnitario = 45000m, StockMinimo = 5 },
            new Repuesto { Id = 19, Codigo = "BOMBA-001", Descripcion = "Bomba de Agua", Stock = 10, PrecioUnitario = 95000m, StockMinimo = 2 },
            new Repuesto { Id = 20, Codigo = "EMBRAGUE-001", Descripcion = "Kit de Embrague", Stock = 8, PrecioUnitario = 350000m, StockMinimo = 2 }
        );
    }
}