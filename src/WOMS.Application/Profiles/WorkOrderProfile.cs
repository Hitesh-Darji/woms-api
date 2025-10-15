using AutoMapper;
using WOMS.Application.Features.WorkOrder.Commands.CreateWorkOrder;
using WOMS.Application.Features.WorkOrder.Commands.UpdateWorkOrder;
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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.WorkOrderNumber, opt => opt.MapFrom(src => src.WorkOrderNumber))
                .ForMember(dest => dest.WorkflowId, opt => opt.MapFrom(src => src.WorkflowId))
                .ForMember(dest => dest.WorkOrderTypeId, opt => opt.MapFrom(src => (Guid?)null)) // Not mapped from entity
                .ForMember(dest => dest.WorkOrderTypeName, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.ServiceAddress, opt => opt.MapFrom(src => src.Address ?? string.Empty))
                .ForMember(dest => dest.MeterNumber, opt => opt.MapFrom(src => (string?)null)) // Not mapped from entity
                .ForMember(dest => dest.CurrentReading, opt => opt.MapFrom(src => (int?)null)) // Not mapped from entity
                .ForMember(dest => dest.AssignedTechnicianId, opt => opt.MapFrom(src => src.Assignee))
                .ForMember(dest => dest.AssignedTechnicianName, opt => opt.MapFrom(src => src.Assignee))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.ScheduledDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.StartedAt, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.CompletedAt, opt => opt.MapFrom(src => src.Status == WorkOrderStatus.Completed ? src.UpdatedOn : (DateTime?)null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedOn))
                .ForMember(dest => dest.Utility, opt => opt.MapFrom(src => src.Utility))
                .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Make))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate));

            // Map from WorkOrderType entity to WorkOrderTypeDto
            CreateMap<WOMS.Domain.Entities.WorkOrderType, WorkOrderTypeDto>();

            // Map from CreateWorkOrderCommand to WorkOrder entity
            CreateMap<CreateWorkOrderCommand, WorkOrder>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.WorkOrderNumber, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => WorkOrderStatus.Pending))
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.WorkOrderColumns, opt => opt.Ignore())
                .ForMember(dest => dest.WorkOrderAssignments, opt => opt.Ignore())
                .ForMember(dest => dest.FormSubmissions, opt => opt.Ignore())
                .ForMember(dest => dest.InvoiceLineItems, opt => opt.Ignore())
                .ForMember(dest => dest.StockTransactions, opt => opt.Ignore())
                .ForMember(dest => dest.StockRequests, opt => opt.Ignore())
                .ForMember(dest => dest.AssetHistories, opt => opt.Ignore())
                .ForMember(dest => dest.WorkflowInstances, opt => opt.Ignore());

            // Map from UpdateWorkOrderCommand to WorkOrder entity
            CreateMap<UpdateWorkOrderCommand, WorkOrder>()
                .ForMember(dest => dest.WorkOrderNumber, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.WorkOrderColumns, opt => opt.Ignore())
                .ForMember(dest => dest.WorkOrderAssignments, opt => opt.Ignore())
                .ForMember(dest => dest.FormSubmissions, opt => opt.Ignore())
                .ForMember(dest => dest.InvoiceLineItems, opt => opt.Ignore())
                .ForMember(dest => dest.StockTransactions, opt => opt.Ignore())
                .ForMember(dest => dest.StockRequests, opt => opt.Ignore())
                .ForMember(dest => dest.AssetHistories, opt => opt.Ignore())
                .ForMember(dest => dest.WorkflowInstances, opt => opt.Ignore());
        }
    }
}
