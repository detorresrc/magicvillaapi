using System.Linq.Expressions;
using MagicVilla.Data;
using MagicVilla.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    internal readonly DbSet<T> dbSet;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        this.dbSet = _context.Set<T>();
    }
    
    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
    {
        IQueryable<T> query = dbSet;
        if(!tracked) query = query.AsNoTracking();
        if (filter != null) query = query.Where(filter);
        
        return await query.FirstOrDefaultAsync();
    }

    public async Task<bool> CreateAsync(T entity)
    {
        dbSet.Add(entity);
        return (await _context.SaveChangesAsync() > 0);
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        dbSet.Remove(entity);
        return (await _context.SaveChangesAsync() > 0);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        dbSet.Update(entity);
        return (await _context.SaveChangesAsync() > 0);
    }
}