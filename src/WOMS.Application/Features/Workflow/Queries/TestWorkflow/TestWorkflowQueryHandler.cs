using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Domain.Enums;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Workflow.Queries.TestWorkflow
{
    public class TestWorkflowQueryHandler : IRequestHandler<TestWorkflowQuery, TestWorkflowResponse>
    {
        private readonly IWorkflowRepository _workflowRepository;

        public TestWorkflowQueryHandler(IWorkflowRepository workflowRepository)
        {
            _workflowRepository = workflowRepository;
        }

        public async Task<TestWorkflowResponse> Handle(TestWorkflowQuery request, CancellationToken cancellationToken)
        {
            var workflow = await _workflowRepository.GetByIdWithNodesAsync(request.WorkflowId, cancellationToken);
            if (workflow == null)
            {
                return new TestWorkflowResponse
                {
                    Success = false,
                    Message = $"Workflow with ID {request.WorkflowId} not found."
                };
            }

            // Simulate workflow execution without creating an actual instance
            var steps = new List<WorkflowExecutionStep>();
            var startNode = workflow.Nodes.FirstOrDefault(n => n.Type == WorkflowNodeType.Start);
            
            if (startNode != null)
            {
                steps.Add(new WorkflowExecutionStep
                {
                    NodeId = startNode.Id,
                    NodeTitle = startNode.Title,
                    NodeType = startNode.Type,
                    Status = "completed",
                    Message = "Start node executed successfully",
                    ExecutedAt = DateTime.UtcNow
                });

                // Simulate execution of other nodes in order
                var sortedNodes = workflow.Nodes
                    .Where(n => n.Type != WorkflowNodeType.Start && n.Type != WorkflowNodeType.End)
                    .OrderBy(n => n.OrderIndex)
                    .ToList();

                foreach (var node in sortedNodes)
                {
                    var message = GetSimulatedMessage(node.Type);
                    
                    steps.Add(new WorkflowExecutionStep
                    {
                        NodeId = node.Id,
                        NodeTitle = node.Title,
                        NodeType = node.Type,
                        Status = "completed",
                        Message = message,
                        ExecutedAt = DateTime.UtcNow
                    });
                }

                // Add end node
                var endNode = workflow.Nodes.FirstOrDefault(n => n.Type == WorkflowNodeType.End);
                if (endNode != null)
                {
                    steps.Add(new WorkflowExecutionStep
                    {
                        NodeId = endNode.Id,
                        NodeTitle = endNode.Title,
                        NodeType = endNode.Type,
                        Status = "completed",
                        Message = "Workflow completed successfully",
                        ExecutedAt = DateTime.UtcNow
                    });
                }
            }

            return new TestWorkflowResponse
            {
                Success = true,
                Message = $"Workflow '{workflow.Name}' simulation completed successfully",
                Steps = steps,
                ResultData = request.TestData ?? new Dictionary<string, object>()
            };
        }

        private string GetSimulatedMessage(WorkflowNodeType nodeType)
        {
            return nodeType switch
            {
                WorkflowNodeType.Status => "Status updated successfully",
                WorkflowNodeType.Condition => "Condition evaluated successfully",
                WorkflowNodeType.Approval => "Approval triggered successfully",
                WorkflowNodeType.Notification => "Notification sent successfully",
                WorkflowNodeType.Escalation => "Escalation triggered successfully",
                _ => "Node executed successfully"
            };
        }
    }
}

