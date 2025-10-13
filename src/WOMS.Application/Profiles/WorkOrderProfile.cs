using AutoMapper;
using WOMS.Application.Features.WorkOrder.DTOs;
using WOMS.Domain.Entities;
using WOMS.Domain.Enums;

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
                .ForMember(dest => dest.WorkOrderTypeName, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.AssignedTechnicianName, opt => opt.MapFrom(src => src.Assignee))
                .ForMember(dest => dest.AssignedTechnicianId, opt => opt.MapFrom(src => src.Assignee))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.ServiceAddress, opt => opt.MapFrom(src => src.Address));

            // Map from WorkOrderType entity to WorkOrderTypeDto
            CreateMap<WOMS.Domain.Entities.WorkOrderType, WorkOrderTypeDto>();
        }
    }
}
