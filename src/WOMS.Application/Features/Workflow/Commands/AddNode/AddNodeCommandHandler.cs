using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using System.Text.Json;

namespace WOMS.Application.Features.Workflow.Commands.AddNode
{
    public class AddNodeCommandHandler : IRequestHandler<AddNodeCommand, WorkflowNodeDto>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddNodeCommandHandler(IWorkflowRepository workflowRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _workflowRepository = workflowRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkflowNodeDto> Handle(AddNodeCommand request, CancellationToken cancellationToken)
        {
            var workflow = await _workflowRepository.GetByIdAsync(request.WorkflowId, cancellationToken);
            if (workflow == null)
            {
                throw new ArgumentException($"Workflow with ID {request.WorkflowId} not found.");
            }

            var node = new WorkflowNode
            {
                WorkflowId = request.WorkflowId,
                Type = request.Type,
                Title = request.Title,
                Description = request.Description,
                Position = request.Position != null ? JsonSerializer.Serialize(request.Position) : null,
                Data = request.Data != null ? JsonSerializer.Serialize(request.Data) : null,
                OrderIndex = request.OrderIndex
            };

            await _workflowRepository.AddNodeAsync(node, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WorkflowNodeDto>(node);
        }
    }
}
