using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class BillingTemplateFieldOrder : BaseEntity
    {
        [Required]
        public Guid BillingTemplateId { get; set; }

        [ForeignKey(nameof(BillingTemplateId))]
        public virtual BillingTemplate BillingTemplate { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string FieldName { get; set; } = string.Empty; // Work Order Id, Description, Quantity, Rate, Total, Location, Category, Work Type

        [Required]
        public int DisplayOrder { get; set; } = 0; // Order in which field appears

        [Required]
        public bool IsEnabled { get; set; } = true; // Whether field is included in billing

        [MaxLength(200)]
        public string? DisplayLabel { get; set; } // Custom label for the field

        [MaxLength(50)]
        public string? FieldType { get; set; } // Text, Number, Date, Currency, etc.

        [Column(TypeName = "json")]
        public string? FieldSettings { get; set; } // JSON for field-specific settings like formatting
    }
}
