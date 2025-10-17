using MediatR;

namespace WOMS.Application.Features.AssignmentTemplate.Commands.DeleteAssignmentTemplate
{
    public class DeleteAssignmentTemplateCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
