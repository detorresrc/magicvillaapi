using System.Linq.Expressions;

namespace MagicVilla.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked=true);
    Task<bool> CreateAsync(T entity);
    Task<bool> RemoveAsync(T entity);
    Task<bool> UpdateAsync(T entity);
}