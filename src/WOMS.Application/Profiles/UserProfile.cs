using AutoMapper;
using WOMS.Application.Features.Users.DTOs;
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
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedOn));
        }
    }
}
