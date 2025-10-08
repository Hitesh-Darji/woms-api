using AutoMapper;
using WOMS.Application.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, ApplicationUser>();
            
            // Add mapping from ApplicationUser to UserDto
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedOn));
        }
    }
}
