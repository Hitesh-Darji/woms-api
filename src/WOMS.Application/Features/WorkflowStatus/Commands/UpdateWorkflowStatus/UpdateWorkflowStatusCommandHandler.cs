using MediatR;
using WOMS.Application.Features.WorkflowStatus.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.WorkflowStatus.Commands.UpdateWorkflowStatus
{
    public class UpdateWorkflowStatusCommand : IRequest<WorkflowStatusDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Color { get; set; } = "#3b82f6";
        public int Order { get; set; } = 1;
        public bool IsActive { get; set; } = true;
    }

    public class UpdateWorkflowStatusCommandHandler : IRequestHandler<UpdateWorkflowStatusCommand, WorkflowStatusDto>
    {
        private readonly IWorkflowStatusRepository _workflowStatusRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkflowStatusCommandHandler(
            IWorkflowStatusRepository workflowStatusRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _workflowStatusRepository = workflowStatusRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkflowStatusDto> Handle(UpdateWorkflowStatusCommand request, CancellationToken cancellationToken)
        {
            var workflowStatus = await _workflowStatusRepository.GetByIdAsync(request.Id, cancellationToken);
            if (workflowStatus == null)
            {
                throw new ArgumentException($"Workflow status with ID {request.Id} not found.");
            }

            // Check if another status with same name already exists (excluding current one)
            var existingStatus = await _workflowStatusRepository.GetByNameAsync(request.Name, cancellationToken);
            if (existingStatus != null && existingStatus.Id != request.Id)
            {
                throw new InvalidOperationException($"Workflow status with name '{request.Name}' already exists.");
            }

            workflowStatus.Name = request.Name;
            workflowStatus.Description = request.Description;
            workflowStatus.Color = request.Color;
            workflowStatus.Order = request.Order;
            workflowStatus.IsActive = request.IsActive;
            workflowStatus.UpdatedOn = DateTime.UtcNow;

            await _workflowStatusRepository.UpdateAsync(workflowStatus, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WorkflowStatusDto>(workflowStatus);
        }
    }
}
