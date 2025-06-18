using JAQOps.Data.Entities;
using JAQOps.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JAQOps.Data.Repositories;

public class TenantRepository : Repository<Tenant>, ITenantRepository
{
    public TenantRepository(JAQOpsDbContext context) : base(context)
    {
    }

    public async Task<Tenant?> GetByNameAsync(string name)
    {
        return await _entities
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Name == name && !t.IsDeleted);
    }

    public async Task<IEnumerable<Tenant>> GetActiveTenantsAsync()
    {
        return await _entities
            .AsNoTracking()
            .Where(t => t.IsActive && !t.IsDeleted)
            .ToListAsync();
    }
}
