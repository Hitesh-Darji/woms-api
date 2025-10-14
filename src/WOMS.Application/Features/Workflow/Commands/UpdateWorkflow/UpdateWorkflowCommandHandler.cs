using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
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

            workflow.Name = request.Name;
            workflow.Description = request.Description;
            workflow.Category = request.Category;
            workflow.IsActive = request.IsActive;
            workflow.UpdatedOn = DateTime.UtcNow;

            await _workflowRepository.UpdateAsync(workflow, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WorkflowDto>(workflow);
        }
    }
}
