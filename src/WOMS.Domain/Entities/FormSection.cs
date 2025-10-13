using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("FormSection")]
    public class FormSection : BaseEntity
    {
        [Required]
        public Guid FormTemplateId { get; set; }

        [ForeignKey(nameof(FormTemplateId))]
        public virtual FormTemplate FormTemplate { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [Required]
        public int OrderIndex { get; set; }

        [Required]
        public bool IsRequired { get; set; } = false;

        [Required]
        public bool IsCollapsible { get; set; } = false;

        [Required]
        public bool IsCollapsed { get; set; } = false;

        // Navigation properties
        public virtual ICollection<FormField> FormFields { get; set; } = new List<FormField>();
    }
}
