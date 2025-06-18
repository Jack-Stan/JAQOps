namespace JAQOps.Data.Entities;

public class InvoiceItem : BaseEntity
{
    public required string Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? Unit { get; set; }
    
    // Invoice relationship
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;
    
    // Optional link to inventory item
    public Guid? InventoryItemId { get; set; }
    public InventoryItem? InventoryItem { get; set; }
    
    // Calculated property (not mapped to DB)
    public decimal TotalPrice => UnitPrice * Quantity;
}
