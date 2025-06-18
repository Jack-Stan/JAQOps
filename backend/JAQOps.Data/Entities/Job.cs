using System.Collections.ObjectModel;

namespace JAQOps.Data.Entities;

public class Job : TenantEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime ScheduledStartDate { get; set; }
    public DateTime? ScheduledEndDate { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
    public decimal EstimatedHours { get; set; }
    public decimal? ActualHours { get; set; }
    public bool IsUrgent { get; set; }
    
    // Status relationship
    public Guid JobStatusId { get; set; }
    public JobStatus JobStatus { get; set; } = null!;
    
    // Customer relationship
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    // Navigation properties
    public ICollection<JobAssignment> Assignments { get; set; } = new Collection<JobAssignment>();
    public ICollection<JobInventoryItem> InventoryItems { get; set; } = new Collection<JobInventoryItem>();
    public ICollection<Invoice> Invoices { get; set; } = new Collection<Invoice>();
}
