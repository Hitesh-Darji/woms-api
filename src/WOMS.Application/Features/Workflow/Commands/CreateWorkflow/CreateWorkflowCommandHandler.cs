using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using System.Text.Json;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.Workflow.Commands.CreateWorkflow
{
    public class CreateWorkflowCommandHandler : IRequestHandler<CreateWorkflowCommand, WorkflowDto>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        IHttpContextAccessor _httpContextAccessor;

        public CreateWorkflowCommandHandler(IWorkflowRepository workflowRepository, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _workflowRepository = workflowRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContext;
        }

        public async Task<WorkflowDto> Handle(CreateWorkflowCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            var workflow = new Domain.Entities.Workflow
            {
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                CurrentVersion = 1,
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userId,
                IsDeleted = false
            };

            await _workflowRepository.AddAsync(workflow, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Add workflow nodes if provided
            if (request.Nodes != null && request.Nodes.Any())
            {
                await CreateWorkflowNodes(workflow.Id, request.Nodes, userId, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            // Load the workflow with nodes to include them in the response
            var workflowWithNodes = await _workflowRepository.GetByIdWithNodesAsync(workflow.Id, cancellationToken);
            return _mapper.Map<WorkflowDto>(workflowWithNodes ?? workflow);
        }

        private async Task CreateWorkflowNodes(Guid workflowId, List<WorkflowNodeDto> nodeDtos, Guid userId, CancellationToken cancellationToken)
        {
            foreach (var nodeDto in nodeDtos)
            {
                var newNode = CreateNewNode(workflowId, nodeDto, userId);
                await _workflowRepository.AddNodeAsync(newNode, cancellationToken);
            }
        }

        private Domain.Entities.WorkflowNode CreateNewNode(Guid workflowId, WorkflowNodeDto nodeDto, Guid userId)
        {
            var newNode = new Domain.Entities.WorkflowNode
            {
                Id = Guid.NewGuid(), // Always generate new ID for new nodes
                WorkflowId = workflowId, // Use the newly created workflow ID
                Type = nodeDto.Type,
                Title = nodeDto.Title,
                Description = nodeDto.Description,
                OrderIndex = nodeDto.OrderIndex,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userId,
                IsDeleted = false
            };

            // Set position
            if (nodeDto.Position != null)
            {
                newNode.Position = JsonSerializer.Serialize(nodeDto.Position);
            }

            // Set connections
            if (nodeDto.Connections != null)
            {
                newNode.Connections = JsonSerializer.Serialize(nodeDto.Connections);
            }

            // Set node-specific data
            var nodeData = new Dictionary<string, object>();
            
            // First, use the Data field if provided (from API payload)
            if (nodeDto.Data != null)
            {
                foreach (var kvp in nodeDto.Data)
                {
                    nodeData[kvp.Key] = kvp.Value;
                }
            }
            
            // Then, override with specific config objects if provided
            switch (nodeDto.Type)
            {
                case WorkflowNodeType.Start:
                    if (nodeDto.StartConfig != null)
                    {
                        nodeData["triggerType"] = nodeDto.StartConfig.TriggerType.ToString();
                    }
                    break;
                    
                case WorkflowNodeType.Status:
                    if (nodeDto.StatusConfig != null)
                    {
                        nodeData["targetStatus"] = nodeDto.StatusConfig.TargetStatus;
                        nodeData["autoAssignTo"] = nodeDto.StatusConfig.AutoAssignTo.ToString();
                    }
                    break;
                    
                case WorkflowNodeType.Condition:
                    if (nodeDto.ConditionConfig != null)
                    {
                        nodeData["fieldToCheck"] = nodeDto.ConditionConfig.FieldToCheck.ToString();
                        nodeData["operator"] = nodeDto.ConditionConfig.Operator.ToString();
                        nodeData["value"] = nodeDto.ConditionConfig.Value;
                        nodeData["logicalOperator"] = nodeDto.ConditionConfig.LogicalOperator;
                    }
                    break;
                    
                case WorkflowNodeType.Approval:
                    if (nodeDto.ApprovalConfig != null)
                    {
                        nodeData["approvalName"] = nodeDto.ApprovalConfig.ApprovalName;
                        nodeData["approverRoles"] = nodeDto.ApprovalConfig.ApproverRoles;
                        nodeData["approvalType"] = nodeDto.ApprovalConfig.ApprovalType.ToString();
                        nodeData["deadlineHours"] = nodeDto.ApprovalConfig.DeadlineHours;
                    }
                    break;
                    
                case WorkflowNodeType.Notification:
                    if (nodeDto.NotificationConfig != null)
                    {
                        nodeData["notificationType"] = nodeDto.NotificationConfig.NotificationType.ToString();
                        nodeData["recipient"] = nodeDto.NotificationConfig.Recipient.ToString();
                        nodeData["messageTemplate"] = nodeDto.NotificationConfig.MessageTemplate.ToString();
                    }
                    break;
                    
                case WorkflowNodeType.Escalation:
                    if (nodeDto.EscalationConfig != null)
                    {
                        nodeData["trigger"] = nodeDto.EscalationConfig.Trigger.ToString();
                        nodeData["hoursToWait"] = nodeDto.EscalationConfig.HoursToWait;
                        nodeData["action"] = nodeDto.EscalationConfig.Action.ToString();
                    }
                    break;
                    
                case WorkflowNodeType.End:
                    if (nodeDto.EndConfig != null)
                    {
                        nodeData["completionAction"] = nodeDto.EndConfig.CompletionAction.ToString();
                    }
                    break;
            }

            if (nodeData.Any())
            {
                newNode.Data = JsonSerializer.Serialize(nodeData);
            }

            return newNode;
        }
    }
}
