using AutoMapper;
using MediatR;
using WOMS.Application.Features.StockRequest.DTOs;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Domain.Enums;
using WOMS.Application.Interfaces;

namespace WOMS.Application.Features.StockRequest.Commands.CreateStockRequest
{
    public class CreateStockRequestCommandHandler : IRequestHandler<CreateStockRequestCommand, StockRequestDto>
    {
        private readonly IStockRequestRepository _stockRequestRepository;
        private readonly IRepository<WOMS.Domain.Entities.Location> _locationRepository;
        private readonly IRepository<InventoryItem> _inventoryItemRepository;
        private readonly IRepository<WOMS.Domain.Entities.WorkOrder> _workOrderRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStockRequestCommandHandler(
            IStockRequestRepository stockRequestRepository,
            IRepository<WOMS.Domain.Entities.Location> locationRepository,
            IRepository<InventoryItem> inventoryItemRepository,
            IRepository<WOMS.Domain.Entities.WorkOrder> workOrderRepository,
            AutoMapper.IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _stockRequestRepository = stockRequestRepository;
            _locationRepository = locationRepository;
            _inventoryItemRepository = inventoryItemRepository;
            _workOrderRepository = workOrderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<StockRequestDto> Handle(CreateStockRequestCommand request, CancellationToken cancellationToken)
        {
            // Validate locations exist
            var fromLocation = await _locationRepository.GetByIdAsync(request.FromLocationId, cancellationToken);
            if (fromLocation == null)
            {
                throw new ArgumentException($"From location with ID {request.FromLocationId} not found.");
            }

            var toLocation = await _locationRepository.GetByIdAsync(request.ToLocationId, cancellationToken);
            if (toLocation == null)
            {
                throw new ArgumentException($"To location with ID {request.ToLocationId} not found.");
            }

            // Validate work order if provided
            if (request.WorkOrderId.HasValue)
            {
                var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId.Value, cancellationToken);
                if (workOrder == null)
                {
                    throw new ArgumentException($"Work order with ID {request.WorkOrderId} not found.");
                }
            }

            // Validate inventory items exist
            foreach (var item in request.RequestItems)
            {
                var inventoryItem = await _inventoryItemRepository.GetByIdAsync(item.ItemId, cancellationToken);
                if (inventoryItem == null)
                {
                    throw new ArgumentException($"Inventory item with ID {item.ItemId} not found.");
                }
            }

            // Create stock request
            var stockRequest = new WOMS.Domain.Entities.StockRequest
            {
                RequesterId = request.CreatedBy,
                FromLocationId = request.FromLocationId,
                ToLocationId = request.ToLocationId,
                Status = StockRequestStatus.Pending,
                RequestDate = DateTime.UtcNow,
                Notes = request.Notes,
                WorkOrderId = request.WorkOrderId,
                CreatedBy = Guid.Parse(request.CreatedBy),
                CreatedOn = DateTime.UtcNow
            };

            // Add request items
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

            await _stockRequestRepository.AddAsync(stockRequest, cancellationToken);
            
            // Save changes to database
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Get the created entity with details for mapping
            var createdStockRequest = await _stockRequestRepository.GetByIdWithDetailsAsync(stockRequest.Id, cancellationToken);
            if (createdStockRequest == null)
            {
                throw new InvalidOperationException("Failed to retrieve created stock request.");
            }

            return _mapper.Map<StockRequestDto>(createdStockRequest);
        }
    }
}
