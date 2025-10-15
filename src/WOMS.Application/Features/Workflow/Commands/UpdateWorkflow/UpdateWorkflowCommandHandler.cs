using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Domain.Enums;
using System.Text.Json;

namespace WOMS.Application.Features.Workflow.Commands.UpdateWorkflow
{
    public class UpdateWorkflowCommandHandler : IRequestHandler<UpdateWorkflowCommand, WorkflowDto>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkflowCommandHandler(IWorkflowRepository workflowRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _workflowRepository = workflowRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkflowDto> Handle(UpdateWorkflowCommand request, CancellationToken cancellationToken)
        {
            var workflow = await _workflowRepository.GetByIdAsync(request.Id, cancellationToken);
            if (workflow == null)
            {
                throw new ArgumentException($"Workflow with ID {request.Id} not found.");
            }

            // Update workflow properties
            workflow.Name = request.Name;
            workflow.Description = request.Description;
            workflow.Category = request.Category;
            workflow.IsActive = request.IsActive;
            workflow.UpdatedOn = DateTime.UtcNow;

            // Update workflow nodes
            if (request.Nodes != null)
            {
                await UpdateWorkflowNodes(workflow, request.Nodes, cancellationToken);
            }

            await _workflowRepository.UpdateAsync(workflow, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WorkflowDto>(workflow);
        }

        private async Task UpdateWorkflowNodes(Domain.Entities.Workflow workflow, List<WorkflowNodeDto> nodeDtos, CancellationToken cancellationToken)
        {
            var existingNodes = await _workflowRepository.GetNodesByWorkflowIdAsync(workflow.Id, cancellationToken);         
            var existingNodesDict = existingNodes.ToDictionary(n => n.Id);
            
            foreach (var nodeDto in nodeDtos)
            {               
                if (nodeDto.Id != Guid.Empty && existingNodesDict.TryGetValue(nodeDto.Id, out var existingNode))
                {
                    var freshNode = await _workflowRepository.GetNodeByIdAsync(nodeDto.Id, cancellationToken);
                    if (freshNode != null)
                    {
                        UpdateExistingNode(freshNode, nodeDto);
                        _workflowRepository.UpdateNode(freshNode, cancellationToken);
                    }
                }
                else
                {
                    var newNode = CreateNewNode(workflow.Id, nodeDto);
                    await _workflowRepository.AddNodeAsync(newNode, cancellationToken);
                }
            }
            
            var nodeIdsInRequest = nodeDtos.Select(n => n.Id).Where(id => id != Guid.Empty).ToHashSet();
            var nodesToSoftDelete = existingNodes.Where(n => !nodeIdsInRequest.Contains(n.Id) && !n.IsDeleted).ToList();
            
            foreach (var nodeToSoftDelete in nodesToSoftDelete)
            {
                nodeToSoftDelete.IsDeleted = true;
                nodeToSoftDelete.DeletedOn = DateTime.UtcNow;
                nodeToSoftDelete.DeletedBy = workflow.UpdatedBy;
                _workflowRepository.UpdateNode(nodeToSoftDelete, cancellationToken);
            }
        }

        private void UpdateExistingNode(Domain.Entities.WorkflowNode existingNode, WorkflowNodeDto nodeDto)
        {
            existingNode.Type = nodeDto.Type;
            existingNode.Title = nodeDto.Title;
            existingNode.Description = nodeDto.Description;
            existingNode.OrderIndex = nodeDto.OrderIndex;
            existingNode.UpdatedOn = DateTime.UtcNow;

            // Update position
            if (nodeDto.Position != null)
            {
                existingNode.Position = JsonSerializer.Serialize(nodeDto.Position);
            }

            // Update connections
            if (nodeDto.Connections != null)
            {
                existingNode.Connections = JsonSerializer.Serialize(nodeDto.Connections);
            }

            // Update node-specific data
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
                existingNode.Data = JsonSerializer.Serialize(nodeData);
            }
        }

        private Domain.Entities.WorkflowNode CreateNewNode(Guid workflowId, WorkflowNodeDto nodeDto)
        {
            var newNode = new Domain.Entities.WorkflowNode
            {
                Id = nodeDto.Id != Guid.Empty ? nodeDto.Id : Guid.NewGuid(),
                WorkflowId = workflowId,
                Type = nodeDto.Type,
                Title = nodeDto.Title,
                Description = nodeDto.Description,
                OrderIndex = nodeDto.OrderIndex,
                CreatedOn = DateTime.UtcNow,
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

            // Set node-specific data (same logic as UpdateExistingNode)
            var nodeData = new Dictionary<string, object>();
            
            // First, use the Data field if provided (from API payload)
            if (nodeDto.Data != null)
            {
                foreach (var kvp in nodeDto.Data)
                {
                    nodeData[kvp.Key] = kvp.Value;
                }
            }
            
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
