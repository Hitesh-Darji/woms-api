using MediatR;
using WOMS.Application.Features.Location.DTOs;

namespace WOMS.Application.Features.Location.Queries.GetAllLocations
{
    public class GetAllLocationsQuery : IRequest<List<LocationDto>>
    {
        public bool IncludeInactive { get; set; } = false;
    }
}

