using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using System.Text.Json;

namespace WOMS.Application.Features.Workflow.Commands.UpdateNode
{
    public class UpdateNodeCommandHandler : IRequestHandler<UpdateNodeCommand, WorkflowNodeDto>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNodeCommandHandler(IWorkflowRepository workflowRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _workflowRepository = workflowRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkflowNodeDto> Handle(UpdateNodeCommand request, CancellationToken cancellationToken)
        {
            var node = await _workflowRepository.GetNodeByIdAsync(request.NodeId, cancellationToken);
            if (node == null)
            {
                throw new ArgumentException($"Node with ID {request.NodeId} not found.");
            }

            node.Title = request.Title;
            node.Description = request.Description;
            node.Position = request.Position != null ? JsonSerializer.Serialize(request.Position) : null;
            node.Data = request.Data != null ? JsonSerializer.Serialize(request.Data) : null;
            node.OrderIndex = request.OrderIndex;
            node.UpdatedOn = DateTime.UtcNow;

            _workflowRepository.UpdateNode(node, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WorkflowNodeDto>(node);
        }
    }
}
