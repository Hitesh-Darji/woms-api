using AutoMapper;
using MediatR;
using WOMS.Application.Features.Location.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Location.Queries.GetLocationById
{
    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, LocationDto?>
    {
        private readonly IRepository<WOMS.Domain.Entities.Location> _locationRepository;
        private readonly AutoMapper.IMapper _mapper;

        public GetLocationByIdQueryHandler(IRepository<WOMS.Domain.Entities.Location> locationRepository, AutoMapper.IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<LocationDto?> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(request.Id);
            
            if (location == null || location.IsDeleted)
            {
                return null;
            }

            return _mapper.Map<LocationDto>(location);
        }
    }
}
