using MediatR;

namespace WOMS.Application.Features.StockRequest.Commands.DeleteStockRequest
{
    public record DeleteStockRequestCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
        public string DeletedBy { get; init; } = string.Empty;
    }
}

