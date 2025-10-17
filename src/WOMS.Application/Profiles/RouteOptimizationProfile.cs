using AutoMapper;
using WOMS.Application.Features.RouteOptimization.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Profiles
{
    public class RouteOptimizationProfile : Profile
    {
        public RouteOptimizationProfile()
        {
            CreateMap<Route, TechnicianRouteDto>()
                .ForMember(dest => dest.TechnicianName, opt => opt.MapFrom(src => $"{src.Driver.FirstName} {src.Driver.LastName}"))
                .ForMember(dest => dest.TechnicianId, opt => opt.MapFrom(src => src.DriverId))
                .ForMember(dest => dest.WorkOrders, opt => opt.MapFrom(src => src.RouteStops.OrderBy(rs => rs.SequenceNumber)));

            CreateMap<RouteStop, WorkOrderAssignmentDto>()
                .ForMember(dest => dest.WorkOrderId, opt => opt.MapFrom(src => src.WorkOrderId))
                .ForMember(dest => dest.WorkOrderNumber, opt => opt.MapFrom(src => src.WorkOrder.WorkOrderNumber))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.WorkOrder.Customer))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.WorkOrder.Address ?? ""))
                .ForMember(dest => dest.EstimatedDuration, opt => opt.MapFrom(src => src.EstimatedDuration))
                .ForMember(dest => dest.ScheduledStartTime, opt => opt.MapFrom(src => src.ScheduledStartTime))
                .ForMember(dest => dest.ScheduledEndTime, opt => opt.MapFrom(src => src.ScheduledEndTime))
                .ForMember(dest => dest.TimeWindow, opt => opt.MapFrom(src => 
                    src.ScheduledStartTime.HasValue && src.ScheduledEndTime.HasValue 
                        ? $"{src.ScheduledStartTime.Value:HH:mm} - {src.ScheduledEndTime.Value:HH:mm}"
                        : null))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => ParseTags(src.WorkOrder.Tags)))
                .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.WorkOrder.Equipment))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }

        private static List<string> ParseTags(string? tagsJson)
        {
            if (string.IsNullOrEmpty(tagsJson))
                return new List<string>();

            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<List<string>>(tagsJson) ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}
