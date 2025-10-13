using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;

namespace WOMS.Infrastructure.Data
{
    public class WomsDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public WomsDbContext(DbContextOptions<WomsDbContext> options) : base(options)
        {
        }

        // Authentication entities
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        // Department entities
        public DbSet<Department> Departments { get; set; }

        // Core Work Order entities
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderColumn> WorkOrderColumns { get; set; }
        public DbSet<WorkOrderView> WorkOrderViews { get; set; }
        public DbSet<WorkOrderAssignment> WorkOrderAssignments { get; set; }

        // Customer entities
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        // Form Builder entities
        public DbSet<FormTemplate> FormTemplates { get; set; }
        public DbSet<FormSection> FormSections { get; set; }
        public DbSet<FormField> FormFields { get; set; }
        public DbSet<ValidationRule> ValidationRules { get; set; }
        public DbSet<FormSubmission> FormSubmissions { get; set; }
        public DbSet<FormAttachment> FormAttachments { get; set; }
        public DbSet<FormSignature> FormSignatures { get; set; }
        public DbSet<FormGeolocation> FormGeolocations { get; set; }

        // Inventory entities
        public DbSet<Location> Locations { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<SerializedAsset> SerializedAssets { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<StockRequest> StockRequests { get; set; }
        public DbSet<RequestItem> RequestItems { get; set; }
        public DbSet<CycleCount> CycleCounts { get; set; }
        public DbSet<CountItem> CountItems { get; set; }
        public DbSet<JobKit> JobKits { get; set; }
        public DbSet<KitItem> KitItems { get; set; }
        public DbSet<AssetHistory> AssetHistories { get; set; }

        // Billing entities
        public DbSet<BillingTemplate> BillingTemplates { get; set; }
        public DbSet<DynamicField> DynamicFields { get; set; }
        public DbSet<AggregationRule> AggregationRules { get; set; }
        public DbSet<RateTable> RateTables { get; set; }
        public DbSet<TieredRate> TieredRates { get; set; }
        public DbSet<ConditionalRate> ConditionalRates { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
        public DbSet<ApprovalRecord> ApprovalRecords { get; set; }
        public DbSet<DeliverySetting> DeliverySettings { get; set; }
        public DbSet<BillingSchedule> BillingSchedules { get; set; }

        // Workflow entities (keeping existing structure)
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<WorkflowNode> WorkflowNodes { get; set; }
        public DbSet<WorkflowStatus> WorkflowStatuses { get; set; }
        public DbSet<WorkflowCondition> WorkflowConditions { get; set; }
        public DbSet<WorkflowAction> WorkflowActions { get; set; }
        public DbSet<WorkflowApproval> WorkflowApprovals { get; set; }
        public DbSet<WorkflowEscalation> WorkflowEscalations { get; set; }
        public DbSet<WorkflowNotification> WorkflowNotifications { get; set; }
        public DbSet<WorkflowVersion> WorkflowVersions { get; set; }
        public DbSet<WorkflowTemplate> WorkflowTemplates { get; set; }
        public DbSet<WorkflowInstance> WorkflowInstances { get; set; }
        public DbSet<WorkflowExecutionLog> WorkflowExecutionLogs { get; set; }
        public DbSet<WorkflowProgress> WorkflowProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure ApplicationUser
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.FirstName).HasMaxLength(100);
                entity.Property(u => u.LastName).HasMaxLength(100);
                entity.Property(u => u.Position).HasMaxLength(100);
                entity.Property(u => u.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(u => u.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(u => u.CreatedBy).HasMaxLength(450);
                entity.Property(u => u.UpdatedBy).HasMaxLength(450);
                entity.Property(u => u.DeletedBy).HasMaxLength(450);

                // Configure self-referencing relationships
                entity.HasOne(u => u.UpdatedByUser)
                      .WithMany()
                      .HasForeignKey(u => u.UpdatedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(u => u.DeletedByUser)
                      .WithMany()
                      .HasForeignKey(u => u.DeletedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                // Configure Department relationship
                entity.HasOne(u => u.DepartmentNavigation)
                      .WithMany()
                      .HasForeignKey(u => u.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Configure Subordinates relationship (Manager -> Subordinates)
                entity.HasMany(u => u.Subordinates)
                      .WithOne()
                      .HasForeignKey("ManagerId")
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure ApplicationRole
            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.Property(r => r.Description).HasColumnType("nvarchar(max)");
                entity.Property(r => r.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            });

            // Configure Department
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
                entity.Property(d => d.Description).HasMaxLength(500);
                entity.Property(d => d.Code).HasMaxLength(50);
                entity.Property(d => d.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(d => d.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(d => d.UpdatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(d => d.CreatedBy).HasMaxLength(450);
                entity.Property(d => d.UpdatedBy).HasMaxLength(450);
                entity.Property(d => d.DeletedBy).HasMaxLength(450);

                // Indexes
                entity.HasIndex(d => d.Name);
                entity.HasIndex(d => d.Code).IsUnique().HasFilter("[Code] IS NOT NULL");
                entity.HasIndex(d => d.IsActive);
                entity.HasIndex(d => d.CreatedBy);
                entity.HasIndex(d => d.UpdatedBy);
                entity.HasIndex(d => d.DeletedBy);

                // Foreign key relationships for audit fields
                entity.HasOne(d => d.CreatedByUser)
                      .WithMany()
                      .HasForeignKey(d => d.CreatedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.UpdatedByUser)
                      .WithMany()
                      .HasForeignKey(d => d.UpdatedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.DeletedByUser)
                      .WithMany()
                      .HasForeignKey(d => d.DeletedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                // Navigation property for users
                entity.HasMany(d => d.Users)
                      .WithOne(u => u.DepartmentNavigation)
                      .HasForeignKey(u => u.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure WorkOrder
            modelBuilder.Entity<WorkOrder>(entity =>
            {
                entity.Property(wo => wo.Customer).IsRequired().HasMaxLength(255);
                entity.Property(wo => wo.CustomerContact).HasMaxLength(255);
                entity.Property(wo => wo.Type).IsRequired().HasMaxLength(100);
                entity.Property(wo => wo.Priority).IsRequired().HasMaxLength(20);
                entity.Property(wo => wo.Status).IsRequired().HasMaxLength(20);
                entity.Property(wo => wo.Assignee).HasMaxLength(255);
                entity.Property(wo => wo.Location).HasMaxLength(255);
                entity.Property(wo => wo.Address).HasColumnType("nvarchar(max)");
                entity.Property(wo => wo.Description).HasColumnType("nvarchar(max)");
                entity.Property(wo => wo.Tags).HasColumnType("nvarchar(max)");
                entity.Property(wo => wo.Equipment).HasMaxLength(255);
                entity.Property(wo => wo.Notes).HasColumnType("nvarchar(max)");
                entity.Property(wo => wo.Utility).HasMaxLength(255);
                entity.Property(wo => wo.Make).HasMaxLength(100);
                entity.Property(wo => wo.Model).HasMaxLength(100);
                entity.Property(wo => wo.Size).HasMaxLength(50);
                entity.Property(wo => wo.ManagerTechnician).HasMaxLength(255);
                entity.Property(wo => wo.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(wo => wo.UpdatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(wo => wo.CreatedBy).IsRequired().HasMaxLength(255);
                entity.Property(wo => wo.UpdatedBy).IsRequired().HasMaxLength(255);
                // entity.Property(wo => wo.IsActive).IsRequired().HasDefaultValue(true); // Property doesn't exist

                // Indexes
                entity.HasIndex(wo => wo.Status);
                entity.HasIndex(wo => wo.Priority);
                entity.HasIndex(wo => wo.Customer);
                entity.HasIndex(wo => wo.Assignee);
                entity.HasIndex(wo => wo.DueDate);
                entity.HasIndex(wo => wo.CreatedOn);
            });

            // Configure Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(255);
                entity.Property(c => c.Type).IsRequired().HasMaxLength(20);
                entity.Property(c => c.Status).IsRequired().HasMaxLength(20);
                entity.Property(c => c.TaxId).HasMaxLength(50);
                entity.Property(c => c.Industry).HasMaxLength(100);
                entity.Property(c => c.Size).HasMaxLength(20);
                entity.Property(c => c.PaymentTerms).HasMaxLength(100);
                entity.Property(c => c.Notes).HasColumnType("nvarchar(max)");
                entity.Property(c => c.Tags).HasColumnType("nvarchar(max)");
                entity.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(c => c.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(c => c.UpdatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(c => c.CreatedBy).IsRequired().HasMaxLength(255);

                // Indexes
                entity.HasIndex(c => c.Name);
                entity.HasIndex(c => c.Status);
                entity.HasIndex(c => c.Type);
            });

            // Configure Location
            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(l => l.Name).IsRequired().HasMaxLength(255);
                entity.Property(l => l.Type).IsRequired().HasMaxLength(20);
                entity.Property(l => l.Address).IsRequired().HasColumnType("nvarchar(max)");
                entity.Property(l => l.Manager).IsRequired().HasMaxLength(255);
                entity.Property(l => l.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(l => l.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(l => l.UpdatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(l => l.CreatedBy).IsRequired().HasMaxLength(255);

                entity.HasOne(l => l.ParentLocation)
                      .WithMany(l => l.SubLocations)
                      .HasForeignKey(l => l.ParentLocationId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Configure StockRequest relationships
                entity.HasMany(l => l.FromRequests)
                      .WithOne(sr => sr.FromLocation)
                      .HasForeignKey(sr => sr.FromLocationId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(l => l.ToRequests)
                      .WithOne(sr => sr.ToLocation)
                      .HasForeignKey(sr => sr.ToLocationId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Configure StockTransaction relationships
                entity.HasMany(l => l.FromTransactions)
                      .WithOne(st => st.FromLocation)
                      .HasForeignKey(st => st.FromLocationId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(l => l.ToTransactions)
                      .WithOne(st => st.ToLocation)
                      .HasForeignKey(st => st.ToLocationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure InventoryItem
            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.Property(ii => ii.PartNumber).IsRequired().HasMaxLength(100);
                entity.Property(ii => ii.Description).IsRequired().HasColumnType("nvarchar(max)");
                entity.Property(ii => ii.Category).IsRequired().HasMaxLength(100);
                entity.Property(ii => ii.Manufacturer).IsRequired().HasMaxLength(255);
                entity.Property(ii => ii.UnitOfMeasure).IsRequired().HasMaxLength(20);
                entity.Property(ii => ii.Barcode).HasMaxLength(100);
                entity.Property(ii => ii.QrCode).HasMaxLength(100);
                entity.Property(ii => ii.IsSerializedAsset).IsRequired().HasDefaultValue(false);
                entity.Property(ii => ii.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(ii => ii.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(ii => ii.UpdatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(ii => ii.CreatedBy).IsRequired().HasMaxLength(255);

                entity.HasIndex(ii => ii.PartNumber).IsUnique();
            });

            // Configure Stock
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.Property(s => s.Quantity).IsRequired().HasDefaultValue(0);
                entity.Property(s => s.MinThreshold).IsRequired().HasDefaultValue(0);
                entity.Property(s => s.MaxThreshold).IsRequired().HasDefaultValue(0);
                entity.Property(s => s.Reserved).IsRequired().HasDefaultValue(0);
                entity.Property(s => s.OnHand).IsRequired().HasDefaultValue(0);
                entity.Property(s => s.InTransit).IsRequired().HasDefaultValue(0);
                // entity.Property(s => s.LastUpdated).IsRequired().HasDefaultValueSql("GETDATE()"); // Property doesn't exist
                entity.Property(s => s.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(s => s.UpdatedOn).IsRequired().HasDefaultValueSql("GETDATE()");

                entity.HasIndex(s => new { s.ItemId, s.LocationId }).IsUnique();
            });

            // Configure SerializedAsset
            modelBuilder.Entity<SerializedAsset>(entity =>
            {
                entity.Property(sa => sa.SerialNumber).IsRequired().HasMaxLength(255);
                entity.Property(sa => sa.Manufacturer).IsRequired().HasMaxLength(255);
                entity.Property(sa => sa.Model).IsRequired().HasMaxLength(255);
                entity.Property(sa => sa.Status).IsRequired().HasMaxLength(20);
                entity.Property(sa => sa.DispositionStatus).HasMaxLength(20);
                entity.Property(sa => sa.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(sa => sa.UpdatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(sa => sa.CreatedBy).IsRequired().HasMaxLength(255);

                entity.HasIndex(sa => sa.SerialNumber).IsUnique();
                entity.HasIndex(sa => sa.Status);
            });

            // Configure StockRequest
            modelBuilder.Entity<StockRequest>(entity =>
            {
                entity.Property(sr => sr.RequesterId).IsRequired().HasMaxLength(255);
                entity.Property(sr => sr.Status).IsRequired().HasMaxLength(20);
                entity.Property(sr => sr.RequestDate).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(sr => sr.ApprovedBy).HasMaxLength(255);
                entity.Property(sr => sr.Notes).HasColumnType("nvarchar(max)");
                entity.Property(sr => sr.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(sr => sr.UpdatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(sr => sr.CreatedBy).IsRequired().HasMaxLength(255);

                entity.HasIndex(sr => sr.Status);
                entity.HasIndex(sr => sr.RequestDate);
                entity.HasIndex(sr => sr.FromLocationId);
                entity.HasIndex(sr => sr.ToLocationId);
            });

            // Configure StockTransaction
            modelBuilder.Entity<StockTransaction>(entity =>
            {
                entity.Property(st => st.Type).IsRequired().HasMaxLength(20);
                entity.Property(st => st.Quantity).IsRequired();
                entity.Property(st => st.UnitCost).IsRequired().HasColumnType("decimal(15,2)");
                entity.Property(st => st.UserId).IsRequired().HasMaxLength(255);
                entity.Property(st => st.SerialNumbers).HasColumnType("nvarchar(max)");
                entity.Property(st => st.Timestamp).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(st => st.Notes).HasColumnType("nvarchar(max)");
                entity.Property(st => st.ApprovedBy).HasMaxLength(255);
                entity.Property(st => st.Reference).HasMaxLength(255);
                entity.Property(st => st.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

                entity.HasIndex(st => st.Type);
                entity.HasIndex(st => st.Timestamp);
                entity.HasIndex(st => st.FromLocationId);
                entity.HasIndex(st => st.ToLocationId);
                entity.HasIndex(st => st.ItemId);
            });

            // Configure FormTemplate
            modelBuilder.Entity<FormTemplate>(entity =>
            {
                entity.Property(ft => ft.Name).IsRequired().HasMaxLength(255);
                entity.Property(ft => ft.Description).HasColumnType("nvarchar(max)");
                entity.Property(ft => ft.Category).IsRequired().HasMaxLength(100);
                entity.Property(ft => ft.Status).IsRequired().HasMaxLength(20);
                entity.Property(ft => ft.Version).IsRequired().HasDefaultValue(1);
                entity.Property(ft => ft.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(ft => ft.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(ft => ft.UpdatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(ft => ft.CreatedBy).IsRequired().HasMaxLength(255);

                entity.HasIndex(ft => ft.Category);
                entity.HasIndex(ft => ft.Status);
                entity.HasIndex(ft => ft.IsActive);
            });

            // Configure BillingTemplate
            modelBuilder.Entity<BillingTemplate>(entity =>
            {
                entity.Property(bt => bt.Name).IsRequired().HasMaxLength(255);
                entity.Property(bt => bt.CustomerId).IsRequired().HasMaxLength(255);
                entity.Property(bt => bt.CustomerName).IsRequired().HasMaxLength(255);
                entity.Property(bt => bt.OutputFormat).IsRequired().HasMaxLength(20);
                entity.Property(bt => bt.FieldOrder).HasColumnType("nvarchar(max)");
                entity.Property(bt => bt.FieldFormats).HasColumnType("nvarchar(max)");
                entity.Property(bt => bt.FileNamingConvention).HasMaxLength(255);
                entity.Property(bt => bt.DeliveryMethod).IsRequired().HasMaxLength(20);
                entity.Property(bt => bt.InvoiceType).IsRequired().HasMaxLength(20);
                entity.Property(bt => bt.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(bt => bt.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(bt => bt.UpdatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(bt => bt.CreatedBy).IsRequired().HasMaxLength(255);
            });

            // Configure Invoice
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(100);
                entity.Property(i => i.CustomerId).IsRequired().HasMaxLength(255);
                entity.Property(i => i.CustomerName).IsRequired().HasMaxLength(255);
                entity.Property(i => i.Status).IsRequired().HasMaxLength(20);
                entity.Property(i => i.InvoiceType).IsRequired().HasMaxLength(20);
                entity.Property(i => i.GeneratedAt).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(i => i.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(i => i.UpdatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(i => i.CreatedBy).IsRequired().HasMaxLength(255);

                entity.HasIndex(i => i.InvoiceNumber).IsUnique();
                entity.HasIndex(i => i.Status);
                entity.HasIndex(i => i.CustomerId);
                entity.HasIndex(i => i.DueDate);
            });

            // Configure Workflow
            modelBuilder.Entity<Workflow>(entity =>
            {
                entity.Property(w => w.Name).IsRequired().HasMaxLength(255);
                entity.Property(w => w.Description).HasColumnType("nvarchar(max)");
                entity.Property(w => w.Category).IsRequired().HasMaxLength(100);
                entity.Property(w => w.CurrentVersion).IsRequired().HasDefaultValue(1);
                entity.Property(w => w.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(w => w.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(w => w.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(w => w.CreatedBy).IsRequired().HasMaxLength(255);

                entity.HasIndex(w => w.Category);
                entity.HasIndex(w => w.IsActive);
            });

            // Configure WorkflowStatus
            modelBuilder.Entity<WorkflowStatus>(entity =>
            {
                entity.Property(ws => ws.StatusId).IsRequired().HasMaxLength(100);
                entity.Property(ws => ws.Name).IsRequired().HasMaxLength(100);
                entity.Property(ws => ws.Description).HasMaxLength(500);
                entity.Property(ws => ws.Color).IsRequired().HasMaxLength(7);
                entity.Property(ws => ws.SortOrder).IsRequired();

                entity.HasOne(ws => ws.Workflow)
                      .WithMany(w => w.Statuses)
                      .HasForeignKey(ws => ws.WorkflowId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Configure WorkflowTransition relationships
                entity.HasMany(ws => ws.OutgoingTransitions)
                      .WithOne(wt => wt.FromStatus)
                      .HasForeignKey(wt => wt.FromStatusId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(ws => ws.IncomingTransitions)
                      .WithOne(wt => wt.ToStatus)
                      .HasForeignKey(wt => wt.ToStatusId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure WorkflowTransition
            modelBuilder.Entity<WorkflowTransition>(entity =>
            {
                entity.Property(wt => wt.TransitionId).IsRequired().HasMaxLength(100);
                entity.Property(wt => wt.Name).IsRequired().HasMaxLength(200);
                entity.Property(wt => wt.Description).HasMaxLength(500);

                entity.HasOne(wt => wt.Workflow)
                      .WithMany()
                      .HasForeignKey(wt => wt.WorkflowId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(wt => wt.FromStatusId);
                entity.HasIndex(wt => wt.ToStatusId);
            });

            // Configure WorkflowNode
            modelBuilder.Entity<WorkflowNode>(entity =>
            {
                entity.Property(wn => wn.Type).IsRequired().HasMaxLength(20);
                entity.Property(wn => wn.Title).IsRequired().HasMaxLength(255);
                entity.Property(wn => wn.Description).HasColumnType("nvarchar(max)");
                entity.Property(wn => wn.Position).HasColumnType("nvarchar(max)");
                entity.Property(wn => wn.Data).HasColumnType("nvarchar(max)");
                entity.Property(wn => wn.Connections).HasColumnType("nvarchar(max)");

                entity.HasOne(wn => wn.Workflow)
                      .WithMany()
                      .HasForeignKey(wn => wn.WorkflowId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure WorkflowInstance
            modelBuilder.Entity<WorkflowInstance>(entity =>
            {
                entity.Property(wi => wi.Status).IsRequired().HasMaxLength(20);
                entity.Property(wi => wi.Data).HasColumnType("nvarchar(max)");
                entity.Property(wi => wi.StartedAt).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(wi => wi.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.Property(wi => wi.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

                entity.HasOne(wi => wi.Workflow)
                      .WithMany()
                      .HasForeignKey(wi => wi.WorkflowId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(wi => wi.WorkOrder)
                      .WithMany(wo => wo.WorkflowInstances)
                      .HasForeignKey(wi => wi.WorkOrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(wi => wi.WorkflowId);
                entity.HasIndex(wi => wi.WorkOrderId);
                entity.HasIndex(wi => wi.Status);
            });

            // Configure WorkflowProgress
            modelBuilder.Entity<WorkflowProgress>(entity =>
            {
                entity.Property(wp => wp.CompletedSteps).HasColumnType("nvarchar(max)");
                entity.Property(wp => wp.PendingSteps).HasColumnType("nvarchar(max)");
                entity.Property(wp => wp.Metadata).HasColumnType("nvarchar(max)");
                entity.Property(wp => wp.LastUpdated).IsRequired().HasDefaultValueSql("GETDATE()");

                entity.HasOne(wp => wp.Workflow)
                      .WithMany()
                      .HasForeignKey(wp => wp.WorkflowId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(wp => wp.CurrentStatus)
                      .WithMany()
                      .HasForeignKey(wp => wp.CurrentStatusId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(wp => wp.WorkflowId);
                entity.HasIndex(wp => wp.CurrentStatusId);
            });
        }
    }
}