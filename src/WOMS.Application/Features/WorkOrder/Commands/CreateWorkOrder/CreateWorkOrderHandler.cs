using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WOMS.Application.Features.WorkOrder.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Enums;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.WorkOrder.Commands.CreateWorkOrder
{
    public class CreateWorkOrderHandler : IRequestHandler<CreateWorkOrderCommand, WorkOrderDto>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IBillingTemplateRepository _billingTemplateRepository;
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateWorkOrderHandler(
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

        public async Task<WorkOrderDto> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
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

            var workOrder = new Domain.Entities.WorkOrder
            {
                Id = Guid.NewGuid(),
                Customer = request.Customer,
                CustomerContact = request.CustomerContact,
                Type = request.Type,
                Priority = request.Priority,
                Status = WorkOrderStatus.Pending,
                Assignee = request.Assignee,
                Location = request.Location,
                Address = request.Address,
                Description = request.Description,
                DueDate = request.DueDate,
                ActualHours = request.ActualHours,
                Cost = request.Cost,
                Tags = request.Tags,
                Equipment = request.Equipment,
                Notes = request.Notes,
                Utility = request.Utility,
                Make = request.Make,
                Model = request.Model,
                Size = request.Size,
                ManagerTechnician = request.ManagerTechnician,
                WorkflowId = request.WorkflowId,
                FormTemplateId = request.FormTemplateId,
                BillingTemplateId = request.BillingTemplateId,
                WorkOrderNumber = GenerateWorkOrderNumber(),
                CreatedDate = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userId,
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = userId,
                IsDeleted = false
            };

            await _workOrderRepository.AddAsync(workOrder, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WorkOrderDto>(workOrder);
        }

        private static string GenerateWorkOrderNumber()
        {
            // Generate a unique work order number
            // Format: WO-YYYYMMDD-HHMMSS-XXXX
            var now = DateTime.UtcNow;
            var random = new Random();
            var randomSuffix = random.Next(1000, 9999);
            return $"WO-{now:yyyyMMdd}-{now:HHmmss}-{randomSuffix}";
        }
    }
}
