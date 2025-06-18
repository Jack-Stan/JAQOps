namespace JAQOps.API.Models.Tenants;

public class TenantDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string CompanyName { get; set; }
    public required string VatNumber { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public string? Website { get; set; }
    public string? LogoUrl { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public bool IsActive { get; set; }
    public DateTime SubscriptionStartDate { get; set; }
    public DateTime? SubscriptionEndDate { get; set; }
}

public class CreateTenantRequest
{
    public required string Name { get; set; }
    public required string CompanyName { get; set; }
    public required string VatNumber { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public string? Website { get; set; }
    public string? LogoUrl { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public DateTime? SubscriptionEndDate { get; set; }
}

public class UpdateTenantRequest : CreateTenantRequest
{
    public bool IsActive { get; set; }
}
