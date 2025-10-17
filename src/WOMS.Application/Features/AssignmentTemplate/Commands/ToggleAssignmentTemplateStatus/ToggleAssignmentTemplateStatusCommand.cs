using MediatR;
using WOMS.Application.Features.AssignmentTemplate.DTOs;

namespace WOMS.Application.Features.AssignmentTemplate.Commands.ToggleAssignmentTemplateStatus
{
    public class ToggleAssignmentTemplateStatusCommand : IRequest<AssignmentTemplateDto>
    {
        public Guid Id { get; set; }
        public WOMS.Domain.Enums.AssignmentTemplateStatus Status { get; set; }
    }
}
