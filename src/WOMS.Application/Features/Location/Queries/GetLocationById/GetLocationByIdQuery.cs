using MediatR;
using WOMS.Application.Features.Location.DTOs;

namespace WOMS.Application.Features.Location.Queries.GetLocationById
{
    public class GetLocationByIdQuery : IRequest<LocationDto?>
    {
        public Guid Id { get; set; }
    }
}

