using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowStatus")]
    public class WorkflowStatus : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(7)] 
        public string Color { get; set; } = "#3b82f6";

        [Required]
        public int Order { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        // Navigation properties - transitions are workflow-specific, not status-specific
    }
}