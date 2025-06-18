namespace JAQOps.Data.Entities;

public class Contact : TenantEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MobileNumber { get; set; }
    public string? JobTitle { get; set; }
    public string? Notes { get; set; }
    public bool IsPrimary { get; set; }
    
    // Customer relationship
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    // Calculated property (not mapped to DB)
    public string FullName => $"{FirstName} {LastName}";
}
