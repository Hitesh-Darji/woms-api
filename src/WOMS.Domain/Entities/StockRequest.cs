using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("StockRequest")]
    public class StockRequest : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string RequesterId { get; set; } = string.Empty;

        [Required]
        public Guid FromLocationId { get; set; }

        [ForeignKey(nameof(FromLocationId))]
        public virtual Location FromLocation { get; set; } = null!;

        [Required]
        public Guid ToLocationId { get; set; }

        [ForeignKey(nameof(ToLocationId))]
        public virtual Location ToLocation { get; set; } = null!;

        [Required]
        public StockRequestStatus Status { get; set; } = StockRequestStatus.Pending;

        [Required]
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        [MaxLength(255)]
        public string? ApprovedBy { get; set; }

        public DateTime? ApprovalDate { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Notes { get; set; }

        public Guid? WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder? WorkOrder { get; set; }

        // Navigation properties
        public virtual ICollection<RequestItem> RequestItems { get; set; } = new List<RequestItem>();
    }
}
