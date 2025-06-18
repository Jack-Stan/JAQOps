using JAQOps.Business.Interfaces;
using JAQOps.Business.Models;
using JAQOps.Data.Entities;
using JAQOps.Data.Repositories.Interfaces;

namespace JAQOps.Business.Services;

public class TenantService : ITenantService
{
    private readonly ITenantRepository _tenantRepository;

    public TenantService(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<IEnumerable<TenantModel>> GetAllTenantsAsync()
    {
        var tenants = await _tenantRepository.GetAllAsync();
        return tenants.Select(MapToModel);
    }

    public async Task<TenantModel?> GetTenantByIdAsync(Guid id)
    {
        var tenant = await _tenantRepository.GetByIdAsync(id);
        return tenant != null ? MapToModel(tenant) : null;
    }

    public async Task<TenantModel> CreateTenantAsync(TenantModel model)
    {
        var entity = MapToEntity(model);
        entity.Id = Guid.NewGuid();
        
        await _tenantRepository.AddAsync(entity);
        await _tenantRepository.SaveChangesAsync();
        
        return MapToModel(entity);
    }

    public async Task<TenantModel?> UpdateTenantAsync(Guid id, TenantModel model)
    {
        var entity = await _tenantRepository.GetByIdAsync(id);
        if (entity == null)
            return null;
        
        // Update properties
        entity.Name = model.Name;
        entity.CompanyName = model.CompanyName;
        entity.VatNumber = model.VatNumber;
        entity.Address = model.Address;
        entity.City = model.City;
        entity.PostalCode = model.PostalCode;
        entity.Country = model.Country;
        entity.PhoneNumber = model.PhoneNumber;
        entity.Email = model.Email;
        entity.Website = model.Website;
        entity.LogoUrl = model.LogoUrl;
        entity.PrimaryColor = model.PrimaryColor;
        entity.SecondaryColor = model.SecondaryColor;
        entity.IsActive = model.IsActive;
        entity.SubscriptionEndDate = model.SubscriptionEndDate;
        
        await _tenantRepository.UpdateAsync(entity);
        await _tenantRepository.SaveChangesAsync();
        
        return MapToModel(entity);
    }

    public async Task<bool> DeleteTenantAsync(Guid id)
    {
        var entity = await _tenantRepository.GetByIdAsync(id);
        if (entity == null)
            return false;
        
        await _tenantRepository.DeleteAsync(id);
        await _tenantRepository.SaveChangesAsync();
        
        return true;
    }
    
    #region Helpers
    
    private static TenantModel MapToModel(Tenant entity)
    {
        return new TenantModel
        {
            Id = entity.Id,
            Name = entity.Name,
            CompanyName = entity.CompanyName,
            VatNumber = entity.VatNumber,
            Address = entity.Address,
            City = entity.City,
            PostalCode = entity.PostalCode,
            Country = entity.Country,
            PhoneNumber = entity.PhoneNumber,
            Email = entity.Email,
            Website = entity.Website,
            LogoUrl = entity.LogoUrl,
            PrimaryColor = entity.PrimaryColor,
            SecondaryColor = entity.SecondaryColor,
            IsActive = entity.IsActive,
            SubscriptionStartDate = entity.SubscriptionStartDate,
            SubscriptionEndDate = entity.SubscriptionEndDate
        };
    }
    
    private static Tenant MapToEntity(TenantModel model)
    {
        return new Tenant
        {
            Id = model.Id,
            Name = model.Name,
            CompanyName = model.CompanyName,
            VatNumber = model.VatNumber,
            Address = model.Address,
            City = model.City,
            PostalCode = model.PostalCode,
            Country = model.Country,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email,
            Website = model.Website,
            LogoUrl = model.LogoUrl,
            PrimaryColor = model.PrimaryColor,
            SecondaryColor = model.SecondaryColor,
            IsActive = model.IsActive,
            SubscriptionStartDate = model.SubscriptionStartDate,
            SubscriptionEndDate = model.SubscriptionEndDate
        };
    }
    
    #endregion
}
