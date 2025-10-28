using MediatR;
using WOMS.Application.Features.Workflow.Commands.SendWorkflowNotification;
using WOMS.Domain.Entities;
using WOMS.Domain.Enums;
using WOMS.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WOMS.Application.Features.Assignment.Commands.AssignWorkOrder
{
    public class AssignWorkOrderHandler : IRequestHandler<AssignWorkOrderCommand, bool>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWorkOrderAssignmentRepository _workOrderAssignmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;

        public AssignWorkOrderHandler(
            IWorkOrderRepository workOrderRepository, 
            IUserRepository userRepository, 
            IWorkOrderAssignmentRepository workOrderAssignmentRepository, 
            IHttpContextAccessor httpContextAccessor,
            IMediator mediator)
        {
            _workOrderRepository = workOrderRepository;
            _userRepository = userRepository;
            _workOrderAssignmentRepository = workOrderAssignmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AssignWorkOrderCommand request, CancellationToken cancellationToken)
        {
            // Extract user ID from JWT token claims
            var assignedBy = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "system";

            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId, cancellationToken);
            if (workOrder == null || workOrder.IsDeleted)
                return false;

            // Verify technician exists and is active
            var technician = await _userRepository.GetByIdActiveAsync(request.TechnicianId, cancellationToken);
            if (technician == null)
                return false;

            if (!string.IsNullOrEmpty(workOrder.Assignee) && workOrder.Assignee != request.TechnicianId)
            {
                var hasActiveAssignment = await _workOrderAssignmentRepository.HasActiveAssignmentAsync(request.WorkOrderId, cancellationToken);
                if (hasActiveAssignment)
                    return false; 
            }

            var technicianWorkload = await _workOrderRepository.GetByAssignedUserAsync(request.TechnicianId, cancellationToken);
            var currentWorkload = technicianWorkload.Count();
            var maxWorkload = 6; 

            if (currentWorkload >= maxWorkload)
                return false; 

            var assignment = new WorkOrderAssignment
            {
                Id = Guid.NewGuid(),
                WorkOrderId = request.WorkOrderId,
                TechnicianId = request.TechnicianId,
                AssignedAt = DateTime.UtcNow,
                AssignedBy = assignedBy,
                Status = AssignmentStatus.Assigned,
                Notes = null, // No notes for manual assignments
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CreatedBy = Guid.TryParse(assignedBy, out var createdByGuid) ? createdByGuid : null,
                UpdatedBy = Guid.TryParse(assignedBy, out var updatedByGuid) ? updatedByGuid : null
            };

            // Update work order
            workOrder.Assignee = request.TechnicianId;
            workOrder.Status = WorkOrderStatus.Assigned;
            workOrder.UpdatedOn = DateTime.UtcNow;
            workOrder.UpdatedBy = Guid.TryParse(assignedBy, out var assignedByGuid) ? assignedByGuid : null;

            // Save changes
            await _workOrderAssignmentRepository.AddAsync(assignment, cancellationToken);
            await _workOrderRepository.UpdateAsync(workOrder, cancellationToken);

            // Send workflow notifications if workflow is assigned
            if (workOrder.WorkflowId.HasValue)
            {
                await _mediator.Send(new SendWorkflowNotificationCommand
                {
                    WorkflowId = workOrder.WorkflowId.Value,
                    Trigger = "work_order_assigned",
                    TemplateData = new Dictionary<string, object>
                    {
                        { "OrderId", workOrder.Id.ToString() },
                        { "OrderNumber", workOrder.WorkOrderNumber },
                        { "Technician", technician.UserName ?? "Unknown" },
                        { "TechnicianName", technician.FullName ?? "Unknown" },
                        { "AssignedAt", assignment.AssignedAt.ToString() },
                        { "Priority", workOrder.Priority.ToString() }
                    }
                }, cancellationToken);
            }

            return true;
        }
    }
}
