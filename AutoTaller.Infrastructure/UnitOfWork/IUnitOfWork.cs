using AutoTaller.Domain.Entities;
using AutoTaller.Infrastructure.Repositories;

namespace AutoTaller.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    // Repositorios para cada entidad
    IGenericRepository<Cliente> Clientes { get; }
    IGenericRepository<Vehiculo> Vehiculos { get; }
    IGenericRepository<Usuario> Usuarios { get; }
    IGenericRepository<Repuesto> Repuestos { get; }
    IGenericRepository<OrdenServicio> OrdenesServicio { get; }
    IGenericRepository<DetalleOrden> DetallesOrden { get; }
    IGenericRepository<Factura> Facturas { get; }
    IGenericRepository<Auditoria> Auditorias { get; }

    // Guardar cambios (transacción atómica)
    Task<int> CommitAsync();
    
    // Deshacer cambios
    void Rollback();
}