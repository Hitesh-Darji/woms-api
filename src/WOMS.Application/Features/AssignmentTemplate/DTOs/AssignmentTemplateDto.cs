using System.ComponentModel.DataAnnotations;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.AssignmentTemplate.DTOs
{
    public class AssignmentTemplateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public AssignmentTemplateStatus Status { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<DayOfWeekEnum> DaysOfWeek { get; set; } = new();
        public List<string> WorkTypes { get; set; } = new();
        public List<string> Zones { get; set; } = new();
        public List<string> PreferredTechnicians { get; set; } = new();
        public List<string> SkillsRequired { get; set; } = new();
        public List<string> AutoAssignmentRules { get; set; } = new();
        public int UsageCount { get; set; }
        public DateTime? LastUsed { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class CreateAssignmentTemplateRequest
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public AssignmentTemplateStatus Status { get; set; } = AssignmentTemplateStatus.Active;

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public List<DayOfWeekEnum> DaysOfWeek { get; set; } = new();

        [Required]
        public List<string> AutoAssignmentRules { get; set; } = new();
    }

    public class UpdateAssignmentTemplateRequest
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public AssignmentTemplateStatus Status { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public List<DayOfWeekEnum> DaysOfWeek { get; set; } = new();

        [Required]
        public List<string> WorkTypes { get; set; } = new();

        [Required]
        public List<string> Zones { get; set; } = new();

        [Required]
        public List<string> PreferredTechnicians { get; set; } = new();

        [Required]
        public List<string> SkillsRequired { get; set; } = new();

        [Required]
        public List<string> AutoAssignmentRules { get; set; } = new();
    }

    public class ToggleAssignmentTemplateStatusRequest
    {
        [Required]
        public AssignmentTemplateStatus Status { get; set; }
    }

    public class AssignmentTemplateListResponse
    {
        public List<AssignmentTemplateDto> Templates { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    public class CopyAssignmentTemplateRequest
    {
        [MaxLength(255)]
        public string? NewName { get; set; }
    }
}
