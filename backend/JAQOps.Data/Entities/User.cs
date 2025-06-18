using System.Collections.ObjectModel;

namespace JAQOps.Data.Entities;

public class User : BaseEntity
{
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginDate { get; set; }
    
    // Tenant relationship (nullable for system admin)
    public Guid? TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    
    // Navigation properties
    public ICollection<UserRole> UserRoles { get; set; } = new Collection<UserRole>();
    public ICollection<JobAssignment> JobAssignments { get; set; } = new Collection<JobAssignment>();
    
    // Calculated property (not mapped to DB)
    public string FullName => $"{FirstName} {LastName}";
}
