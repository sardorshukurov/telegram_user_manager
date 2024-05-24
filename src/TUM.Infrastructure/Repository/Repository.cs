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
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(Guid id, T entity)
    {
        var existingEntity = await _dbSet.FindAsync(id);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<T?> GetOneAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(filter);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.AsNoTracking().Where(filter).ToListAsync();
    }
}