using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowExecutionLog")]
    public class WorkflowExecutionLog : BaseEntity
    {
        [Required]
        public Guid InstanceId { get; set; }

        [ForeignKey(nameof(InstanceId))]
        public virtual WorkflowInstance Instance { get; set; } = null!;

        public Guid? NodeId { get; set; }

        [ForeignKey(nameof(NodeId))]
        public virtual WorkflowNode? Node { get; set; }

        [Required]
        [MaxLength(100)]
        public string Action { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Result { get; set; } = string.Empty; // success, failure, skipped

        [Column(TypeName = "nvarchar(max)")]
        public string? Message { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Data { get; set; } // JSON as string

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [MaxLength(255)]
        public string? UserId { get; set; }
    }
}
