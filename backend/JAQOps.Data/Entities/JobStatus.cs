using System.Collections.ObjectModel;

namespace JAQOps.Data.Entities;

public class JobStatus : TenantEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public int Order { get; set; }
    
    // Navigation properties
    public ICollection<Job> Jobs { get; set; } = new Collection<Job>();
}
