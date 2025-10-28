using MediatR;
using WOMS.Application.Features.Workflow.DTOs;

namespace WOMS.Application.Features.Workflow.Queries.TestWorkflow
{
    public class TestWorkflowQuery : IRequest<TestWorkflowResponse>
    {
        public Guid WorkflowId { get; set; }
        public Dictionary<string, object>? TestData { get; set; }
    }
}

