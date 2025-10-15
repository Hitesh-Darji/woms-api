using System.ComponentModel.DataAnnotations;

namespace WOMS.Application.Features.Forms.DTOs
{
    public class FormTemplateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = "draft";
        public int Version { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public List<FormSectionDto> Sections { get; set; } = new List<FormSectionDto>();
    }

    public class FormSectionDto
    {
        public Guid Id { get; set; }
        public Guid FormTemplateId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int OrderIndex { get; set; }
        public bool IsRequired { get; set; } = false;
        public bool IsCollapsible { get; set; } = false;
        public bool IsCollapsed { get; set; } = false;
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public List<FormFieldDto> Fields { get; set; } = new List<FormFieldDto>();
    }

    public class FormFieldDto
    {
        public Guid Id { get; set; }
        public Guid FormSectionId { get; set; }
        public string FieldType { get; set; } = "text";
        public string Label { get; set; } = string.Empty;
        public string? Placeholder { get; set; }
        public string? HelpText { get; set; }
        public bool IsRequired { get; set; } = false;
        public bool IsReadOnly { get; set; } = false;
        public bool IsVisible { get; set; } = true;
        public int OrderIndex { get; set; }
        public string? ValidationRules { get; set; }
        public string? Options { get; set; }
        public string? DefaultValue { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public string? Pattern { get; set; }
        public decimal? Step { get; set; }
        public int? Rows { get; set; }
        public int? Columns { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

