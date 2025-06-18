using System.Collections.ObjectModel;

namespace JAQOps.Data.Entities;

public enum InvoiceStatus
{
    Draft,
    Sent,
    Paid,
    Overdue,
    Cancelled
}

public class Invoice : TenantEntity
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    public string? Notes { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime? PaidDate { get; set; }
    public string? PaymentReference { get; set; }
    
    // Customer relationship
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    // Job relationship (optional - an invoice might be for multiple jobs)
    public Guid? JobId { get; set; }
    public Job? Job { get; set; }
    
    // Navigation properties
    public ICollection<InvoiceItem> Items { get; set; } = new Collection<InvoiceItem>();
    
    // Methods
    public void CalculateTotals()
    {
        Subtotal = Items.Sum(i => i.Quantity * i.UnitPrice);
        TaxAmount = Subtotal * 0.21m; // Assuming 21% VAT
        TotalAmount = Subtotal + TaxAmount;
    }
}
