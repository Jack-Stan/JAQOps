using JAQOps.Data.Entities;
using JAQOps.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JAQOps.Data.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly JAQOpsDbContext _context;
    protected readonly DbSet<T> _entities;

    public Repository(JAQOpsDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _entities
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities
            .AsNoTracking()
            .Where(e => !e.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate)
    {
        return await Task.FromResult(
            _entities
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .Where(predicate)
                .ToList()
        );
    }

    public async Task AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await _entities.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Entry(entity).State = EntityState.Modified;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _entities.FindAsync(id);
        if (entity != null)
        {
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
