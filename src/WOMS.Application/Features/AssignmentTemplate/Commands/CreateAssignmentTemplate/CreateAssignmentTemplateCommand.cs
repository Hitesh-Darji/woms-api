using MediatR;
using WOMS.Application.Features.AssignmentTemplate.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.AssignmentTemplate.Commands.CreateAssignmentTemplate
{
    public class CreateAssignmentTemplateCommand : IRequest<AssignmentTemplateDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public WOMS.Domain.Enums.AssignmentTemplateStatus Status { get; set; } = WOMS.Domain.Enums.AssignmentTemplateStatus.Active;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<DayOfWeekEnum> DaysOfWeek { get; set; } = new();
        public List<string> AutoAssignmentRules { get; set; } = new();
    }
}
