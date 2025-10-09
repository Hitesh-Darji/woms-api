using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class View : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // e.g., "Default View", "Custom View"

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string EntityType { get; set; } = string.Empty; // e.g., "WorkOrder", "User", etc.

        public bool IsDefault { get; set; } = false;

        public bool IsPublic { get; set; } = true; 

        [ForeignKey(nameof(CreatedBy))]
        public virtual ApplicationUser? CreatedByUser { get; set; }

        // Navigation properties
        public virtual ICollection<ViewColumn> Columns { get; set; } = new List<ViewColumn>();
        public virtual ICollection<ViewFilter> Filters { get; set; } = new List<ViewFilter>();
    }
}
