using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowAction")]
    public class WorkflowAction : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string ActionId { get; set; } = string.Empty; // Unique identifier within the workflow

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty; // timestamp, assignment, notification, validation, status_change

        [Column(TypeName = "nvarchar(max)")]
        public string? Config { get; set; } // JSON configuration for the action
    }
}
