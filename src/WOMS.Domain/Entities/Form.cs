using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("Form")]
    public class Form : BaseEntity
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Fields { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Settings { get; set; } 

        public bool Locked { get; set; } = false;

        // Navigation properties
        public virtual ICollection<WorkflowForm> WorkflowForms { get; set; } = new List<WorkflowForm>();
    }
}
