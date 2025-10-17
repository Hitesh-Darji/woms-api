using AutoMapper;
using WOMS.Application.Features.Location.DTOs;
using WOMS.Application.Features.Location.Commands.CreateLocation;
using WOMS.Application.Features.Location.Commands.UpdateLocation;
using WOMS.Domain.Enums;

namespace WOMS.Application.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            // Map from Location entity to LocationDto
            CreateMap<WOMS.Domain.Entities.Location, LocationDto>()
                .ForMember(dest => dest.TypeDescription, opt => opt.MapFrom(src => GetLocationTypeDescription(src.Type)))
                .ForMember(dest => dest.ParentLocationName, opt => opt.MapFrom(src => src.ParentLocation != null ? src.ParentLocation.Name : null))
                .ForMember(dest => dest.SubLocations, opt => opt.MapFrom(src => src.SubLocations.Where(s => !s.IsDeleted)));

            CreateMap<CreateLocationDto, WOMS.Domain.Entities.Location>();
            CreateMap<UpdateLocationDto, WOMS.Domain.Entities.Location>();

            // Map from DTOs to Commands
            CreateMap<CreateLocationDto, CreateLocationCommand>();
            CreateMap<UpdateLocationDto, UpdateLocationCommand>();
        }

        private static string GetLocationTypeDescription(LocationType type)
        {
            return type switch
            {
                LocationType.Warehouse => "Warehouse",
                LocationType.Office => "Office",
                LocationType.Field => "Field",
                LocationType.Vehicle => "Vehicle",
                LocationType.CustomerSite => "Customer Site",
                LocationType.Supplier => "Supplier",
                LocationType.Other => "Other",
                _ => type.ToString()
            };
        }
    }
}
