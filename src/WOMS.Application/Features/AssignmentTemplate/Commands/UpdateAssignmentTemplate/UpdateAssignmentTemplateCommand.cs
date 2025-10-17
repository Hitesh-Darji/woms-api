using MediatR;
using WOMS.Application.Features.AssignmentTemplate.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.AssignmentTemplate.Commands.UpdateAssignmentTemplate
{
    public class UpdateAssignmentTemplateCommand : IRequest<AssignmentTemplateDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public WOMS.Domain.Enums.AssignmentTemplateStatus Status { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<DayOfWeekEnum> DaysOfWeek { get; set; } = new();
        public List<string> WorkTypes { get; set; } = new();
        public List<string> Zones { get; set; } = new();
        public List<string> PreferredTechnicians { get; set; } = new();
        public List<string> SkillsRequired { get; set; } = new();
        public List<string> AutoAssignmentRules { get; set; } = new();
    }
}
