using MediatR;
using WOMS.Application.Features.RouteOptimization.Commands.ReorderWorkOrders;
using WOMS.Application.Features.RouteOptimization.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.RouteOptimization.Commands.ReorderWorkOrders
{
    public class ReorderWorkOrdersHandler : IRequestHandler<ReorderWorkOrdersCommand, ReorderWorkOrdersResponse>
    {
        private readonly IRouteRepository _routeRepository;

        public ReorderWorkOrdersHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<ReorderWorkOrdersResponse> Handle(ReorderWorkOrdersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Get the route with its stops
                var route = await _routeRepository.GetRouteWithStopsAsync(request.RouteId, cancellationToken);

                if (route == null)
                {
                    return new ReorderWorkOrdersResponse
                    {
                        Success = false,
                        Message = "Route not found"
                    };
                }

                // Get all route stops ordered by sequence
                var routeStops = route.RouteStops
                    .Where(rs => !rs.IsDeleted)
                    .OrderBy(rs => rs.SequenceNumber)
                    .ToList();

                // Find the current work order's position
                var currentStop = routeStops.FirstOrDefault(rs => rs.WorkOrderId == request.WorkOrderId);
                if (currentStop == null)
                {
                    return new ReorderWorkOrdersResponse
                    {
                        Success = false,
                        Message = "Work order not found in route"
                    };
                }

                var currentIndex = routeStops.IndexOf(currentStop);

                // Calculate new position based on direction
                int newIndex;
                if (request.Direction == ReorderDirection.Up)
                {
                    newIndex = Math.Max(0, currentIndex - 1);
                }
                else // Down
                {
                    newIndex = Math.Min(routeStops.Count - 1, currentIndex + 1);
                }

                // If position hasn't changed, return current order
                if (currentIndex == newIndex)
                {
                    return new ReorderWorkOrdersResponse
                    {
                        RouteId = request.RouteId,
                        WorkOrders = MapToWorkOrderSequence(routeStops),
                        Success = true,
                        Message = "Work order is already at the desired position"
                    };
                }

                // Reorder the stops
                var itemToMove = routeStops[currentIndex];
                routeStops.RemoveAt(currentIndex);
                routeStops.Insert(newIndex, itemToMove);

                // Update sequence numbers
                for (int i = 0; i < routeStops.Count; i++)
                {
                    routeStops[i].SequenceNumber = i + 1;
                    routeStops[i].UpdatedOn = DateTime.UtcNow;
                }

                // Save changes
                await _routeRepository.UpdateAsync(route, cancellationToken);

                return new ReorderWorkOrdersResponse
                {
                    RouteId = request.RouteId,
                    WorkOrders = MapToWorkOrderSequence(routeStops),
                    Success = true,
                    Message = $"Work order moved {(request.Direction == ReorderDirection.Up ? "up" : "down")} successfully"
                };
            }
            catch (Exception ex)
            {
                return new ReorderWorkOrdersResponse
                {
                    Success = false,
                    Message = $"Error reordering work orders: {ex.Message}"
                };
            }
        }

        private static List<WorkOrderSequenceDto> MapToWorkOrderSequence(List<Domain.Entities.RouteStop> routeStops)
        {
            return routeStops.Select(rs => new WorkOrderSequenceDto
            {
                WorkOrderId = rs.WorkOrderId,
                WorkOrderNumber = rs.WorkOrder?.WorkOrderNumber ?? "N/A",
                Customer = rs.WorkOrder?.Customer ?? "N/A",
                Location = rs.WorkOrder?.Location ?? "N/A",
                Sequence = rs.SequenceNumber,
                TimeWindow = FormatTimeWindow(rs.ScheduledStartTime, rs.ScheduledEndTime),
                EstimatedDuration = FormatDuration(rs.EstimatedDuration),
                EquipmentRequired = rs.WorkOrder?.Equipment ?? ""
            }).ToList();
        }

        private static string FormatTimeWindow(DateTime? startTime, DateTime? endTime)
        {
            if (startTime.HasValue && endTime.HasValue)
            {
                return $"{startTime.Value:HH:mm} - {endTime.Value:HH:mm}";
            }
            return "";
        }

        private static string FormatDuration(decimal duration)
        {
            return $"{duration:F1}h";
        }
    }
}