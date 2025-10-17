using MediatR;
using WOMS.Application.Features.StockRequest.DTOs;

namespace WOMS.Application.Features.StockRequest.Commands.UpdateStockRequest
{
    public record UpdateStockRequestCommand : IRequest<StockRequestDto>
    {
        public Guid Id { get; init; }
        public string? Notes { get; init; }
        public List<UpdateRequestItemDto> RequestItems { get; init; } = new List<UpdateRequestItemDto>();
        public string UpdatedBy { get; init; } = string.Empty;
    }
}

