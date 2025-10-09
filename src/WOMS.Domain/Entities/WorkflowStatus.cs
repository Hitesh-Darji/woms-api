using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class WorkflowStatus : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string StatusId { get; set; } = string.Empty; // Unique identifier within the workflow

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // e.g., "Planned", "Dispatched", "In Progress"

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(7)]
        public string Color { get; set; } = string.Empty; // e.g., "#3b82f6" (hex color code)

        public bool IsInitial { get; set; } = false; // Indicates if this is the initial status

        public bool IsFinal { get; set; } = false; // Indicates if this is a final status

        [Required]
        public int SortOrder { get; set; } // The display order of the status

        // Navigation properties for transitions
        public virtual ICollection<WorkflowTransition> OutgoingTransitions { get; set; } = new List<WorkflowTransition>();
        public virtual ICollection<WorkflowTransition> IncomingTransitions { get; set; } = new List<WorkflowTransition>();
        public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
        public virtual ICollection<WorkflowProgress> WorkflowProgresses { get; set; } = new List<WorkflowProgress>();
    }
}
