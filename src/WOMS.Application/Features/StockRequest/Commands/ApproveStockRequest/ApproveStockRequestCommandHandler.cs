using AutoMapper;
using MediatR;
using WOMS.Application.Features.StockRequest.DTOs;
using WOMS.Domain.Repositories;
using WOMS.Domain.Enums;
using WOMS.Application.Interfaces;

namespace WOMS.Application.Features.StockRequest.Commands.ApproveStockRequest
{
    public class ApproveStockRequestCommandHandler : IRequestHandler<ApproveStockRequestCommand, StockRequestDto>
    {
        private readonly IStockRequestRepository _stockRequestRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ApproveStockRequestCommandHandler(
            IStockRequestRepository stockRequestRepository,
            AutoMapper.IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _stockRequestRepository = stockRequestRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<StockRequestDto> Handle(ApproveStockRequestCommand request, CancellationToken cancellationToken)
        {
            var stockRequest = await _stockRequestRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);
            if (stockRequest == null)
            {
                throw new ArgumentException($"Stock request with ID {request.Id} not found.");
            }

            // Only allow approval if status is Pending
            if (stockRequest.Status != StockRequestStatus.Pending)
            {
                throw new InvalidOperationException("Only pending stock requests can be approved.");
            }

            // Update stock request
            stockRequest.Status = StockRequestStatus.Approved;
            stockRequest.ApprovedBy = request.ApprovedBy;
            stockRequest.ApprovalDate = DateTime.UtcNow;
            stockRequest.Notes = request.ApprovalNotes;
            stockRequest.UpdatedBy = Guid.Parse(request.ApprovedBy);
            stockRequest.UpdatedOn = DateTime.UtcNow;

            // Update request items with approved quantities
            foreach (var approvalItem in request.RequestItems)
            {
                var requestItem = stockRequest.RequestItems.FirstOrDefault(ri => ri.Id == approvalItem.Id);
                if (requestItem != null)
                {
                    requestItem.ApprovedQuantity = approvalItem.ApprovedQuantity;
                    if (!string.IsNullOrEmpty(approvalItem.Notes))
                    {
                        requestItem.Notes = approvalItem.Notes;
                    }
                }
            }

            await _stockRequestRepository.UpdateAsync(stockRequest, cancellationToken);
            
            // Save changes to database
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Get the updated entity with details for mapping
            var updatedStockRequest = await _stockRequestRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);
            if (updatedStockRequest == null)
            {
                throw new InvalidOperationException("Failed to retrieve updated stock request.");
            }

            return _mapper.Map<StockRequestDto>(updatedStockRequest);
        }
    }
}

