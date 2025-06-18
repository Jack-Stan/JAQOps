using System.Collections.ObjectModel;

namespace JAQOps.Data.Entities;

public class Tenant : BaseEntity
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
    public bool IsActive { get; set; } = true;
    public DateTime SubscriptionStartDate { get; set; } = DateTime.UtcNow;
    public DateTime? SubscriptionEndDate { get; set; }
    
    // Navigation properties - initialize in constructor
    public ICollection<User> Users { get; set; } = new Collection<User>();
    public ICollection<Customer> Customers { get; set; } = new Collection<Customer>();
    public ICollection<Job> Jobs { get; set; } = new Collection<Job>();
    public ICollection<InventoryItem> InventoryItems { get; set; } = new Collection<InventoryItem>();
    public ICollection<Invoice> Invoices { get; set; } = new Collection<Invoice>();
}
