using System.Collections.ObjectModel;

namespace JAQOps.Data.Entities;

public class Role : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    // Navigation properties
    public ICollection<UserRole> UserRoles { get; set; } = new Collection<UserRole>();
}

public class UserRole
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
