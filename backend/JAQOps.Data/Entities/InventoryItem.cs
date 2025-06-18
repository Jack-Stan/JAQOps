using System.Collections.ObjectModel;

namespace JAQOps.Data.Entities;

public class InventoryItem : TenantEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? SKU { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal SalePrice { get; set; }
    public int StockQuantity { get; set; }
    public int MinimumStockQuantity { get; set; }
    public string? Unit { get; set; }  // e.g., pcs, kg, m, etc.
    public string? Location { get; set; }
    public string? Supplier { get; set; }
    
    // Navigation properties
    public ICollection<JobInventoryItem> JobItems { get; set; } = new Collection<JobInventoryItem>();
}
