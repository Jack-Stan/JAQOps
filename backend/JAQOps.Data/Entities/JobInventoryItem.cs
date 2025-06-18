namespace JAQOps.Data.Entities;

public class JobInventoryItem : BaseEntity
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? Notes { get; set; }
    
    // Job relationship
    public Guid JobId { get; set; }
    public Job Job { get; set; } = null!;
    
    // Inventory Item relationship
    public Guid InventoryItemId { get; set; }
    public InventoryItem InventoryItem { get; set; } = null!;
    
    // Calculated property (not mapped to DB)
    public decimal TotalPrice => UnitPrice * Quantity;
}
