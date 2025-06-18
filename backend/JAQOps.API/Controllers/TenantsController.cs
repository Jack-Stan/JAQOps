using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JAQOps.Business.Interfaces;
using JAQOps.Business.Models;
using JAQOps.API.Models.Tenants;

namespace JAQOps.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class TenantsController : ControllerBase
{
    private readonly ITenantService _tenantService;
    
    public TenantsController(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tenants = await _tenantService.GetAllTenantsAsync();
        var result = tenants.Select(t => new TenantDto
        {
            Id = t.Id,
            Name = t.Name,
            CompanyName = t.CompanyName,
            VatNumber = t.VatNumber,
            Address = t.Address,
            City = t.City,
            PostalCode = t.PostalCode,
            Country = t.Country,
            PhoneNumber = t.PhoneNumber,
            Email = t.Email,
            Website = t.Website,
            LogoUrl = t.LogoUrl,
            PrimaryColor = t.PrimaryColor,
            SecondaryColor = t.SecondaryColor,
            IsActive = t.IsActive,
            SubscriptionStartDate = t.SubscriptionStartDate,
            SubscriptionEndDate = t.SubscriptionEndDate
        });
            
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tenant = await _tenantService.GetTenantByIdAsync(id);
            
        if (tenant == null)
        {
            return NotFound();
        }
        
        return Ok(new TenantDto
        {
            Id = tenant.Id,
            Name = tenant.Name,
            CompanyName = tenant.CompanyName,
            VatNumber = tenant.VatNumber,
            Address = tenant.Address,
            City = tenant.City,
            PostalCode = tenant.PostalCode,
            Country = tenant.Country,
            PhoneNumber = tenant.PhoneNumber,
            Email = tenant.Email,
            Website = tenant.Website,
            LogoUrl = tenant.LogoUrl,
            PrimaryColor = tenant.PrimaryColor,
            SecondaryColor = tenant.SecondaryColor,
            IsActive = tenant.IsActive,
            SubscriptionStartDate = tenant.SubscriptionStartDate,
            SubscriptionEndDate = tenant.SubscriptionEndDate
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateTenantRequest request)
    {
        var model = new TenantModel
        {
            Name = request.Name,
            CompanyName = request.CompanyName,
            VatNumber = request.VatNumber,
            Address = request.Address,
            City = request.City,
            PostalCode = request.PostalCode,
            Country = request.Country,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Website = request.Website,
            LogoUrl = request.LogoUrl,
            PrimaryColor = request.PrimaryColor,
            SecondaryColor = request.SecondaryColor,
            SubscriptionStartDate = DateTime.UtcNow,
            SubscriptionEndDate = request.SubscriptionEndDate
        };
        
        var tenant = await _tenantService.CreateTenantAsync(model);
        
        return CreatedAtAction(nameof(GetById), new { id = tenant.Id }, new { tenant.Id });
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateTenantRequest request)
    {
        var model = new TenantModel
        {
            Id = id,
            Name = request.Name,
            CompanyName = request.CompanyName,
            VatNumber = request.VatNumber,
            Address = request.Address,
            City = request.City,
            PostalCode = request.PostalCode,
            Country = request.Country,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Website = request.Website,
            LogoUrl = request.LogoUrl,
            PrimaryColor = request.PrimaryColor,
            SecondaryColor = request.SecondaryColor,
            IsActive = request.IsActive,
            SubscriptionEndDate = request.SubscriptionEndDate
        };
        
        var tenant = await _tenantService.UpdateTenantAsync(id, model);
        
        if (tenant == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _tenantService.DeleteTenantAsync(id);
        
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}
