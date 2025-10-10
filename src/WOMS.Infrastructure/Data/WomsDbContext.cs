using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;

namespace WOMS.Infrastructure.Data
{
    public class WomsDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public WomsDbContext(DbContextOptions<WomsDbContext> options) : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Department> Departments { get; set; }
        
        // Workflow entities
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<WorkflowNode> WorkflowNodes { get; set; }
        public DbSet<WorkflowEdge> WorkflowEdges { get; set; }
        public DbSet<WorkflowStatus> WorkflowStatuses { get; set; }
        public DbSet<WorkflowTransition> WorkflowTransitions { get; set; }
        public DbSet<WorkflowRule> WorkflowRules { get; set; }
        public DbSet<WorkflowRuleCondition> WorkflowRuleConditions { get; set; }
        public DbSet<WorkflowRuleAction> WorkflowRuleActions { get; set; }
        public DbSet<WorkflowAction> WorkflowActions { get; set; }
        public DbSet<WorkflowAssignment> WorkflowAssignments { get; set; }
        public DbSet<WorkflowProgress> WorkflowProgresses { get; set; }
        public DbSet<WorkflowInstance> WorkflowInstances { get; set; }
        public DbSet<WorkflowInstanceStep> WorkflowInstanceSteps { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<WorkflowForm> WorkflowForms { get; set; }
        
        // Additional entities from Supabase schema
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<SentNotification> SentNotifications { get; set; }
        public DbSet<WorkOrderAssignment> WorkOrderAssignments { get; set; }
        public DbSet<WorkOrderAttachment> WorkOrderAttachments { get; set; }
        
        // New entities for enhanced functionality
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<WorkOrderType> WorkOrderTypes { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteStop> RouteStops { get; set; }
        public DbSet<AssignmentTemplate> AssignmentTemplates { get; set; }
        
        // Junction tables
        public DbSet<TechnicianSkill> TechnicianSkills { get; set; }
        public DbSet<TechnicianEquipment> TechnicianEquipments { get; set; }
        public DbSet<TechnicianZone> TechnicianZones { get; set; }
        public DbSet<WorkOrderSkillRequirement> WorkOrderSkillRequirements { get; set; }
        public DbSet<WorkOrderEquipmentRequirement> WorkOrderEquipmentRequirements { get; set; }
        public DbSet<WorkOrderZone> WorkOrderZones { get; set; }
        public DbSet<WorkOrderTypeSkillRequirement> WorkOrderTypeSkillRequirements { get; set; }
        public DbSet<WorkOrderTypeEquipmentRequirement> WorkOrderTypeEquipmentRequirements { get; set; }
        public DbSet<AssignmentTemplateWorkType> AssignmentTemplateWorkTypes { get; set; }
        public DbSet<AssignmentTemplateZone> AssignmentTemplateZones { get; set; }
        public DbSet<AssignmentTemplateSkill> AssignmentTemplateSkills { get; set; }
        public DbSet<AssignmentTemplateTechnician> AssignmentTemplateTechnicians { get; set; }
        
        // Additional inventory management entities
        public DbSet<Location> Locations { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<CycleCount> CycleCounts { get; set; }
        public DbSet<CycleCountItem> CycleCountItems { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetHistory> AssetHistories { get; set; }
        public DbSet<JobKit> JobKits { get; set; }
        public DbSet<JobKitItem> JobKitItems { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<InventoryDisposition> InventoryDispositions { get; set; }
        public DbSet<ValidationIssue> ValidationIssues { get; set; }
        public DbSet<InventoryLocation> InventoryLocations { get; set; }
        public DbSet<ScanLog> ScanLogs { get; set; }
        
        // Billing Template entities
        public DbSet<BillingTemplate> BillingTemplates { get; set; }
        public DbSet<BillingTemplateFieldOrder> BillingTemplateFieldOrders { get; set; }
        
        // Rate Table entities
        public DbSet<RateTable> RateTables { get; set; }
        public DbSet<RateTableItem> RateTableItems { get; set; }
        
        // Billing Schedule entities
        public DbSet<BillingSchedule> BillingSchedules { get; set; }
        public DbSet<BillingScheduleTemplate> BillingScheduleTemplates { get; set; }
        public DbSet<BillingScheduleRun> BillingScheduleRuns { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
           

            // Configure ApplicationUser self references
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(u => u.UpdatedByUser)
                      .WithMany()
                      .HasForeignKey(u => u.UpdatedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(u => u.DeletedByUser)
                      .WithMany()
                      .HasForeignKey(u => u.DeletedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.FullName).HasMaxLength(200);
                entity.Property(u => u.Address).IsRequired().HasMaxLength(500);
                entity.Property(u => u.City).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PostalCode).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Phone).HasMaxLength(20);
                entity.Property(u => u.Skills).HasColumnType("nvarchar(max)");
                entity.Property(u => u.Status).HasConversion<string>();
            });

            // Configure RefreshToken entity
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(rt => rt.Id);
                entity.Property(rt => rt.UserId).IsRequired();
                entity.Property(rt => rt.Refresh_Token).IsRequired();
                entity.Property(rt => rt.JwtToken).IsRequired();
                entity.Property(rt => rt.RefreshTokenExpirationTime).IsRequired();
                entity.Property(rt => rt.CreatedOn).IsRequired();

                entity.HasOne(rt => rt.User)
                      .WithMany()
                      .HasForeignKey(rt => rt.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure WorkOrder entity
            modelBuilder.Entity<WorkOrder>(entity =>
            {
                entity.HasKey(wo => wo.Id);
                entity.Property(wo => wo.WorkOrderNumber).IsRequired().HasMaxLength(50);
                entity.Property(wo => wo.Priority).IsRequired().HasMaxLength(20);
                entity.Property(wo => wo.Status).IsRequired().HasMaxLength(50);
                entity.Property(wo => wo.ServiceAddress).IsRequired().HasMaxLength(500);
                entity.Property(wo => wo.Metadata).HasColumnType("nvarchar(max)");

                entity.HasOne(wo => wo.Workflow)
                      .WithMany()
                      .HasForeignKey(wo => wo.WorkflowId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(wo => wo.AssignedTechnician)
                      .WithMany()
                      .HasForeignKey(wo => wo.AssignedTechnicianId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(wo => wo.CreatedByUser)
                      .WithMany()
                      .HasForeignKey(wo => wo.CreatedBy)
                      .OnDelete(DeleteBehavior.NoAction);

                // Single column indexes for filtering and sorting
                entity.HasIndex(wo => wo.WorkOrderNumber).IsUnique();
                entity.HasIndex(wo => wo.Status);
                entity.HasIndex(wo => wo.Priority);
                entity.HasIndex(wo => wo.AssignedTechnicianId);
                entity.HasIndex(wo => wo.WorkOrderTypeId);
                entity.HasIndex(wo => wo.ScheduledDate);
                entity.HasIndex(wo => wo.CreatedOn);
                entity.HasIndex(wo => wo.IsDeleted);
                
                // Composite indexes for common query patterns
                entity.HasIndex(wo => new { wo.IsDeleted, wo.Status })
                      .HasDatabaseName("IX_WorkOrder_IsDeleted_Status");
                      
                entity.HasIndex(wo => new { wo.IsDeleted, wo.Priority })
                      .HasDatabaseName("IX_WorkOrder_IsDeleted_Priority");
                      
                entity.HasIndex(wo => new { wo.IsDeleted, wo.AssignedTechnicianId })
                      .HasDatabaseName("IX_WorkOrder_IsDeleted_AssignedTechnicianId");
                      
                entity.HasIndex(wo => new { wo.IsDeleted, wo.WorkOrderTypeId })
                      .HasDatabaseName("IX_WorkOrder_IsDeleted_WorkOrderTypeId");
                      
                entity.HasIndex(wo => new { wo.IsDeleted, wo.ScheduledDate })
                      .HasDatabaseName("IX_WorkOrder_IsDeleted_ScheduledDate");
                      
                entity.HasIndex(wo => new { wo.IsDeleted, wo.CreatedOn })
                      .HasDatabaseName("IX_WorkOrder_IsDeleted_CreatedOn");
                      
                // Composite index for sorting and filtering
                entity.HasIndex(wo => new { wo.IsDeleted, wo.Status, wo.CreatedOn })
                      .HasDatabaseName("IX_WorkOrder_IsDeleted_Status_CreatedOn");
                      
                entity.HasIndex(wo => new { wo.IsDeleted, wo.Priority, wo.CreatedOn })
                      .HasDatabaseName("IX_WorkOrder_IsDeleted_Priority_CreatedOn");
            });

            // Configure Workflow entity
            modelBuilder.Entity<Workflow>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Name).IsRequired().HasMaxLength(200);
                entity.Property(w => w.Version).IsRequired();
                entity.Property(w => w.Metadata).HasColumnType("nvarchar(max)");

                entity.HasOne(w => w.CreatedByUser)
                      .WithMany()
                      .HasForeignKey(w => w.CreatedBy)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasIndex(w => w.Name);
            });

            // Configure WorkflowNode entity
            modelBuilder.Entity<WorkflowNode>(entity =>
            {
                entity.HasKey(wn => wn.Id);
                entity.Property(wn => wn.NodeId).IsRequired().HasMaxLength(100);
                entity.Property(wn => wn.Type).IsRequired().HasMaxLength(50);
                entity.Property(wn => wn.Label).IsRequired().HasMaxLength(200);
                entity.Property(wn => wn.Config).HasColumnType("nvarchar(max)");

                entity.HasOne(wn => wn.Workflow)
                      .WithMany(w => w.Nodes)
                      .HasForeignKey(wn => wn.WorkflowId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(wn => new { wn.WorkflowId, wn.NodeId }).IsUnique();
            });

            // Configure WorkflowEdge entity
            modelBuilder.Entity<WorkflowEdge>(entity =>
            {
                entity.HasKey(we => we.Id);
                entity.Property(we => we.EdgeId).IsRequired().HasMaxLength(100);
                entity.Property(we => we.SourceNodeId).IsRequired().HasMaxLength(100);
                entity.Property(we => we.TargetNodeId).IsRequired().HasMaxLength(100);

                entity.HasOne(we => we.Workflow)
                      .WithMany(w => w.Edges)
                      .HasForeignKey(we => we.WorkflowId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Note: Complex foreign key relationships will be handled in application logic
                // entity.HasOne(we => we.SourceNode) - requires composite foreign keys
                // entity.HasOne(we => we.TargetNode) - requires composite foreign keys

                entity.HasIndex(we => new { we.WorkflowId, we.EdgeId }).IsUnique();
            });

            // Configure WorkflowStatus entity
            modelBuilder.Entity<WorkflowStatus>(entity =>
            {
                entity.HasKey(ws => ws.Id);
                entity.Property(ws => ws.StatusId).IsRequired().HasMaxLength(100);
                entity.Property(ws => ws.Name).IsRequired().HasMaxLength(100);
                entity.Property(ws => ws.Color).IsRequired().HasMaxLength(7);
                entity.Property(ws => ws.SortOrder).IsRequired();

                entity.HasOne(ws => ws.Workflow)
                      .WithMany(w => w.Statuses)
                      .HasForeignKey(ws => ws.WorkflowId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasIndex(ws => new { ws.WorkflowId, ws.StatusId }).IsUnique();
            });

            // Configure WorkflowTransition entity
            modelBuilder.Entity<WorkflowTransition>(entity =>
            {
                entity.HasKey(wt => wt.Id);
                entity.Property(wt => wt.TransitionId).IsRequired().HasMaxLength(100);
                entity.Property(wt => wt.Name).IsRequired().HasMaxLength(200);

                entity.HasOne(wt => wt.Workflow)
                      .WithMany(w => w.Transitions)
                      .HasForeignKey(wt => wt.WorkflowId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(wt => wt.FromStatus)
                      .WithMany(ws => ws.OutgoingTransitions)
                      .HasForeignKey(wt => wt.FromStatusId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(wt => wt.ToStatus)
                      .WithMany(ws => ws.IncomingTransitions)
                      .HasForeignKey(wt => wt.ToStatusId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasIndex(wt => new { wt.WorkflowId, wt.TransitionId }).IsUnique();
            });

            // Configure WorkflowRule entity
            modelBuilder.Entity<WorkflowRule>(entity =>
            {
                entity.HasKey(wr => wr.Id);
                entity.Property(wr => wr.RuleId).IsRequired().HasMaxLength(100);
                entity.Property(wr => wr.Name).IsRequired().HasMaxLength(200);
                entity.Property(wr => wr.Type).IsRequired().HasMaxLength(50);

                entity.HasOne(wr => wr.Workflow)
                      .WithMany(w => w.Rules)
                      .HasForeignKey(wr => wr.WorkflowId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(wr => new { wr.WorkflowId, wr.RuleId }).IsUnique();
            });

            // Configure WorkflowRuleCondition entity
            modelBuilder.Entity<WorkflowRuleCondition>(entity =>
            {
                entity.HasKey(wrc => wrc.Id);
                entity.Property(wrc => wrc.ConditionId).IsRequired().HasMaxLength(100);
                entity.Property(wrc => wrc.Field).IsRequired().HasMaxLength(100);
                entity.Property(wrc => wrc.Operator).IsRequired().HasMaxLength(50);
                entity.Property(wrc => wrc.Value).HasColumnType("nvarchar(max)");
                entity.Property(wrc => wrc.SortOrder).IsRequired();

                entity.HasOne(wrc => wrc.Rule)
                      .WithMany(wr => wr.Conditions)
                      .HasForeignKey(wrc => wrc.RuleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure WorkflowRuleAction entity
            modelBuilder.Entity<WorkflowRuleAction>(entity =>
            {
                entity.HasKey(wra => wra.Id);
                entity.Property(wra => wra.ActionId).IsRequired().HasMaxLength(100);
                entity.Property(wra => wra.Type).IsRequired().HasMaxLength(50);
                entity.Property(wra => wra.Config).HasColumnType("nvarchar(max)");
                entity.Property(wra => wra.SortOrder).IsRequired();

                entity.HasOne(wra => wra.Rule)
                      .WithMany(wr => wr.Actions)
                      .HasForeignKey(wra => wra.RuleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure WorkflowAction entity
            modelBuilder.Entity<WorkflowAction>(entity =>
            {
                entity.HasKey(wa => wa.Id);
                entity.Property(wa => wa.ActionId).IsRequired().HasMaxLength(100);
                entity.Property(wa => wa.Type).IsRequired().HasMaxLength(50);
                entity.Property(wa => wa.Config).HasColumnType("nvarchar(max)");

                entity.HasOne(wa => wa.Workflow)
                      .WithMany(w => w.Actions)
                      .HasForeignKey(wa => wa.WorkflowId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(wa => new { wa.WorkflowId, wa.ActionId }).IsUnique();
            });

            // Configure WorkflowAssignment entity
            modelBuilder.Entity<WorkflowAssignment>(entity =>
            {
                entity.HasKey(wa => wa.Id);
                entity.Property(wa => wa.NodeId).IsRequired().HasMaxLength(100);
                entity.Property(wa => wa.AssigneeId).IsRequired().HasMaxLength(100);
                entity.Property(wa => wa.AssigneeName).IsRequired().HasMaxLength(200);
                entity.Property(wa => wa.Status).IsRequired().HasMaxLength(50);
                entity.Property(wa => wa.Priority).IsRequired().HasMaxLength(20);

                entity.HasOne(wa => wa.Workflow)
                      .WithMany()
                      .HasForeignKey(wa => wa.WorkflowId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Note: Complex foreign key relationship will be handled in application logic
                // entity.HasOne(wa => wa.Node) - requires composite foreign keys
            });

            // Configure WorkflowProgress entity
            modelBuilder.Entity<WorkflowProgress>(entity =>
            {
                entity.HasKey(wp => wp.Id);
                entity.Property(wp => wp.CompletedSteps).HasColumnType("nvarchar(max)");
                entity.Property(wp => wp.PendingSteps).HasColumnType("nvarchar(max)");
                entity.Property(wp => wp.Metadata).HasColumnType("nvarchar(max)");

                entity.HasOne(wp => wp.Workflow)
                      .WithMany()
                      .HasForeignKey(wp => wp.WorkflowId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(wp => wp.CurrentStatus)
                      .WithMany(ws => ws.WorkflowProgresses)
                      .HasForeignKey(wp => wp.CurrentStatusId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure WorkflowInstance entity
            modelBuilder.Entity<WorkflowInstance>(entity =>
            {
                entity.HasKey(wi => wi.Id);
                entity.Property(wi => wi.InstanceName).HasMaxLength(200);
                entity.Property(wi => wi.Status).IsRequired().HasMaxLength(20);
                entity.Property(wi => wi.CurrentNodeId).HasMaxLength(100);
                entity.Property(wi => wi.Metadata).HasColumnType("nvarchar(max)");

                entity.HasOne(wi => wi.Workflow)
                      .WithMany(w => w.Instances)
                      .HasForeignKey(wi => wi.WorkflowId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(wi => wi.CreatedByUser)
                      .WithMany()
                      .HasForeignKey(wi => wi.CreatedBy)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // Configure WorkflowInstanceStep entity
            modelBuilder.Entity<WorkflowInstanceStep>(entity =>
            {
                entity.HasKey(wis => wis.Id);
                entity.Property(wis => wis.NodeId).IsRequired().HasMaxLength(100);
                entity.Property(wis => wis.StepName).IsRequired().HasMaxLength(200);
                entity.Property(wis => wis.Status).IsRequired().HasMaxLength(20);
                entity.Property(wis => wis.AssigneeId).HasMaxLength(100);
                entity.Property(wis => wis.AssigneeName).HasMaxLength(200);
                entity.Property(wis => wis.ResultData).HasColumnType("nvarchar(max)");
                entity.Property(wis => wis.ErrorMessage).HasMaxLength(1000);

                entity.HasOne(wis => wis.Instance)
                      .WithMany(wi => wi.Steps)
                      .HasForeignKey(wis => wis.InstanceId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Note: Complex foreign key relationship will be handled in application logic
                // entity.HasOne(wis => wis.Node) - requires additional WorkflowId property
            });

            // Configure Form entity
            modelBuilder.Entity<Form>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired().HasMaxLength(200);
                entity.Property(f => f.Fields).HasColumnType("nvarchar(max)");
                entity.Property(f => f.Settings).HasColumnType("nvarchar(max)");

                entity.HasOne(f => f.User)
                      .WithMany()
                      .HasForeignKey(f => f.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(f => f.Name);
            });

            // Configure WorkflowForm entity
            modelBuilder.Entity<WorkflowForm>(entity =>
            {
                entity.HasKey(wf => wf.Id);

                entity.HasOne(wf => wf.Workflow)
                      .WithMany(w => w.WorkflowForms)
                      .HasForeignKey(wf => wf.WorkflowId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(wf => wf.Form)
                      .WithMany(f => f.WorkflowForms)
                      .HasForeignKey(wf => wf.FormId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(wf => new { wf.WorkflowId, wf.FormId }).IsUnique();
            });

            // Configure Inventory entity
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.AccountNumber).IsRequired().HasMaxLength(50);
                entity.Property(i => i.Identifier).IsRequired().HasMaxLength(100);
                entity.Property(i => i.SystemId).IsRequired().HasMaxLength(100);
                entity.Property(i => i.FileName).IsRequired().HasMaxLength(255);
                entity.Property(i => i.UploadStatus).IsRequired().HasConversion<string>();
                entity.Property(i => i.Category).HasConversion<string>();
                entity.Property(i => i.UnitOfMeasure).HasConversion<string>();
                entity.Property(i => i.CurrentStatus).HasConversion<string>();
                entity.Property(i => i.DispositionStatus).HasConversion<string>();

                entity.HasIndex(i => i.AccountNumber);
                entity.HasIndex(i => i.Identifier);
                entity.HasIndex(i => i.SystemId);
            });

            // Configure NotificationTemplate entity
            modelBuilder.Entity<NotificationTemplate>(entity =>
            {
                entity.HasKey(nt => nt.Id);
                entity.Property(nt => nt.Name).IsRequired().HasMaxLength(200);
                entity.Property(nt => nt.Type).IsRequired().HasMaxLength(50);
                entity.Property(nt => nt.Content).IsRequired().HasColumnType("nvarchar(max)");
                entity.Property(nt => nt.Placeholders).HasColumnType("nvarchar(max)");

                entity.HasOne(nt => nt.User)
                      .WithMany()
                      .HasForeignKey(nt => nt.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(nt => nt.Name);
                entity.HasIndex(nt => nt.Type);
            });

            // Configure SentNotification entity
            modelBuilder.Entity<SentNotification>(entity =>
            {
                entity.HasKey(sn => sn.Id);
                entity.Property(sn => sn.Recipient).IsRequired().HasMaxLength(200);
                entity.Property(sn => sn.Status).IsRequired().HasMaxLength(50);
                entity.Property(sn => sn.Type).IsRequired().HasMaxLength(50);
                entity.Property(sn => sn.Content).IsRequired().HasColumnType("nvarchar(max)");
                entity.Property(sn => sn.Metadata).HasColumnType("nvarchar(max)");

                entity.HasOne(sn => sn.Template)
                      .WithMany(nt => nt.SentNotifications)
                      .HasForeignKey(sn => sn.TemplateId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(sn => sn.User)
                      .WithMany()
                      .HasForeignKey(sn => sn.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(sn => sn.Recipient);
                entity.HasIndex(sn => sn.Status);
            });


            // Configure WorkOrderAssignment entity
            modelBuilder.Entity<WorkOrderAssignment>(entity =>
            {
                entity.HasKey(woa => woa.Id);
                entity.Property(woa => woa.AccountNumber).IsRequired().HasMaxLength(50);
                entity.Property(woa => woa.Identifier).IsRequired().HasMaxLength(100);
                entity.Property(woa => woa.SystemId).IsRequired().HasMaxLength(100);

                entity.HasOne(woa => woa.WorkOrder)
                      .WithMany(wo => wo.WorkOrderAssignments)
                      .HasForeignKey(woa => woa.WorkOrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(woa => woa.AccountNumber);
                entity.HasIndex(woa => woa.Identifier);
            });

            // Configure WorkOrderAttachment entity
            modelBuilder.Entity<WorkOrderAttachment>(entity =>
            {
                entity.HasKey(woa => woa.Id);
                entity.Property(woa => woa.FileName).IsRequired().HasMaxLength(255);
                entity.Property(woa => woa.FilePath).IsRequired().HasMaxLength(500);

                entity.HasOne(woa => woa.WorkOrder)
                      .WithMany(wo => wo.WorkOrderAttachments)
                      .HasForeignKey(woa => woa.WorkOrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(woa => woa.UploadedByUser)
                      .WithMany()
                      .HasForeignKey(woa => woa.UploadedBy)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasIndex(woa => woa.FileName);
            });

            // Configure View entity
            modelBuilder.Entity<View>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Name).IsRequired().HasMaxLength(100);
                entity.Property(v => v.SelectedColumns).IsRequired().HasColumnType("nvarchar(max)");

                entity.HasOne(v => v.CreatedByUser)
                      .WithMany()
                      .HasForeignKey(v => v.CreatedBy)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasIndex(v => v.Name);
            });

            // Configure Department entity
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
                entity.Property(d => d.Description).HasMaxLength(500);
                entity.Property(d => d.Code).HasMaxLength(50);
                entity.Property(d => d.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Active");
                entity.Property(d => d.IsActive).IsRequired().HasDefaultValue(true);

                entity.HasOne(d => d.CreatedByUser)
                      .WithMany()
                      .HasForeignKey(d => d.CreatedBy)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(d => d.UpdatedByUser)
                      .WithMany()
                      .HasForeignKey(d => d.UpdatedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.DeletedByUser)
                      .WithMany()
                      .HasForeignKey(d => d.DeletedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(d => d.Name);
                entity.HasIndex(d => d.Code).IsUnique();
                entity.HasIndex(d => d.Status);
                entity.HasIndex(d => d.IsActive);
            });


            // Configure new entities
            ConfigureSkillEntity(modelBuilder);
            ConfigureEquipmentEntity(modelBuilder);
            ConfigureWorkOrderTypeEntity(modelBuilder);
            ConfigureZoneEntity(modelBuilder);
            ConfigureRouteEntity(modelBuilder);
            ConfigureRouteStopEntity(modelBuilder);
            ConfigureAssignmentTemplateEntity(modelBuilder);
            ConfigureJunctionEntities(modelBuilder);
            ConfigureInventoryManagementEntities(modelBuilder);
            ConfigureBillingTemplateEntities(modelBuilder);
            ConfigureRateTableEntities(modelBuilder);
            ConfigureBillingScheduleEntities(modelBuilder);
        }

        private void ConfigureSkillEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Category).HasMaxLength(50);
                entity.Property(s => s.Level).HasMaxLength(20);
                entity.HasIndex(s => s.Name);
                entity.HasIndex(s => s.Category);
            });
        }

        private void ConfigureEquipmentEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Category).HasConversion<string>();
                entity.Property(e => e.Status).HasConversion<string>();
                entity.Property(e => e.SerialNumber).HasMaxLength(100);
                entity.HasIndex(e => e.Name);
                entity.HasIndex(e => e.Category);
                entity.HasIndex(e => e.SerialNumber);
            });
        }

        private void ConfigureWorkOrderTypeEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkOrderType>(entity =>
            {
                entity.HasKey(wot => wot.Id);
                entity.Property(wot => wot.Name).IsRequired().HasMaxLength(100);
                entity.Property(wot => wot.Category).HasMaxLength(50);
                entity.Property(wot => wot.Priority).IsRequired().HasMaxLength(20);
                entity.HasIndex(wot => wot.Name);
                entity.HasIndex(wot => wot.Category);
            });
        }

        private void ConfigureZoneEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zone>(entity =>
            {
                entity.HasKey(z => z.Id);
                entity.Property(z => z.Name).IsRequired().HasMaxLength(100);
                entity.Property(z => z.Type).HasMaxLength(50);
                entity.Property(z => z.Status).HasMaxLength(20);
                entity.HasIndex(z => z.Name);
                entity.HasIndex(z => z.Type);
            });
        }

        private void ConfigureRouteEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Status).IsRequired().HasMaxLength(50);
                entity.Property(r => r.TotalDistance).HasPrecision(10, 2);
                entity.Property(r => r.TotalTime).HasPrecision(10, 2);
                entity.Property(r => r.Efficiency).HasPrecision(5, 2);

                entity.HasOne(r => r.Driver)
                      .WithMany(u => u.Routes)
                      .HasForeignKey(r => r.DriverId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(r => r.RouteDate);
                entity.HasIndex(r => r.Status);
            });
        }

        private void ConfigureRouteStopEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RouteStop>(entity =>
            {
                entity.HasKey(rs => rs.Id);
                entity.Property(rs => rs.Status).HasMaxLength(50);
                entity.Property(rs => rs.EstimatedDuration).HasPrecision(10, 2);
                entity.Property(rs => rs.TravelTime).HasPrecision(10, 2);
                entity.Property(rs => rs.Distance).HasPrecision(10, 2);

                entity.HasOne(rs => rs.Route)
                      .WithMany(r => r.RouteStops)
                      .HasForeignKey(rs => rs.RouteId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rs => rs.WorkOrder)
                      .WithMany(wo => wo.RouteStops)
                      .HasForeignKey(rs => rs.WorkOrderId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(rs => new { rs.RouteId, rs.SequenceNumber });
            });
        }

        private void ConfigureAssignmentTemplateEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignmentTemplate>(entity =>
            {
                entity.HasKey(at => at.Id);
                entity.Property(at => at.Name).IsRequired().HasMaxLength(200);
                entity.Property(at => at.StartTime).IsRequired();
                entity.Property(at => at.EndTime).IsRequired();
                entity.HasIndex(at => at.Name);
                entity.HasIndex(at => at.IsActive);
            });
        }

        private void ConfigureJunctionEntities(ModelBuilder modelBuilder)
        {
            // TechnicianSkill
            modelBuilder.Entity<TechnicianSkill>(entity =>
            {
                entity.HasKey(ts => ts.Id);
                entity.Property(ts => ts.Level).HasMaxLength(20);
                entity.Property(ts => ts.CertificationNumber).HasMaxLength(200);

                entity.HasOne(ts => ts.Technician)
                      .WithMany(u => u.TechnicianSkills)
                      .HasForeignKey(ts => ts.TechnicianId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ts => ts.Skill)
                      .WithMany(s => s.TechnicianSkills)
                      .HasForeignKey(ts => ts.SkillId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(ts => new { ts.TechnicianId, ts.SkillId }).IsUnique();
            });

            // TechnicianEquipment
            modelBuilder.Entity<TechnicianEquipment>(entity =>
            {
                entity.HasKey(te => te.Id);
                entity.Property(te => te.Status).HasMaxLength(50);

                entity.HasOne(te => te.Technician)
                      .WithMany(u => u.TechnicianEquipments)
                      .HasForeignKey(te => te.TechnicianId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(te => te.Equipment)
                      .WithMany(e => e.TechnicianEquipments)
                      .HasForeignKey(te => te.EquipmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // TechnicianZone
            modelBuilder.Entity<TechnicianZone>(entity =>
            {
                entity.HasKey(tz => tz.Id);
                entity.Property(tz => tz.Status).HasMaxLength(50);

                entity.HasOne(tz => tz.Technician)
                      .WithMany(u => u.TechnicianZones)
                      .HasForeignKey(tz => tz.TechnicianId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tz => tz.Zone)
                      .WithMany(z => z.TechnicianZones)
                      .HasForeignKey(tz => tz.ZoneId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // WorkOrderSkillRequirement
            modelBuilder.Entity<WorkOrderSkillRequirement>(entity =>
            {
                entity.HasKey(wosr => wosr.Id);
                entity.Property(wosr => wosr.RequiredLevel).HasMaxLength(20);

                entity.HasOne(wosr => wosr.WorkOrder)
                      .WithMany(wo => wo.WorkOrderSkillRequirements)
                      .HasForeignKey(wosr => wosr.WorkOrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(wosr => wosr.Skill)
                      .WithMany(s => s.WorkOrderSkillRequirements)
                      .HasForeignKey(wosr => wosr.SkillId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // WorkOrderEquipmentRequirement
            modelBuilder.Entity<WorkOrderEquipmentRequirement>(entity =>
            {
                entity.HasKey(woer => woer.Id);

                entity.HasOne(woer => woer.WorkOrder)
                      .WithMany(wo => wo.WorkOrderEquipmentRequirements)
                      .HasForeignKey(woer => woer.WorkOrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(woer => woer.Equipment)
                      .WithMany(e => e.WorkOrderEquipmentRequirements)
                      .HasForeignKey(woer => woer.EquipmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // WorkOrderZone
            modelBuilder.Entity<WorkOrderZone>(entity =>
            {
                entity.HasKey(woz => woz.Id);
                entity.Property(woz => woz.Status).HasMaxLength(50);

                entity.HasOne(woz => woz.WorkOrder)
                      .WithMany(wo => wo.WorkOrderZones)
                      .HasForeignKey(woz => woz.WorkOrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(woz => woz.Zone)
                      .WithMany(z => z.WorkOrderZones)
                      .HasForeignKey(woz => woz.ZoneId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // WorkOrderTypeSkillRequirement
            modelBuilder.Entity<WorkOrderTypeSkillRequirement>(entity =>
            {
                entity.HasKey(wotsr => wotsr.Id);
                entity.Property(wotsr => wotsr.RequiredLevel).HasMaxLength(20);

                entity.HasOne(wotsr => wotsr.WorkOrderType)
                      .WithMany(wot => wot.WorkOrderTypeSkillRequirements)
                      .HasForeignKey(wotsr => wotsr.WorkOrderTypeId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(wotsr => wotsr.Skill)
                      .WithMany()
                      .HasForeignKey(wotsr => wotsr.SkillId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // WorkOrderTypeEquipmentRequirement
            modelBuilder.Entity<WorkOrderTypeEquipmentRequirement>(entity =>
            {
                entity.HasKey(woter => woter.Id);

                entity.HasOne(woter => woter.WorkOrderType)
                      .WithMany(wot => wot.WorkOrderTypeEquipmentRequirements)
                      .HasForeignKey(woter => woter.WorkOrderTypeId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(woter => woter.Equipment)
                      .WithMany()
                      .HasForeignKey(woter => woter.EquipmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // AssignmentTemplateWorkType
            modelBuilder.Entity<AssignmentTemplateWorkType>(entity =>
            {
                entity.HasKey(atwt => atwt.Id);

                entity.HasOne(atwt => atwt.AssignmentTemplate)
                      .WithMany(at => at.AssignmentTemplateWorkTypes)
                      .HasForeignKey(atwt => atwt.AssignmentTemplateId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(atwt => atwt.WorkOrderType)
                      .WithMany()
                      .HasForeignKey(atwt => atwt.WorkOrderTypeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // AssignmentTemplateZone
            modelBuilder.Entity<AssignmentTemplateZone>(entity =>
            {
                entity.HasKey(atz => atz.Id);

                entity.HasOne(atz => atz.AssignmentTemplate)
                      .WithMany(at => at.AssignmentTemplateZones)
                      .HasForeignKey(atz => atz.AssignmentTemplateId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(atz => atz.Zone)
                      .WithMany()
                      .HasForeignKey(atz => atz.ZoneId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // AssignmentTemplateSkill
            modelBuilder.Entity<AssignmentTemplateSkill>(entity =>
            {
                entity.HasKey(ats => ats.Id);
                entity.Property(ats => ats.RequiredLevel).HasMaxLength(20);

                entity.HasOne(ats => ats.AssignmentTemplate)
                      .WithMany(at => at.AssignmentTemplateSkills)
                      .HasForeignKey(ats => ats.AssignmentTemplateId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ats => ats.Skill)
                      .WithMany()
                      .HasForeignKey(ats => ats.SkillId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // AssignmentTemplateTechnician
            modelBuilder.Entity<AssignmentTemplateTechnician>(entity =>
            {
                entity.HasKey(att => att.Id);
                entity.Property(att => att.Status).HasMaxLength(50);

                entity.HasOne(att => att.AssignmentTemplate)
                      .WithMany(at => at.AssignmentTemplateTechnicians)
                      .HasForeignKey(att => att.AssignmentTemplateId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(att => att.Technician)
                      .WithMany(u => u.AssignmentTemplateTechnicians)
                      .HasForeignKey(att => att.TechnicianId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureInventoryManagementEntities(ModelBuilder modelBuilder)
        {
            // Location entity configuration
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Name).IsRequired().HasMaxLength(100);
                entity.Property(l => l.Type).IsRequired().HasConversion<string>();
                entity.Property(l => l.Status).IsRequired().HasConversion<string>();
                entity.Property(l => l.Code).HasMaxLength(100);

                entity.HasOne(l => l.Manager)
                      .WithMany()
                      .HasForeignKey(l => l.ManagerId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(l => l.ParentLocation)
                      .WithMany(l => l.SubLocations)
                      .HasForeignKey(l => l.ParentLocationId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(l => l.Code).IsUnique();
                entity.HasIndex(l => l.Type);
                entity.HasIndex(l => l.Status);
            });

            // InventoryTransaction entity configuration
            modelBuilder.Entity<InventoryTransaction>(entity =>
            {
                entity.HasKey(it => it.Id);
                entity.Property(it => it.TransactionType).IsRequired().HasConversion<string>();
                entity.Property(it => it.Status).IsRequired().HasConversion<string>();
                entity.Property(it => it.Reference).HasMaxLength(200);

                entity.HasOne(it => it.Inventory)
                      .WithMany(i => i.InventoryTransactions)
                      .HasForeignKey(it => it.InventoryId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(it => it.FromLocation)
                      .WithMany(l => l.FromTransactions)
                      .HasForeignKey(it => it.FromLocationId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(it => it.ToLocation)
                      .WithMany(l => l.ToTransactions)
                      .HasForeignKey(it => it.ToLocationId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(it => it.User)
                      .WithMany()
                      .HasForeignKey(it => it.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(it => it.TransactionDate);
                entity.HasIndex(it => it.TransactionType);
                entity.HasIndex(it => it.Status);
            });

            // CycleCount entity configuration
            modelBuilder.Entity<CycleCount>(entity =>
            {
                entity.HasKey(cc => cc.Id);
                entity.Property(cc => cc.CountType).IsRequired().HasConversion<string>();
                entity.Property(cc => cc.Status).IsRequired().HasConversion<string>();

                entity.HasOne(cc => cc.Location)
                      .WithMany()
                      .HasForeignKey(cc => cc.LocationId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(cc => cc.CountedByUser)
                      .WithMany()
                      .HasForeignKey(cc => cc.CountedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(cc => cc.PlannedDate);
                entity.HasIndex(cc => cc.Status);
            });

            // CycleCountItem entity configuration
            modelBuilder.Entity<CycleCountItem>(entity =>
            {
                entity.HasKey(cci => cci.Id);
                entity.Property(cci => cci.Status).IsRequired().HasConversion<string>();
                
                // Ignore computed properties
                entity.Ignore(cci => cci.Variance);
                entity.Ignore(cci => cci.IsVariance);

                entity.HasOne(cci => cci.CycleCount)
                      .WithMany(cc => cc.CycleCountItems)
                      .HasForeignKey(cci => cci.CycleCountId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cci => cci.Inventory)
                      .WithMany(i => i.CycleCountItems)
                      .HasForeignKey(cci => cci.InventoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Asset entity configuration
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.SerialNumber).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Model).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Status).IsRequired().HasConversion<string>();
                entity.Property(a => a.DispositionStatus).IsRequired().HasConversion<string>();
                entity.Property(a => a.Category).HasConversion<string>();

                entity.HasOne(a => a.CurrentLocation)
                      .WithMany(l => l.Assets)
                      .HasForeignKey(a => a.CurrentLocationId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(a => a.WorkOrder)
                      .WithMany()
                      .HasForeignKey(a => a.WorkOrderId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasIndex(a => a.SerialNumber).IsUnique();
                entity.HasIndex(a => a.Model);
                entity.HasIndex(a => a.Status);
                entity.HasIndex(a => a.Category);
            });

            // AssetHistory entity configuration
            modelBuilder.Entity<AssetHistory>(entity =>
            {
                entity.HasKey(ah => ah.Id);
                entity.Property(ah => ah.Action).IsRequired().HasConversion<string>();
                entity.Property(ah => ah.Status).IsRequired().HasConversion<string>();

                entity.HasOne(ah => ah.Asset)
                      .WithMany(a => a.AssetHistories)
                      .HasForeignKey(ah => ah.AssetId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ah => ah.FromLocation)
                      .WithMany()
                      .HasForeignKey(ah => ah.FromLocationId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(ah => ah.ToLocation)
                      .WithMany()
                      .HasForeignKey(ah => ah.ToLocationId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(ah => ah.User)
                      .WithMany()
                      .HasForeignKey(ah => ah.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ah => ah.WorkOrder)
                      .WithMany()
                      .HasForeignKey(ah => ah.WorkOrderId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasIndex(ah => ah.Action);
                entity.HasIndex(ah => ah.Status);
            });

            // JobKit entity configuration
            modelBuilder.Entity<JobKit>(entity =>
            {
                entity.HasKey(jk => jk.Id);
                entity.Property(jk => jk.Name).IsRequired().HasMaxLength(100);
                entity.Property(jk => jk.Status).IsRequired().HasConversion<string>();
                entity.Property(jk => jk.Availability).IsRequired().HasConversion<string>();

                entity.HasIndex(jk => jk.Name);
                entity.HasIndex(jk => jk.Status);
                entity.HasIndex(jk => jk.JobType);
            });

            // JobKitItem entity configuration
            modelBuilder.Entity<JobKitItem>(entity =>
            {
                entity.HasKey(jki => jki.Id);
                entity.Property(jki => jki.Status).IsRequired().HasConversion<string>();

                entity.HasOne(jki => jki.JobKit)
                      .WithMany(jk => jk.JobKitItems)
                      .HasForeignKey(jki => jki.JobKitId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(jki => jki.Inventory)
                      .WithMany(i => i.JobKitItems)
                      .HasForeignKey(jki => jki.InventoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(jki => jki.IsOptional);
            });

            // Vendor entity configuration
            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Name).IsRequired().HasMaxLength(100);
                entity.Property(v => v.Status).IsRequired().HasConversion<string>();

                entity.HasIndex(v => v.Name);
                entity.HasIndex(v => v.Status);
            });

            // InventoryDisposition entity configuration
            modelBuilder.Entity<InventoryDisposition>(entity =>
            {
                entity.HasKey(id => id.Id);
                entity.Property(id => id.DispositionType).IsRequired().HasConversion<string>();
                entity.Property(id => id.Status).IsRequired().HasConversion<string>();

                entity.HasOne(id => id.Inventory)
                      .WithMany(i => i.InventoryDispositions)
                      .HasForeignKey(id => id.InventoryId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(id => id.DisposedByUser)
                      .WithMany()
                      .HasForeignKey(id => id.DisposedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(id => id.Vendor)
                      .WithMany(v => v.InventoryDispositions)
                      .HasForeignKey(id => id.VendorId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasIndex(id => id.DispositionType);
                entity.HasIndex(id => id.Status);
            });

            // ValidationIssue entity configuration
            modelBuilder.Entity<ValidationIssue>(entity =>
            {
                entity.HasKey(vi => vi.Id);
                entity.Property(vi => vi.Type).IsRequired().HasConversion<string>();
                entity.Property(vi => vi.Code).IsRequired().HasMaxLength(100);
                entity.Property(vi => vi.Message).IsRequired().HasMaxLength(500);
                entity.Property(vi => vi.Severity).IsRequired().HasConversion<string>();

                entity.HasOne(vi => vi.Asset)
                      .WithMany(a => a.ValidationIssues)
                      .HasForeignKey(vi => vi.AssetId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(vi => vi.ResolvedByUser)
                      .WithMany()
                      .HasForeignKey(vi => vi.ResolvedByUserId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasIndex(vi => vi.Type);
                entity.HasIndex(vi => vi.IsResolved);
                entity.HasIndex(vi => vi.Severity);
            });

            // InventoryLocation entity configuration
            modelBuilder.Entity<InventoryLocation>(entity =>
            {
                entity.HasKey(il => il.Id);
                entity.Property(il => il.Status).IsRequired().HasConversion<string>();

                entity.HasOne(il => il.Inventory)
                      .WithMany(i => i.InventoryLocations)
                      .HasForeignKey(il => il.InventoryId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(il => il.Location)
                      .WithMany(l => l.InventoryLocations)
                      .HasForeignKey(il => il.LocationId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(il => new { il.InventoryId, il.LocationId }).IsUnique();
                entity.HasIndex(il => il.Status);
            });

            // ScanLog entity configuration
            modelBuilder.Entity<ScanLog>(entity =>
            {
                entity.HasKey(sl => sl.Id);
                entity.Property(sl => sl.ScannedCode).IsRequired().HasMaxLength(100);
                entity.Property(sl => sl.ScanType).IsRequired().HasConversion<string>();
                entity.Property(sl => sl.Result).HasMaxLength(50);

                entity.HasOne(sl => sl.Inventory)
                      .WithMany()
                      .HasForeignKey(sl => sl.InventoryId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(sl => sl.Asset)
                      .WithMany()
                      .HasForeignKey(sl => sl.AssetId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(sl => sl.ScannedByUser)
                      .WithMany()
                      .HasForeignKey(sl => sl.ScannedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(sl => sl.ScannedCode);
                entity.HasIndex(sl => sl.ScanType);
                entity.HasIndex(sl => sl.Result);
            });
        }

        private void ConfigureBillingTemplateEntities(ModelBuilder modelBuilder)
        {
            // BillingTemplate entity configuration
            modelBuilder.Entity<BillingTemplate>(entity =>
            {
                entity.HasKey(bt => bt.Id);
                entity.Property(bt => bt.TemplateName).IsRequired().HasMaxLength(200);
                entity.Property(bt => bt.CustomerId).IsRequired().HasMaxLength(50);
                entity.Property(bt => bt.CustomerName).IsRequired().HasMaxLength(200);
                entity.Property(bt => bt.OutputFormat).IsRequired().HasMaxLength(20);
                entity.Property(bt => bt.DeliveryMethod).IsRequired().HasMaxLength(20);
                entity.Property(bt => bt.InvoiceType).IsRequired().HasMaxLength(50);
                entity.Property(bt => bt.FileNamingConvention).IsRequired().HasMaxLength(200);
                entity.Property(bt => bt.Description).HasMaxLength(1000);
                entity.Property(bt => bt.AdditionalSettings).HasColumnType("nvarchar(max)");

                entity.HasIndex(bt => bt.TemplateName);
                entity.HasIndex(bt => bt.CustomerId);
                entity.HasIndex(bt => bt.IsActive);
            });

            // BillingTemplateFieldOrder entity configuration
            modelBuilder.Entity<BillingTemplateFieldOrder>(entity =>
            {
                entity.HasKey(btfo => btfo.Id);
                entity.Property(btfo => btfo.FieldName).IsRequired().HasMaxLength(100);
                entity.Property(btfo => btfo.DisplayLabel).HasMaxLength(200);
                entity.Property(btfo => btfo.FieldType).HasMaxLength(50);
                entity.Property(btfo => btfo.FieldSettings).HasColumnType("nvarchar(max)");

                entity.HasOne(btfo => btfo.BillingTemplate)
                      .WithMany(bt => bt.FieldOrders)
                      .HasForeignKey(btfo => btfo.BillingTemplateId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(btfo => new { btfo.BillingTemplateId, btfo.DisplayOrder });
                entity.HasIndex(btfo => btfo.FieldName);
            });
        }

        private void ConfigureRateTableEntities(ModelBuilder modelBuilder)
        {
            // RateTable entity configuration
            modelBuilder.Entity<RateTable>(entity =>
            {
                entity.HasKey(rt => rt.Id);
                entity.Property(rt => rt.RateTableName).IsRequired().HasMaxLength(200);
                entity.Property(rt => rt.RateType).IsRequired().HasMaxLength(50);
                entity.Property(rt => rt.Description).HasMaxLength(1000);
                entity.Property(rt => rt.BaseRate).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(rt => rt.EffectiveStartDate).IsRequired();
                entity.Property(rt => rt.EffectiveEndDate).IsRequired();
                entity.Property(rt => rt.Currency).HasMaxLength(50);
                entity.Property(rt => rt.Category).HasMaxLength(100);
                entity.Property(rt => rt.RateRules).HasColumnType("nvarchar(max)");
                entity.Property(rt => rt.AdditionalSettings).HasColumnType("nvarchar(max)");

                entity.HasIndex(rt => rt.RateTableName);
                entity.HasIndex(rt => rt.RateType);
                entity.HasIndex(rt => rt.IsActive);
                entity.HasIndex(rt => rt.EffectiveStartDate);
                entity.HasIndex(rt => rt.EffectiveEndDate);
            });

            // RateTableItem entity configuration
            modelBuilder.Entity<RateTableItem>(entity =>
            {
                entity.HasKey(rti => rti.Id);
                entity.Property(rti => rti.ItemName).IsRequired().HasMaxLength(200);
                entity.Property(rti => rti.Description).HasMaxLength(500);
                entity.Property(rti => rti.Rate).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(rti => rti.Unit).HasMaxLength(50);
                entity.Property(rti => rti.Category).HasMaxLength(100);
                entity.Property(rti => rti.SkillLevel).HasMaxLength(50);
                entity.Property(rti => rti.WorkType).HasMaxLength(100);
                entity.Property(rti => rti.Conditions).HasColumnType("nvarchar(max)");
                entity.Property(rti => rti.AdditionalSettings).HasColumnType("nvarchar(max)");

                entity.HasOne(rti => rti.RateTable)
                      .WithMany(rt => rt.RateTableItems)
                      .HasForeignKey(rti => rti.RateTableId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(rti => new { rti.RateTableId, rti.DisplayOrder });
                entity.HasIndex(rti => rti.ItemName);
                entity.HasIndex(rti => rti.Category);
                entity.HasIndex(rti => rti.IsActive);
            });
        }

        private void ConfigureBillingScheduleEntities(ModelBuilder modelBuilder)
        {
            // BillingSchedule entity configuration
            modelBuilder.Entity<BillingSchedule>(entity =>
            {
                entity.HasKey(bs => bs.Id);
                entity.Property(bs => bs.ScheduleName).IsRequired().HasMaxLength(200);
                entity.Property(bs => bs.Frequency).IsRequired().HasMaxLength(50);
                entity.Property(bs => bs.Time).IsRequired();
                entity.Property(bs => bs.DayOfWeekName).HasMaxLength(100);
                entity.Property(bs => bs.Description).HasMaxLength(1000);
                entity.Property(bs => bs.Status).HasMaxLength(50);
                entity.Property(bs => bs.LastRunStatus).HasMaxLength(500);
                entity.Property(bs => bs.LastRunMessage).HasMaxLength(1000);
                entity.Property(bs => bs.ScheduleSettings).HasColumnType("nvarchar(max)");
                entity.Property(bs => bs.NotificationSettings).HasColumnType("nvarchar(max)");

                entity.HasIndex(bs => bs.ScheduleName);
                entity.HasIndex(bs => bs.Frequency);
                entity.HasIndex(bs => bs.IsActive);
                entity.HasIndex(bs => bs.Status);
                entity.HasIndex(bs => bs.NextRunDate);
            });

            // BillingScheduleTemplate entity configuration
            modelBuilder.Entity<BillingScheduleTemplate>(entity =>
            {
                entity.HasKey(bst => bst.Id);
                entity.Property(bst => bst.Notes).HasMaxLength(500);
                entity.Property(bst => bst.TemplateSettings).HasColumnType("nvarchar(max)");

                entity.HasOne(bst => bst.BillingSchedule)
                      .WithMany(bs => bs.BillingScheduleTemplates)
                      .HasForeignKey(bst => bst.BillingScheduleId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(bst => bst.BillingTemplate)
                      .WithMany()
                      .HasForeignKey(bst => bst.BillingTemplateId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(bst => new { bst.BillingScheduleId, bst.DisplayOrder });
                entity.HasIndex(bst => bst.IsActive);
            });

            // BillingScheduleRun entity configuration
            modelBuilder.Entity<BillingScheduleRun>(entity =>
            {
                entity.HasKey(bsr => bsr.Id);
                entity.Property(bsr => bsr.RunDate).IsRequired();
                entity.Property(bsr => bsr.ScheduledRunDate).IsRequired();
                entity.Property(bsr => bsr.Status).IsRequired().HasMaxLength(50);
                entity.Property(bsr => bsr.ErrorMessage).HasMaxLength(1000);
                entity.Property(bsr => bsr.Summary).HasMaxLength(2000);
                entity.Property(bsr => bsr.RunDetails).HasColumnType("nvarchar(max)");
                entity.Property(bsr => bsr.ErrorDetails).HasColumnType("nvarchar(max)");

                entity.HasOne(bsr => bsr.BillingSchedule)
                      .WithMany(bs => bs.BillingScheduleRuns)
                      .HasForeignKey(bsr => bsr.BillingScheduleId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(bsr => bsr.RunDate);
                entity.HasIndex(bsr => bsr.ScheduledRunDate);
                entity.HasIndex(bsr => bsr.Status);
            });
        }
    }
}
