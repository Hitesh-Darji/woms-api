using MediatR;
using WOMS.Application.Features.StockRequest.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.StockRequest.Queries.GetAllStockRequests
{
    public record GetAllStockRequestsQuery : IRequest<StockRequestListResponse>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? SearchTerm { get; init; }
        public StockRequestStatus? Status { get; init; }
        public Guid? FromLocationId { get; init; }
        public Guid? ToLocationId { get; init; }
        public string? RequesterId { get; init; }
        public string SortBy { get; init; } = "RequestDate";
        public bool SortDescending { get; init; } = true;
    }
}

