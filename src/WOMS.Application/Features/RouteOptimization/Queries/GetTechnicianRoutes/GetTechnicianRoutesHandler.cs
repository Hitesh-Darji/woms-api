using AutoMapper;
using MediatR;
using WOMS.Application.Features.RouteOptimization.DTOs;
using WOMS.Application.Features.RouteOptimization.Queries.GetTechnicianRoutes;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.RouteOptimization.Queries.GetTechnicianRoutes
{
    public class GetTechnicianRoutesHandler : IRequestHandler<GetTechnicianRoutesQuery, TechnicianRoutesResponse>
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IMapper _mapper;

        public GetTechnicianRoutesHandler(IRouteRepository routeRepository, IMapper mapper)
        {
            _routeRepository = routeRepository;
            _mapper = mapper;
        }

        public async Task<TechnicianRoutesResponse> Handle(GetTechnicianRoutesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.Route> routes;

            if (!string.IsNullOrEmpty(request.TechnicianId))
            {
                routes = await _routeRepository.GetRoutesByTechnicianAsync(request.TechnicianId, request.Date, cancellationToken);
            }
            else
            {
                routes = await _routeRepository.GetRoutesByDateAsync(request.Date, cancellationToken);
            }

            var routeDtos = routes.Select(route => new TechnicianRouteDto
            {
                RouteId = route.Id,
                TechnicianName = $"{route.Driver.FirstName} {route.Driver.LastName}",
                TechnicianId = route.DriverId,
                TotalDistance = route.TotalDistance,
                TotalTime = route.TotalTime,
                TotalStops = route.TotalStops,
                Efficiency = route.Efficiency,
                Status = route.Status,
                Constraints = route.Constraints,
                WorkOrders = route.RouteStops
                    .OrderBy(rs => rs.SequenceNumber)
                    .Select(rs => new WorkOrderAssignmentDto
                    {
                        WorkOrderId = rs.WorkOrderId,
                        WorkOrderNumber = rs.WorkOrder.WorkOrderNumber,
                        Customer = rs.WorkOrder.Customer,
                        Address = rs.WorkOrder.Address ?? "",
                        SequenceNumber = rs.SequenceNumber,
                        EstimatedDuration = rs.EstimatedDuration,
                        ScheduledStartTime = rs.ScheduledStartTime,
                        ScheduledEndTime = rs.ScheduledEndTime,
                        TimeWindow = rs.ScheduledStartTime.HasValue && rs.ScheduledEndTime.HasValue 
                            ? $"{rs.ScheduledStartTime.Value:HH:mm} - {rs.ScheduledEndTime.Value:HH:mm}"
                            : null,
                        Tags = ParseTags(rs.WorkOrder.Tags),
                        Equipment = rs.WorkOrder.Equipment,
                        Status = rs.Status
                    })
                    .ToList()
            }).ToList();

            return new TechnicianRoutesResponse
            {
                Routes = routeDtos,
                TotalCount = routeDtos.Count
            };
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
