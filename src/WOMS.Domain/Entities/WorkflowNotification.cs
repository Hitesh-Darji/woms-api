using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowNotification")]
    public class WorkflowNotification : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string Type { get; set; } = "email"; // email, sms, in_app

        [Column(TypeName = "nvarchar(max)")]
        public string? Recipients { get; set; } // JSON array as string

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Template { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string? Triggers { get; set; } // JSON array as string

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public int OrderIndex { get; set; }
    }
}
