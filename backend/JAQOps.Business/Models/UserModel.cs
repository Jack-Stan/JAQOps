namespace JAQOps.Business.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImageUrl { get; set; }
    public bool IsActive { get; set; }
    public Guid? TenantId { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
    
    // For creating new users
    public string? Password { get; set; }
    
    // Calculated property
    public string FullName => $"{FirstName} {LastName}";
}
