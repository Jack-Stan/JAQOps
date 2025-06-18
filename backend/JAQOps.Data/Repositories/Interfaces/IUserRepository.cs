using JAQOps.Data.Entities;

namespace JAQOps.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetUsersByTenantIdAsync(Guid tenantId);
    Task<IEnumerable<string>> GetUserRolesAsync(Guid userId);
}
