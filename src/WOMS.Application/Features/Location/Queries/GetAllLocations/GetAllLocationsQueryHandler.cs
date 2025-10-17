using AutoMapper;
using MediatR;
using WOMS.Application.Features.Location.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Location.Queries.GetAllLocations
{
    public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, List<LocationDto>>
    {
        private readonly IRepository<WOMS.Domain.Entities.Location> _locationRepository;
        private readonly AutoMapper.IMapper _mapper;

        public GetAllLocationsQueryHandler(IRepository<WOMS.Domain.Entities.Location> locationRepository, AutoMapper.IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<List<LocationDto>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _locationRepository.GetAllAsync();
            
            if (!request.IncludeInactive)
            {
                locations = locations.Where(l => l.IsActive && !l.IsDeleted).ToList();
            }
            else
            {
                locations = locations.Where(l => !l.IsDeleted).ToList();
            }

            var locationDtos = _mapper.Map<List<LocationDto>>(locations);
            
            // Build hierarchy
            return BuildLocationHierarchy(locationDtos);
        }

        private List<LocationDto> BuildLocationHierarchy(List<LocationDto> locations)
        {
            var locationDict = locations.ToDictionary(l => l.Id);
            var rootLocations = new List<LocationDto>();

            foreach (var location in locations)
            {
                if (location.ParentLocationId == null)
                {
                    rootLocations.Add(location);
                }
                else if (locationDict.ContainsKey(location.ParentLocationId.Value))
                {
                    var parent = locationDict[location.ParentLocationId.Value];
                    parent.SubLocations.Add(location);
                    location.ParentLocationName = parent.Name;
                }
            }

            return rootLocations;
        }
    }
}
