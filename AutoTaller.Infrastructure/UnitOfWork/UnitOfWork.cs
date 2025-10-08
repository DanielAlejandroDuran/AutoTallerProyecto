using AutoTaller.Domain.Entities;
using AutoTaller.Infrastructure.Data;
using AutoTaller.Infrastructure.Repositories;

namespace AutoTaller.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AutoTallerDbContext _context;
    
    // Repositories privados (lazy loading)
    private IGenericRepository<Cliente>? _clientes;
    private IGenericRepository<Vehiculo>? _vehiculos;
    private IGenericRepository<Usuario>? _usuarios;
    private IGenericRepository<Repuesto>? _repuestos;
    private IGenericRepository<OrdenServicio>? _ordenesServicio;
    private IGenericRepository<DetalleOrden>? _detallesOrden;
    private IGenericRepository<Factura>? _facturas;
    private IGenericRepository<Auditoria>? _auditorias;

    public UnitOfWork(AutoTallerDbContext context)
    {
        _context = context;
    }

    // Propiedades públicas (lazy initialization)
    public IGenericRepository<Cliente> Clientes 
        => _clientes ??= new GenericRepository<Cliente>(_context);

    public IGenericRepository<Vehiculo> Vehiculos 
        => _vehiculos ??= new GenericRepository<Vehiculo>(_context);

    public IGenericRepository<Usuario> Usuarios 
        => _usuarios ??= new GenericRepository<Usuario>(_context);

    public IGenericRepository<Repuesto> Repuestos 
        => _repuestos ??= new GenericRepository<Repuesto>(_context);

    public IGenericRepository<OrdenServicio> OrdenesServicio 
        => _ordenesServicio ??= new GenericRepository<OrdenServicio>(_context);

    public IGenericRepository<DetalleOrden> DetallesOrden 
        => _detallesOrden ??= new GenericRepository<DetalleOrden>(_context);

    public IGenericRepository<Factura> Facturas 
        => _facturas ??= new GenericRepository<Factura>(_context);

    public IGenericRepository<Auditoria> Auditorias 
        => _auditorias ??= new GenericRepository<Auditoria>(_context);

    // Guardar todos los cambios en una transacción
    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    // Deshacer cambios no guardados
    public void Rollback()
    {
        _context.ChangeTracker.Clear();
    }

    // Dispose pattern
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}