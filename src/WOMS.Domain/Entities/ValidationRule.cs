using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("ValidationRule")]
    public class ValidationRule : BaseEntity
    {
        [Required]
        public Guid FormFieldId { get; set; }

        [ForeignKey(nameof(FormFieldId))]
        public virtual FormField FormField { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string RuleType { get; set; } = string.Empty; // required, min_length, max_length, min_value, max_value, pattern, email, phone, date_range, file_type, file_size, custom

        [Column(TypeName = "nvarchar(max)")]
        public string? RuleValue { get; set; }

        [Required]
        [MaxLength(255)]
        public string ErrorMessage { get; set; } = string.Empty;

        [Required]
        public int OrderIndex { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
