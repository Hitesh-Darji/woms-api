using MediatR;
using WOMS.Application.Features.StockRequest.DTOs;

namespace WOMS.Application.Features.StockRequest.Queries.GetStockRequestById
{
    public record GetStockRequestByIdQuery : IRequest<StockRequestDto?>
    {
        public Guid Id { get; init; }
    }
}

