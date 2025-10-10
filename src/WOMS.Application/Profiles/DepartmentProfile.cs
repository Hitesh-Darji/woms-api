using AutoMapper;
using WOMS.Application.Features.Departments.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();
            
            CreateMap<Department, DepartmentDto>();
        }
    }
}
