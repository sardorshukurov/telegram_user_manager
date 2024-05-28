using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TUM.Domain.Entities;
using TUM.Infrastructure.Data;

namespace TUM.Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class, IBaseEntity
{
    private readonly MainDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(MainDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    
    public async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public async Task UpdateAsync(Guid id, T entity)
    {
        var existingEntity = await _dbSet.FindAsync(id);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
    }

    public async Task<T?> GetOneAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return await query.AsNoTracking().FirstOrDefaultAsync(filter);
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return await query.AsNoTracking().Where(filter).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}