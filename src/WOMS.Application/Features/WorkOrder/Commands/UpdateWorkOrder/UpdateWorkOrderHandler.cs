using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WOMS.Application.Features.WorkOrder.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.WorkOrder.Commands.UpdateWorkOrder
{
    public class UpdateWorkOrderHandler : IRequestHandler<UpdateWorkOrderCommand, WorkOrderDto>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IBillingTemplateRepository _billingTemplateRepository;
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateWorkOrderHandler(
            IWorkOrderRepository workOrderRepository, 
            IBillingTemplateRepository billingTemplateRepository,
            IFormTemplateRepository formTemplateRepository,
            IWorkflowRepository workflowRepository,
            AutoMapper.IMapper mapper, 
            IUnitOfWork unitOfWork, 
            IHttpContextAccessor httpContextAccessor)
        {
            _workOrderRepository = workOrderRepository;
            _billingTemplateRepository = billingTemplateRepository;
            _formTemplateRepository = formTemplateRepository;
            _workflowRepository = workflowRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<WorkOrderDto> Handle(UpdateWorkOrderCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            var workOrder = await _workOrderRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (workOrder == null)
            {
                throw new KeyNotFoundException($"WorkOrder with ID {request.Id} not found.");
            }

            // Validate foreign key references
            if (request.BillingTemplateId.HasValue)
            {
                var billingTemplateExists = await _billingTemplateRepository.GetByIdAsync(request.BillingTemplateId.Value, cancellationToken);
                if (billingTemplateExists == null)
                {
                    throw new ArgumentException($"BillingTemplate with ID {request.BillingTemplateId.Value} not found.");
                }
            }

            if (request.FormTemplateId.HasValue)
            {
                var formTemplateExists = await _formTemplateRepository.GetByIdAsync(request.FormTemplateId.Value, cancellationToken);
                if (formTemplateExists == null)
                {
                    throw new ArgumentException($"FormTemplate with ID {request.FormTemplateId.Value} not found.");
                }
            }

            if (request.WorkflowId.HasValue)
            {
                var workflowExists = await _workflowRepository.GetByIdAsync(request.WorkflowId.Value, cancellationToken);
                if (workflowExists == null)
                {
                    throw new ArgumentException($"Workflow with ID {request.WorkflowId.Value} not found.");
                }
            }

            // Update properties
            workOrder.Customer = request.Customer;
            workOrder.CustomerContact = request.CustomerContact;
            workOrder.Type = request.Type;
            workOrder.Priority = request.Priority;
            workOrder.Status = request.Status;
            workOrder.Assignee = request.Assignee;
            workOrder.Location = request.Location;
            workOrder.Address = request.Address;
            workOrder.Description = request.Description;
            workOrder.DueDate = request.DueDate;
            workOrder.ActualHours = request.ActualHours;
            workOrder.Cost = request.Cost;
            workOrder.Tags = request.Tags;
            workOrder.Equipment = request.Equipment;
            workOrder.Notes = request.Notes;
            workOrder.Utility = request.Utility;
            workOrder.Make = request.Make;
            workOrder.Model = request.Model;
            workOrder.Size = request.Size;
            workOrder.ManagerTechnician = request.ManagerTechnician;
            workOrder.WorkflowId = request.WorkflowId;
            workOrder.FormTemplateId = request.FormTemplateId;
            workOrder.BillingTemplateId = request.BillingTemplateId;
            workOrder.UpdatedOn = DateTime.UtcNow;
            workOrder.UpdatedBy = userId;

            await _workOrderRepository.UpdateAsync(workOrder, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WorkOrderDto>(workOrder);
        }
    }
}
