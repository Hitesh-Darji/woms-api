using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("WorkOrder")]
    public class WorkOrder : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Customer { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? CustomerContact { get; set; }

        [Required]
        public Enums.WorkOrderType Type { get; set; } = Enums.WorkOrderType.MeterInstallation;

        [Required]
        public WorkOrderPriority Priority { get; set; } = WorkOrderPriority.Medium;

        [Required]
        public WorkOrderStatus Status { get; set; } = WorkOrderStatus.Pending;

        [MaxLength(255)]
        public string? Assignee { get; set; }

        [MaxLength(255)]
        public string? Location { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Address { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? ActualHours { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal? Cost { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Tags { get; set; } // JSON array as string

        [MaxLength(255)]
        public string? Equipment { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Notes { get; set; }

        [MaxLength(255)]
        public string? Utility { get; set; }

        [MaxLength(100)]
        public string? Make { get; set; }

        [MaxLength(100)]
        public string? Model { get; set; }

        [MaxLength(50)]
        public string? Size { get; set; }

        [MaxLength(255)]
        public string? ManagerTechnician { get; set; }

        // Additional properties for compatibility with existing DTOs
        [MaxLength(100)]
        public string WorkOrderNumber { get; set; } = string.Empty;

        public Guid? WorkflowId { get; set; }

        public Guid? FormTemplateId { get; set; }

        public Guid? BillingTemplateId { get; set; }

        // Navigation properties
        public virtual ICollection<WorkOrderColumn> WorkOrderColumns { get; set; } = new List<WorkOrderColumn>();
        public virtual ICollection<WorkOrderAssignment> WorkOrderAssignments { get; set; } = new List<WorkOrderAssignment>();
        public virtual ICollection<FormSubmission> FormSubmissions { get; set; } = new List<FormSubmission>();
        public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; } = new List<InvoiceLineItem>();
        public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
        public virtual ICollection<StockRequest> StockRequests { get; set; } = new List<StockRequest>();
        public virtual ICollection<AssetHistory> AssetHistories { get; set; } = new List<AssetHistory>();
        public virtual ICollection<WorkflowInstance> WorkflowInstances { get; set; } = new List<WorkflowInstance>();
    }
}