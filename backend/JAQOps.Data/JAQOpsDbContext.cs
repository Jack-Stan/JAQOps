using Microsoft.EntityFrameworkCore;
using JAQOps.Data.Entities;

namespace JAQOps.Data;

public class JAQOpsDbContext : DbContext
{
    public JAQOpsDbContext(DbContextOptions<JAQOpsDbContext> options) : base(options)
    {
    }

    // Tenants
    public DbSet<Tenant> Tenants { get; set; }
    
    // Users & Authentication
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    // Customers & Contacts
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    
    // Jobs & Planning
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobStatus> JobStatuses { get; set; }
    public DbSet<JobAssignment> JobAssignments { get; set; }

    // Inventory
    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<JobInventoryItem> JobInventoryItems { get; set; }

    // Invoicing
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure global query filters for multi-tenancy
        modelBuilder.Entity<Customer>().HasQueryFilter(c => c.TenantId == GetTenantId());
        modelBuilder.Entity<Contact>().HasQueryFilter(c => c.TenantId == GetTenantId());
        modelBuilder.Entity<Job>().HasQueryFilter(j => j.TenantId == GetTenantId());
        modelBuilder.Entity<InventoryItem>().HasQueryFilter(i => i.TenantId == GetTenantId());
        modelBuilder.Entity<Invoice>().HasQueryFilter(i => i.TenantId == GetTenantId());
        modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == GetTenantId() || u.TenantId == null);

        // Many-to-many relationship for User-Role
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        // Configure other relationships and constraints as needed
    }

    // Helper method to get the current tenant ID from the context
    private Guid? GetTenantId()
    {
        // In a real implementation, this would get the tenant ID from the current user claims
        // For now, we'll return null (indicating no tenant filtering)
        return null;
    }
}
