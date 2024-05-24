using System.Linq.Expressions;

namespace TUM.Infrastructure.Repository;

public interface IRepository<T>
{
    Task CreateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Guid id, T entity);
    Task<T?> GetOneAsync(Expression<Func<T, bool>> filter);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter);
}