using MediatR;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.WorkOrder.Commands.DeleteWorkOrder
{
    public class DeleteWorkOrderHandler : IRequestHandler<DeleteWorkOrderCommand, bool>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWorkOrderHandler(IWorkOrderRepository workOrderRepository, IUnitOfWork unitOfWork)
        {
            _workOrderRepository = workOrderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var workOrder = await _workOrderRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (workOrder == null)
            {
                return false;
            }

            await _workOrderRepository.DeleteAsync(workOrder, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
