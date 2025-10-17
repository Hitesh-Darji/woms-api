using AutoMapper;
using MediatR;
using WOMS.Application.Features.StockRequest.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.StockRequest.Queries.GetStockRequestById
{
    public class GetStockRequestByIdQueryHandler : IRequestHandler<GetStockRequestByIdQuery, StockRequestDto?>
    {
        private readonly IStockRequestRepository _stockRequestRepository;
        private readonly IMapper _mapper;

        public GetStockRequestByIdQueryHandler(
            IStockRequestRepository stockRequestRepository,
            IMapper mapper)
        {
            _stockRequestRepository = stockRequestRepository;
            _mapper = mapper;
        }

        public async Task<StockRequestDto?> Handle(GetStockRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var stockRequest = await _stockRequestRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);
            if (stockRequest == null)
            {
                return null;
            }

            return _mapper.Map<StockRequestDto>(stockRequest);
        }
    }
}

