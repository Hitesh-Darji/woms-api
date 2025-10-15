using MediatR;
using WOMS.Application.Features.Workflow.DTOs;

namespace WOMS.Application.Features.Workflow.Queries.GetWorkflowById
{
    public class GetWorkflowByIdQuery : IRequest<WorkflowGetDto?>
    {
        public Guid Id { get; set; }
    }
}
