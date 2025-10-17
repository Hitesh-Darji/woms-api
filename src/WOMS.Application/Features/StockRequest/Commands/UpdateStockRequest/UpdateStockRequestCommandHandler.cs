using AutoMapper;
using MediatR;
using WOMS.Application.Features.StockRequest.DTOs;
using WOMS.Domain.Repositories;
using WOMS.Domain.Enums;
using WOMS.Domain.Entities;
using WOMS.Application.Interfaces;

namespace WOMS.Application.Features.StockRequest.Commands.UpdateStockRequest
{
    public class UpdateStockRequestCommandHandler : IRequestHandler<UpdateStockRequestCommand, StockRequestDto>
    {
        private readonly IStockRequestRepository _stockRequestRepository;
        private readonly IRepository<InventoryItem> _inventoryItemRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStockRequestCommandHandler(
            IStockRequestRepository stockRequestRepository,
            IRepository<InventoryItem> inventoryItemRepository,
            AutoMapper.IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _stockRequestRepository = stockRequestRepository;
            _inventoryItemRepository = inventoryItemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<StockRequestDto> Handle(UpdateStockRequestCommand request, CancellationToken cancellationToken)
        {
            var stockRequest = await _stockRequestRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);
            if (stockRequest == null)
            {
                throw new ArgumentException($"Stock request with ID {request.Id} not found.");
            }

            // Only allow updates if status is Pending
            if (stockRequest.Status != StockRequestStatus.Pending)
            {
                throw new InvalidOperationException("Only pending stock requests can be updated.");
            }

            // Update basic properties
            stockRequest.Notes = request.Notes;
            stockRequest.UpdatedBy = Guid.Parse(request.UpdatedBy);
            stockRequest.UpdatedOn = DateTime.UtcNow;

            // Validate inventory items for new/updated items
            foreach (var item in request.RequestItems)
            {
                var inventoryItem = await _inventoryItemRepository.GetByIdAsync(item.ItemId, cancellationToken);
                if (inventoryItem == null)
                {
                    throw new ArgumentException($"Inventory item with ID {item.ItemId} not found.");
                }
            }

            // Update request items
            stockRequest.RequestItems.Clear();
            for (int i = 0; i < request.RequestItems.Count; i++)
            {
                var requestItem = new WOMS.Domain.Entities.RequestItem
                {
                    RequestId = stockRequest.Id,
                    ItemId = request.RequestItems[i].ItemId,
                    RequestedQuantity = request.RequestItems[i].RequestedQuantity,
                    Notes = request.RequestItems[i].Notes,
                    OrderIndex = i
                };
                stockRequest.RequestItems.Add(requestItem);
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
