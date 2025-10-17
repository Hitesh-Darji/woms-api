using MediatR;
using WOMS.Application.Features.StockRequest.DTOs;

namespace WOMS.Application.Features.StockRequest.Commands.ApproveStockRequest
{
    public record ApproveStockRequestCommand : IRequest<StockRequestDto>
    {
        public Guid Id { get; init; }
        public string? ApprovalNotes { get; init; }
        public List<ApproveRequestItemDto> RequestItems { get; init; } = new List<ApproveRequestItemDto>();
        public string ApprovedBy { get; init; } = string.Empty;
    }
}

