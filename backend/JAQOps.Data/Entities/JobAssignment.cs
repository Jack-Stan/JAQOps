namespace JAQOps.Data.Entities;

public class JobAssignment : BaseEntity
{
    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
    
    // Job relationship
    public Guid JobId { get; set; }
    public Job Job { get; set; } = null!;
    
    // User (technician) relationship
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
