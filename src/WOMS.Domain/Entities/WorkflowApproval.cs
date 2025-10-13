using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowApproval")]
    public class WorkflowApproval : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string? ApproverRoles { get; set; } // JSON array as string

        [Column(TypeName = "nvarchar(max)")]
        public string? ApproverUsers { get; set; } // JSON array as string

        [Required]
        public bool IsSequential { get; set; } = false;

        [Required]
        public bool IsParallel { get; set; } = false;

        public int? Deadline { get; set; } // hours

        [Required]
        public int OrderIndex { get; set; }
    }
}
