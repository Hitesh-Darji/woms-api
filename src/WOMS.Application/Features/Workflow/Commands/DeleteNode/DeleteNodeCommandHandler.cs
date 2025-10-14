using MediatR;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Workflow.Commands.DeleteNode
{
    public class DeleteNodeCommandHandler : IRequestHandler<DeleteNodeCommand, bool>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNodeCommandHandler(IWorkflowRepository workflowRepository, IUnitOfWork unitOfWork)
        {
            _workflowRepository = workflowRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
        {
            var node = await _workflowRepository.GetNodeByIdAsync(request.NodeId, cancellationToken);
            if (node == null)
            {
                return false;
            }

            await _workflowRepository.DeleteNodeAsync(node, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
