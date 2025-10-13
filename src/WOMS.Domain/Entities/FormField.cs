using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("FormField")]
    public class FormField : BaseEntity
    {
        [Required]
        public Guid FormSectionId { get; set; }

        [ForeignKey(nameof(FormSectionId))]
        public virtual FormSection FormSection { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string FieldType { get; set; } = "text"; // text, number, email, phone, date, time, datetime, textarea, select, multiselect, checkbox, radio, file, signature, location, barcode, rating, slider

        [Required]
        [MaxLength(255)]
        public string Label { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Placeholder { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? HelpText { get; set; }

        [Required]
        public bool IsRequired { get; set; } = false;

        [Required]
        public bool IsReadOnly { get; set; } = false;

        [Required]
        public bool IsVisible { get; set; } = true;

        [Required]
        public int OrderIndex { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? ValidationRules { get; set; } // JSON as string

        [Column(TypeName = "nvarchar(max)")]
        public string? Options { get; set; } // JSON array for select/radio options

        [Column(TypeName = "nvarchar(max)")]
        public string? DefaultValue { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal? MinValue { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal? MaxValue { get; set; }

        public int? MinLength { get; set; }

        public int? MaxLength { get; set; }

        [MaxLength(255)]
        public string? Pattern { get; set; } // Regex pattern

        [Column(TypeName = "decimal(15,2)")]
        public decimal? Step { get; set; } // For number fields

        public int? Rows { get; set; } // For textarea

        public int? Columns { get; set; } // For layout

        // Navigation properties
        public virtual ICollection<ValidationRule> ValidationRulesList { get; set; } = new List<ValidationRule>();
        public virtual ICollection<FormAttachment> FormAttachments { get; set; } = new List<FormAttachment>();
        public virtual ICollection<FormSignature> FormSignatures { get; set; } = new List<FormSignature>();
        public virtual ICollection<FormGeolocation> FormGeolocations { get; set; } = new List<FormGeolocation>();
    }
}
