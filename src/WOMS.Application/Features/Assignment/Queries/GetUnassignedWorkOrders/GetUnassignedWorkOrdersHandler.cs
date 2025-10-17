using AutoMapper;
using MediatR;
using WOMS.Application.Features.Assignment.DTOs;
using WOMS.Application.Features.Assignment.Queries.GetUnassignedWorkOrders;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Assignment.Queries.GetUnassignedWorkOrders
{
    public class GetUnassignedWorkOrdersHandler : IRequestHandler<GetUnassignedWorkOrdersQuery, UnassignedWorkOrdersResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IMapper _mapper;

        public GetUnassignedWorkOrdersHandler(IWorkOrderRepository workOrderRepository, IMapper mapper)
        {
            _workOrderRepository = workOrderRepository;
            _mapper = mapper;
        }

        public async Task<UnassignedWorkOrdersResponse> Handle(GetUnassignedWorkOrdersQuery request, CancellationToken cancellationToken)
        {
            // Get unassigned work orders (where Assignee is null or empty)
            var unassignedWorkOrders = await _workOrderRepository.GetUnassignedWorkOrdersAsync(
                request.Priority, 
                request.WorkType, 
                request.Location, 
                cancellationToken);

            var workOrderDtos = unassignedWorkOrders.Select(wo => new UnassignedWorkOrderDto
            {
                WorkOrderId = wo.Id,
                WorkOrderNumber = wo.WorkOrderNumber,
                Customer = wo.Customer,
                WorkType = wo.Type.ToString(),
                Location = wo.Location ?? "",
                Priority = wo.Priority.ToString(),
                CreatedDate = wo.CreatedDate ?? DateTime.UtcNow,
                DueDate = wo.DueDate,
                Description = wo.Description,
                RequiredSkills = ParseSkills(wo.Tags),
                RequiredEquipment = ParseEquipment(wo.Equipment),
                Recommendation = GenerateRecommendation(wo),
                AvailableTechnicians = GetAvailableTechnicians(wo)
            }).ToList();

            return new UnassignedWorkOrdersResponse
            {
                WorkOrders = workOrderDtos,
                TotalCount = workOrderDtos.Count,
                CriticalCount = workOrderDtos.Count(w => w.Priority == "Critical"),
                HighCount = workOrderDtos.Count(w => w.Priority == "High"),
                MediumCount = workOrderDtos.Count(w => w.Priority == "Medium"),
                LowCount = workOrderDtos.Count(w => w.Priority == "Low")
            };
        }

        private static List<string> ParseSkills(string? tagsJson)
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

        private static List<string> ParseEquipment(string? equipment)
        {
            if (string.IsNullOrEmpty(equipment))
                return new List<string>();

            return equipment.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim())
                .ToList();
        }

        private static AssignmentRecommendationDto? GenerateRecommendation(Domain.Entities.WorkOrder workOrder)
        {
            // In a real implementation, this would use an algorithm to find the best technician
            // For now, return a mock recommendation
            return new AssignmentRecommendationDto
            {
                TechnicianName = "John Smith",
                TechnicianId = "tech123",
                MatchScore = 85.5m,
                Reason = "Skills match: Installation",
                MatchingSkills = new List<string> { "Installation", "Repair" },
                HasRequiredEquipment = true,
                DistanceFromLocation = 2.5m,
                CurrentWorkload = 3,
                MaxWorkload = 6
            };
        }

        private static List<TechnicianOptionDto> GetAvailableTechnicians(Domain.Entities.WorkOrder workOrder)
        {
            // In a real implementation, this would query available technicians
            // For now, return mock data
            return new List<TechnicianOptionDto>
            {
                new TechnicianOptionDto
                {
                    TechnicianId = "tech123",
                    TechnicianName = "John Smith",
                    CurrentWorkload = 3,
                    MaxWorkload = 6,
                    Status = "available",
                    Location = "Downtown",
                    Skills = new List<string> { "Installation", "Repair", "Maintenance" },
                    Equipment = new List<string> { "Meter Kit", "Safety Gear" },
                    MatchScore = 85.5m
                },
                new TechnicianOptionDto
                {
                    TechnicianId = "tech456",
                    TechnicianName = "Mike Wilson",
                    CurrentWorkload = 2,
                    MaxWorkload = 7,
                    Status = "available",
                    Location = "Residential",
                    Skills = new List<string> { "Emergency Repair", "Installation" },
                    Equipment = new List<string> { "Emergency Kit", "Power Tools" },
                    MatchScore = 78.2m
                }
            };
        }
    }
}
