using System.ComponentModel.DataAnnotations;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.WorkflowStatus.DTOs
{
    public class WorkflowStatusDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Color { get; set; } = "#3b82f6";
        public int Order { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public List<WorkflowStatusTransitionDto> AllowedTransitions { get; set; } = new List<WorkflowStatusTransitionDto>();
    }

    public class WorkflowStatusTransitionDto
    {
        public Guid Id { get; set; }
        public Guid FromStatusId { get; set; }
        public Guid ToStatusId { get; set; }
        public string ToStatusName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class CreateWorkflowStatusRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(7)]
        public string Color { get; set; } = "#3b82f6";

        [Required]
        [Range(1, int.MaxValue)]
        public int Order { get; set; } = 1;

        public bool IsActive { get; set; } = true;
    }

    public class UpdateWorkflowStatusRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(7)]
        public string Color { get; set; } = "#3b82f6";

        [Required]
        [Range(1, int.MaxValue)]
        public int Order { get; set; } = 1;

        public bool IsActive { get; set; } = true;
    }

    public class WorkflowCategoryDto
    {
        public WorkflowCategory Value { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class CreateWorkflowStatusTransitionRequest
    {
        [Required]
        public Guid FromStatusId { get; set; }

        [Required]
        public Guid ToStatusId { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
