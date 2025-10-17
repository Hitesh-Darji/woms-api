using MediatR;
using WOMS.Domain.Repositories;
using WOMS.Domain.Enums;
using WOMS.Application.Interfaces;

namespace WOMS.Application.Features.StockRequest.Commands.DeleteStockRequest
{
    public class DeleteStockRequestCommandHandler : IRequestHandler<DeleteStockRequestCommand, bool>
    {
        private readonly IStockRequestRepository _stockRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStockRequestCommandHandler(IStockRequestRepository stockRequestRepository, IUnitOfWork unitOfWork)
        {
            _stockRequestRepository = stockRequestRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteStockRequestCommand request, CancellationToken cancellationToken)
        {
            var stockRequest = await _stockRequestRepository.GetByIdAsync(request.Id, cancellationToken);
            if (stockRequest == null)
            {
                return false;
            }

            // Only allow deletion if status is Pending
            if (stockRequest.Status != StockRequestStatus.Pending)
            {
                throw new InvalidOperationException("Only pending stock requests can be deleted.");
            }

            await _stockRequestRepository.SoftDeleteAsync(request.Id, request.DeletedBy, cancellationToken);
            
            // Save changes to database
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}
