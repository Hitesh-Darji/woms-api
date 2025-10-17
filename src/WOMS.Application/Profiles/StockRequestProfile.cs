using AutoMapper;
using WOMS.Application.Features.StockRequest.Commands.CreateStockRequest;
using WOMS.Application.Features.StockRequest.Commands.UpdateStockRequest;
using WOMS.Application.Features.StockRequest.Commands.ApproveStockRequest;
using WOMS.Application.Features.StockRequest.DTOs;
using WOMS.Domain.Entities;
using WOMS.Domain.Enums;
using WOMS.Domain.Extensions;

namespace WOMS.Application.Profiles
{
    public class StockRequestProfile : Profile
    {
        public StockRequestProfile()
        {
            // Map from StockRequest entity to StockRequestDto
            CreateMap<StockRequest, StockRequestDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RequesterId, opt => opt.MapFrom(src => src.RequesterId))
                .ForMember(dest => dest.RequesterName, opt => opt.MapFrom(src => src.RequesterId)) // Will be populated by handler if needed
                .ForMember(dest => dest.FromLocationId, opt => opt.MapFrom(src => src.FromLocationId))
                .ForMember(dest => dest.FromLocationName, opt => opt.MapFrom(src => src.FromLocation.Name))
                .ForMember(dest => dest.ToLocationId, opt => opt.MapFrom(src => src.ToLocationId))
                .ForMember(dest => dest.ToLocationName, opt => opt.MapFrom(src => src.ToLocation.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => src.Status.GetDescription()))
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
                .ForMember(dest => dest.ApprovedBy, opt => opt.MapFrom(src => src.ApprovedBy))
                .ForMember(dest => dest.ApprovedByName, opt => opt.MapFrom(src => src.ApprovedBy)) // Will be populated by handler if needed
                .ForMember(dest => dest.ApprovalDate, opt => opt.MapFrom(src => src.ApprovalDate))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.WorkOrderId, opt => opt.MapFrom(src => src.WorkOrderId))
                .ForMember(dest => dest.WorkOrderNumber, opt => opt.MapFrom(src => src.WorkOrder != null ? src.WorkOrder.WorkOrderNumber : null))
                .ForMember(dest => dest.RequestItems, opt => opt.MapFrom(src => src.RequestItems))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => src.UpdatedOn))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.ToString()))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy.ToString()));

            // Map from RequestItem entity to RequestItemDto
            CreateMap<RequestItem, RequestItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.RequestId))
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.ItemPartNumber, opt => opt.MapFrom(src => src.Item.PartNumber))
                .ForMember(dest => dest.ItemDescription, opt => opt.MapFrom(src => src.Item.Description))
                .ForMember(dest => dest.ItemCategory, opt => opt.MapFrom(src => src.Item.Category))
                .ForMember(dest => dest.ItemManufacturer, opt => opt.MapFrom(src => src.Item.Manufacturer))
                .ForMember(dest => dest.ItemUnitOfMeasure, opt => opt.MapFrom(src => src.Item.UnitOfMeasure))
                .ForMember(dest => dest.ItemUnitCost, opt => opt.MapFrom(src => src.Item.UnitCost))
                .ForMember(dest => dest.RequestedQuantity, opt => opt.MapFrom(src => src.RequestedQuantity))
                .ForMember(dest => dest.ApprovedQuantity, opt => opt.MapFrom(src => src.ApprovedQuantity))
                .ForMember(dest => dest.FulfilledQuantity, opt => opt.MapFrom(src => src.FulfilledQuantity))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.OrderIndex, opt => opt.MapFrom(src => src.OrderIndex));

            // Map from CreateStockRequestCommand to StockRequest entity
            CreateMap<CreateStockRequestCommand, StockRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RequesterId, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.FromLocationId, opt => opt.MapFrom(src => src.FromLocationId))
                .ForMember(dest => dest.ToLocationId, opt => opt.MapFrom(src => src.ToLocationId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StockRequestStatus.Pending))
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ApprovedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ApprovalDate, opt => opt.Ignore())
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.WorkOrderId, opt => opt.MapFrom(src => src.WorkOrderId))
                .ForMember(dest => dest.RequestItems, opt => opt.Ignore()) // Will be handled manually
                .ForMember(dest => dest.FromLocation, opt => opt.Ignore())
                .ForMember(dest => dest.ToLocation, opt => opt.Ignore())
                .ForMember(dest => dest.WorkOrder, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedOn, opt => opt.Ignore());

            // Map from UpdateStockRequestCommand to StockRequest entity
            CreateMap<UpdateStockRequestCommand, StockRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RequesterId, opt => opt.Ignore())
                .ForMember(dest => dest.FromLocationId, opt => opt.Ignore())
                .ForMember(dest => dest.ToLocationId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.RequestDate, opt => opt.Ignore())
                .ForMember(dest => dest.ApprovedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ApprovalDate, opt => opt.Ignore())
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.WorkOrderId, opt => opt.Ignore())
                .ForMember(dest => dest.RequestItems, opt => opt.Ignore()) // Will be handled manually
                .ForMember(dest => dest.FromLocation, opt => opt.Ignore())
                .ForMember(dest => dest.ToLocation, opt => opt.Ignore())
                .ForMember(dest => dest.WorkOrder, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedOn, opt => opt.Ignore());

            // Map from ApproveStockRequestCommand to StockRequest entity
            CreateMap<ApproveStockRequestCommand, StockRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RequesterId, opt => opt.Ignore())
                .ForMember(dest => dest.FromLocationId, opt => opt.Ignore())
                .ForMember(dest => dest.ToLocationId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StockRequestStatus.Approved))
                .ForMember(dest => dest.RequestDate, opt => opt.Ignore())
                .ForMember(dest => dest.ApprovedBy, opt => opt.MapFrom(src => src.ApprovedBy))
                .ForMember(dest => dest.ApprovalDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.ApprovalNotes))
                .ForMember(dest => dest.WorkOrderId, opt => opt.Ignore())
                .ForMember(dest => dest.RequestItems, opt => opt.Ignore()) // Will be handled manually
                .ForMember(dest => dest.FromLocation, opt => opt.Ignore())
                .ForMember(dest => dest.ToLocation, opt => opt.Ignore())
                .ForMember(dest => dest.WorkOrder, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedOn, opt => opt.Ignore());

            CreateMap<StockRequest, StockRequestDto>().ReverseMap();
        }
    }
}
