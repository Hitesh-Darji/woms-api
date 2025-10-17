using AutoMapper;
using MediatR;
using WOMS.Application.Features.StockRequest.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.StockRequest.Queries.GetAllStockRequests
{
    public class GetAllStockRequestsQueryHandler : IRequestHandler<GetAllStockRequestsQuery, StockRequestListResponse>
    {
        private readonly IStockRequestRepository _stockRequestRepository;
        private readonly IMapper _mapper;

        public GetAllStockRequestsQueryHandler(
            IStockRequestRepository stockRequestRepository,
            IMapper mapper)
        {
            _stockRequestRepository = stockRequestRepository;
            _mapper = mapper;
        }

        public async Task<StockRequestListResponse> Handle(GetAllStockRequestsQuery request, CancellationToken cancellationToken)
        {
            var (stockRequests, totalCount) = await _stockRequestRepository.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                request.SearchTerm,
                request.Status,
                request.FromLocationId,
                request.ToLocationId,
                request.RequesterId,
                request.SortBy,
                request.SortDescending,
                cancellationToken);

            var stockRequestDtos = _mapper.Map<List<StockRequestDto>>(stockRequests);

            return new StockRequestListResponse
            {
                StockRequests = stockRequestDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

