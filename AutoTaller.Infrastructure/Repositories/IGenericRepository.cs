using System.Linq.Expressions;

namespace AutoTaller.Infrastructure.Repositories;

public interface IGenericRepository<T> where T : class
{
    // Obtener todos los registros
    Task<IEnumerable<T>> GetAllAsync();
    
    // Obtener por ID
    Task<T?> GetByIdAsync(int id);
    
    // Buscar con filtro
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    
    // Obtener con paginación
    Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
        int pageNumber, 
        int pageSize, 
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null
    );
    
    // Agregar
    Task<T> AddAsync(T entity);
    
    // Agregar múltiples
    Task AddRangeAsync(IEnumerable<T> entities);
    
    // Actualizar
    void Update(T entity);
    
    // Eliminar
    void Delete(T entity);
    
    // Eliminar múltiples
    void DeleteRange(IEnumerable<T> entities);
    
    // Contar registros
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    
    // Verificar si existe
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}