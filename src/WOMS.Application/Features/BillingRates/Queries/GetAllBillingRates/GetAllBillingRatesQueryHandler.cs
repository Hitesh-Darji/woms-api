using AutoMapper;
using MediatR;
using WOMS.Application.Features.BillingRates.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingRates.Queries.GetAllBillingRates
{
    public class GetAllBillingRatesQueryHandler : IRequestHandler<GetAllBillingRatesQuery, IEnumerable<BillingRateDto>>
    {
        private readonly IRateTableRepository _rateTableRepository;
        private readonly AutoMapper.IMapper _mapper;

        public GetAllBillingRatesQueryHandler(IRateTableRepository rateTableRepository, AutoMapper.IMapper mapper)
        {
            _rateTableRepository = rateTableRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BillingRateDto>> Handle(GetAllBillingRatesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<WOMS.Domain.Entities.RateTable> rateTables;

            if (request.IsActive.HasValue && request.IsActive.Value)
            {
                rateTables = await _rateTableRepository.GetActiveRateTablesAsync(cancellationToken);
            }
            else if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                rateTables = await _rateTableRepository.GetRateTablesByDateRangeAsync(request.StartDate.Value, request.EndDate.Value, cancellationToken);
            }
            else
            {
                rateTables = await _rateTableRepository.GetAllAsync(cancellationToken);
            }

            return _mapper.Map<IEnumerable<BillingRateDto>>(rateTables);
        }
    }
}
