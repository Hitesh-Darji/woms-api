using MediatR;
using WOMS.Application.Features.StockRequest.DTOs;

namespace WOMS.Application.Features.StockRequest.Commands.CreateStockRequest
{
    public record CreateStockRequestCommand : IRequest<StockRequestDto>
    {
        public Guid FromLocationId { get; init; }
        public Guid ToLocationId { get; init; }
        public string? Notes { get; init; }
        public Guid? WorkOrderId { get; init; }
        public List<CreateRequestItemDto> RequestItems { get; init; } = new List<CreateRequestItemDto>();
        public string CreatedBy { get; init; } = string.Empty;
    }
}

