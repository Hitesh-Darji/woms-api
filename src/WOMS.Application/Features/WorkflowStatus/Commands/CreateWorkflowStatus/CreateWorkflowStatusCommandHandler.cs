using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Features.WorkflowStatus.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.WorkflowStatus.Commands.CreateWorkflowStatus
{
    public class CreateWorkflowStatusCommand : IRequest<WorkflowStatusDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Color { get; set; } = "#3b82f6";
        public int Order { get; set; } = 1;
        public bool IsActive { get; set; } = true;
    }

    public class CreateWorkflowStatusCommandHandler : IRequestHandler<CreateWorkflowStatusCommand, WorkflowStatusDto>
    {
        private readonly IWorkflowStatusRepository _workflowStatusRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateWorkflowStatusCommandHandler(
            IWorkflowStatusRepository workflowStatusRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _workflowStatusRepository = workflowStatusRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<WorkflowStatusDto> Handle(CreateWorkflowStatusCommand request, CancellationToken cancellationToken)
        {
            // Check if status with same name already exists
            if (await _workflowStatusRepository.ExistsByNameAsync(request.Name, cancellationToken))
            {
                throw new InvalidOperationException($"Workflow status with name '{request.Name}' already exists.");
            }

            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            var workflowStatus = new Domain.Entities.WorkflowStatus
            {
                Name = request.Name,
                Description = request.Description,
                Color = request.Color,
                Order = request.Order,
                IsActive = request.IsActive,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userId,
                IsDeleted = false
            };

            await _workflowStatusRepository.AddAsync(workflowStatus, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WorkflowStatusDto>(workflowStatus);
        }
    }
}
