using System.Collections.ObjectModel;

namespace JAQOps.Data.Entities;

public class Customer : TenantEntity
{
    public required string Name { get; set; }
    public required string VatNumber { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Notes { get; set; }
    
    // Navigation properties
    public ICollection<Contact> Contacts { get; set; } = new Collection<Contact>();
    public ICollection<Job> Jobs { get; set; } = new Collection<Job>();
    public ICollection<Invoice> Invoices { get; set; } = new Collection<Invoice>();
}
