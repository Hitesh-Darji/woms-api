using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using System.Text.Json;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Workflow.Commands.CreateWorkflow
{
    public class CreateWorkflowCommandHandler : IRequestHandler<CreateWorkflowCommand, WorkflowDto>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMapper _mapper; 
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkflowCommandHandler(IWorkflowRepository workflowRepository, IMapper mapper,IUnitOfWork unitOfWork)
        {
            _workflowRepository = workflowRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkflowDto> Handle(CreateWorkflowCommand request, CancellationToken cancellationToken)
        {
            var workflow = new Domain.Entities.Workflow
            {
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                CurrentVersion = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,

            };

            await _workflowRepository.AddAsync(workflow, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WorkflowDto>(workflow);
        }
    }
}
