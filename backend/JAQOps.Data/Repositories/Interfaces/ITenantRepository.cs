using JAQOps.Data.Entities;

namespace JAQOps.Data.Repositories.Interfaces;

public interface ITenantRepository : IRepository<Tenant>
{
    Task<Tenant?> GetByNameAsync(string name);
    Task<IEnumerable<Tenant>> GetActiveTenantsAsync();
}
