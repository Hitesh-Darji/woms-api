using AutoMapper;
using WOMS.Application.Features.WorkOrder.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Profiles
{
    public class WorkOrderProfile : Profile
    {
        public WorkOrderProfile()
        {
            // Map from WorkOrder entity to WorkOrderDto
            CreateMap<WorkOrder, WorkOrderDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedOn))
                .ForMember(dest => dest.WorkOrderTypeName, opt => opt.MapFrom(src => src.WorkOrderType != null ? src.WorkOrderType.Name : null));
        }
    }
}
