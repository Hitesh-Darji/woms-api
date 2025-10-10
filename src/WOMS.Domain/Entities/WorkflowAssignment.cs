using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowAssignment")]
    public class WorkflowAssignment : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string NodeId { get; set; } = string.Empty;

        [Required]
        public Guid AssigneeId { get; set; }

        [ForeignKey(nameof(AssigneeId))]
        public virtual ApplicationUser Assignee { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string AssigneeName { get; set; } = string.Empty;

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Priority { get; set; } = "medium"; // low, medium, high, urgent

        // Navigation properties
        [NotMapped]
        public virtual WorkflowNode? Node { get; set; }
    }
}
