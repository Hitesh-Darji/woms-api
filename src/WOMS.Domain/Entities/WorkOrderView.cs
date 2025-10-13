using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkOrderView")]
    public class WorkOrderView : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string UserId { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string? Filters { get; set; } // JSON as string

        [MaxLength(100)]
        public string? SortBy { get; set; }

        [MaxLength(10)]
        public string? SortOrder { get; set; } // asc, desc

        [Required]
        public bool IsDefault { get; set; } = false;
    }
}
