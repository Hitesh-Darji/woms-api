using MediatR;
using WOMS.Application.Features.Location.DTOs;

namespace WOMS.Application.Features.Location.Commands.CreateLocation
{
    public class CreateLocationCommand : IRequest<LocationDto>
    {
        public string Name { get; set; } = string.Empty;
        public Domain.Enums.LocationType Type { get; set; } = Domain.Enums.LocationType.Warehouse;
        public string Address { get; set; } = string.Empty;
        public string Manager { get; set; } = string.Empty;
        public Guid? ParentLocationId { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid? CreatedBy { get; set; }
    }
}
