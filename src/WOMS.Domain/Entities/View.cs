using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class View : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string SelectedColumns { get; set; } = string.Empty; // JSON array of selected column names

        [ForeignKey(nameof(CreatedBy))]
        public virtual ApplicationUser? CreatedByUser { get; set; }
    }
}
