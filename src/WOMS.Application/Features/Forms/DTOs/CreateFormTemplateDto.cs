using System.ComponentModel.DataAnnotations;

namespace WOMS.Application.Features.Forms.DTOs
{
    public class CreateFormTemplateDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Status { get; set; } = "draft";

        public bool IsActive { get; set; } = true;

        public List<CreateFormSectionDto> Sections { get; set; } = new List<CreateFormSectionDto>();
    }

    public class CreateFormSectionDto
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public int OrderIndex { get; set; }

        public bool IsRequired { get; set; } = false;
        public bool IsCollapsible { get; set; } = false;
        public bool IsCollapsed { get; set; } = false;

        public List<CreateFormFieldDto> Fields { get; set; } = new List<CreateFormFieldDto>();
    }

    public class CreateFormFieldDto
    {
        [Required]
        [MaxLength(20)]
        public string FieldType { get; set; } = "text";

        [Required]
        [MaxLength(255)]
        public string Label { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Placeholder { get; set; }

        [MaxLength(1000)]
        public string? HelpText { get; set; }

        public bool IsRequired { get; set; } = false;
        public bool IsReadOnly { get; set; } = false;
        public bool IsVisible { get; set; } = true;

        [Required]
        public int OrderIndex { get; set; }

        [MaxLength(2000)]
        public string? ValidationRules { get; set; }

        [MaxLength(2000)]
        public string? Options { get; set; }

        [MaxLength(1000)]
        public string? DefaultValue { get; set; }

        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }

        [MaxLength(255)]
        public string? Pattern { get; set; }

        public decimal? Step { get; set; }
        public int? Rows { get; set; }
        public int? Columns { get; set; }
    }
}

