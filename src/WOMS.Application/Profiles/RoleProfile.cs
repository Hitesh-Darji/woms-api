using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WOMS.Application.Features.Roles.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<ApplicationRole, RoleDto>();
        }
    }
}
