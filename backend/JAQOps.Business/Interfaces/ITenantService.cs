using JAQOps.Business.Models;

namespace JAQOps.Business.Interfaces;

public interface ITenantService
{
    Task<IEnumerable<TenantModel>> GetAllTenantsAsync();
    Task<TenantModel?> GetTenantByIdAsync(Guid id);
    Task<TenantModel> CreateTenantAsync(TenantModel tenant);
    Task<TenantModel?> UpdateTenantAsync(Guid id, TenantModel tenant);
    Task<bool> DeleteTenantAsync(Guid id);
}
