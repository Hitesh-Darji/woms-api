using MediatR;
using WOMS.Application.Features.AssignmentTemplate.DTOs;

namespace WOMS.Application.Features.AssignmentTemplate.Queries.GetAssignmentTemplateById
{
    public class GetAssignmentTemplateByIdQuery : IRequest<AssignmentTemplateDto?>
    {
        public Guid Id { get; set; }
    }
}
