using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    public class InventoryDisposition : BaseEntity
    {
        [Required]
        public Guid InventoryId { get; set; }

        [ForeignKey(nameof(InventoryId))]
        public virtual Inventory Inventory { get; set; } = null!;

        [Required]
        public DispositionType DispositionType { get; set; } = DispositionType.Reinstall;

        [MaxLength(1000)]
        public string? Notes { get; set; }

        [Required]
        public DateTime DispositionDate { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid DisposedByUserId { get; set; }

        [ForeignKey(nameof(DisposedByUserId))]
        public virtual ApplicationUser DisposedByUser { get; set; } = null!;

        public Guid? VendorId { get; set; }

        [ForeignKey(nameof(VendorId))]
        public virtual Vendor? Vendor { get; set; }

        [Required]
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

        [MaxLength(200)]
        public string? Reference { get; set; } // RMA Number, Return Authorization, etc.

        public DateTime? CompletedDate { get; set; }

        [MaxLength(500)]
        public string? CompletionNotes { get; set; }
    }
}
