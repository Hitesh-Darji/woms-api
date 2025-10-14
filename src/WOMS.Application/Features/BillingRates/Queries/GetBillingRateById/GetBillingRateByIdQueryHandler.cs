using AutoMapper;
using MediatR;
using WOMS.Application.Features.BillingRates.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingRates.Queries.GetBillingRateById
{
    public class GetBillingRateByIdQueryHandler : IRequestHandler<GetBillingRateByIdQuery, BillingRateDto?>
    {
        private readonly IRateTableRepository _rateTableRepository;
        private readonly AutoMapper.IMapper _mapper;

        public GetBillingRateByIdQueryHandler(IRateTableRepository rateTableRepository, AutoMapper.IMapper mapper)
        {
            _rateTableRepository = rateTableRepository;
            _mapper = mapper;
        }

        public async Task<BillingRateDto?> Handle(GetBillingRateByIdQuery request, CancellationToken cancellationToken)
        {
            var rateTable = await _rateTableRepository.GetByIdAsync(request.Id, cancellationToken);
            return rateTable == null ? null : _mapper.Map<BillingRateDto>(rateTable);
        }
    }
}
