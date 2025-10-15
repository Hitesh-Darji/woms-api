using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.Workflow.Queries.GetNodeTypes
{
    public class GetNodeTypesQueryHandler : IRequestHandler<GetNodeTypesQuery, List<NodeTypeInfoDto>>
    {
        public Task<List<NodeTypeInfoDto>> Handle(GetNodeTypesQuery request, CancellationToken cancellationToken)
        {
            var nodeTypes = new List<NodeTypeInfoDto>
            {
                new NodeTypeInfoDto
                {
                    Type = WorkflowNodeType.Start,
                    Title = "Start",
                    Description = "Workflow entry point.",
                    Icon = "play",
                    Color = "green"
                },
                new NodeTypeInfoDto
                {
                    Type = WorkflowNodeType.Status,
                    Title = "Status",
                    Description = "Change work order status.",
                    Icon = "square",
                    Color = "blue"
                },
                new NodeTypeInfoDto
                {
                    Type = WorkflowNodeType.Condition,
                    Title = "Condition",
                    Description = "Branch based on criteria.",
                    Icon = "branch",
                    Color = "yellow"
                },
                new NodeTypeInfoDto
                {
                    Type = WorkflowNodeType.Approval,
                    Title = "Approval",
                    Description = "Require approval to continue.",
                    Icon = "users",
                    Color = "purple"
                },
                new NodeTypeInfoDto
                {
                    Type = WorkflowNodeType.Notification,
                    Title = "Notification",
                    Description = "Send email, SMS, or alert.",
                    Icon = "bell",
                    Color = "lightblue"
                },
                new NodeTypeInfoDto
                {
                    Type = WorkflowNodeType.Escalation,
                    Title = "Escalation",
                    Description = "Escalate overdue items.",
                    Icon = "warning",
                    Color = "red"
                },
                new NodeTypeInfoDto
                {
                    Type = WorkflowNodeType.End,
                    Title = "End",
                    Description = "Workflow completion.",
                    Icon = "target",
                    Color = "gray"
                }
            };

            return Task.FromResult(nodeTypes);
        }
    }
}
