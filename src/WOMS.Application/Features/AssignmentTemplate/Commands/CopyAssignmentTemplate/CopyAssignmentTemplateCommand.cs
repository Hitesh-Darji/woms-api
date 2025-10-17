using MediatR;
using WOMS.Application.Features.AssignmentTemplate.DTOs;

namespace WOMS.Application.Features.AssignmentTemplate.Commands.CopyAssignmentTemplate
{
    public class CopyAssignmentTemplateCommand : IRequest<AssignmentTemplateDto>
    {
        public Guid Id { get; set; }
        public string? NewName { get; set; } // Optional: if not provided, will add "(Copy)" to original name
    }
}
