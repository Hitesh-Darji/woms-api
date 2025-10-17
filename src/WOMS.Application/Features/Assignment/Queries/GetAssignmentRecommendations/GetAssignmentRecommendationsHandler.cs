using MediatR;
using WOMS.Application.Features.Assignment.DTOs;
using WOMS.Application.Features.Assignment.Queries.GetAssignmentRecommendations;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Assignment.Queries.GetAssignmentRecommendations
{
    public class GetAssignmentRecommendationsHandler : IRequestHandler<GetAssignmentRecommendationsQuery, AssignmentRecommendationsDto?>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITechnicianEquipmentRepository _technicianEquipmentRepository;
        private readonly IDistanceCalculationService _distanceCalculationService;

        public GetAssignmentRecommendationsHandler(IWorkOrderRepository workOrderRepository, IUserRepository userRepository, ITechnicianEquipmentRepository technicianEquipmentRepository, IDistanceCalculationService distanceCalculationService)
        {
            _workOrderRepository = workOrderRepository;
            _userRepository = userRepository;
            _technicianEquipmentRepository = technicianEquipmentRepository;
            _distanceCalculationService = distanceCalculationService;
        }

        public async Task<AssignmentRecommendationsDto?> Handle(GetAssignmentRecommendationsQuery request, CancellationToken cancellationToken)
        {
            // Get work order
            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId, cancellationToken);
            if (workOrder == null || workOrder.IsDeleted)
                return null;

            // Get available technicians
            var technicians = await _userRepository.GetTechniciansAsync(null, null, cancellationToken);

            // Generate recommendations
            var recommendations = new List<AssignmentRecommendationDto>();
            foreach (var t in technicians.Where(t => IsTechnicianAvailable(t)))
            {
                var recommendation = new AssignmentRecommendationDto
                {
                    TechnicianName = $"{t.FirstName} {t.LastName}",
                    TechnicianId = t.Id,
                    MatchScore = await CalculateMatchScore(workOrder, t),
                    Reason = GenerateReason(workOrder, t),
                    MatchingSkills = GetMatchingSkills(workOrder, t),
                    HasRequiredEquipment = await HasRequiredEquipment(workOrder, t),
                    DistanceFromLocation = await CalculateDistance(workOrder, t, cancellationToken),
                    CurrentWorkload = await GetCurrentWorkload(t),
                    MaxWorkload = GetMaxWorkload(t)
                };
                recommendations.Add(recommendation);
            }

            var topRecommendations = recommendations
                .OrderByDescending(r => r.MatchScore)
                .Take(5)
                .ToList();

            // Get all available technicians for dropdown
            var allTechnicians = new List<TechnicianOptionDto>();
            foreach (var t in technicians.Where(t => IsTechnicianAvailable(t)))
            {
                var technicianOption = new TechnicianOptionDto
                {
                    TechnicianId = t.Id,
                    TechnicianName = $"{t.FirstName} {t.LastName}",
                    CurrentWorkload = await GetCurrentWorkload(t),
                    MaxWorkload = GetMaxWorkload(t),
                    Status = await GetTechnicianStatus(t),
                    Location = t.City ?? "Unknown",
                    Skills = ParseSkills(t.Skills),
                    Equipment = await GetTechnicianEquipment(t, cancellationToken),
                    MatchScore = await CalculateMatchScore(workOrder, t)
                };
                allTechnicians.Add(technicianOption);
            }

            var orderedTechnicians = allTechnicians
                .OrderByDescending(t => t.MatchScore)
                .ToList();

            return new AssignmentRecommendationsDto
            {
                WorkOrderId = workOrder.Id,
                WorkOrderNumber = workOrder.WorkOrderNumber,
                Recommendations = topRecommendations,
                AllAvailableTechnicians = orderedTechnicians
            };
        }

        private static bool IsTechnicianAvailable(Domain.Entities.ApplicationUser technician)
        {
            return technician.IsActive && !technician.IsDeleted;
        }

        private async Task<decimal> CalculateMatchScore(Domain.Entities.WorkOrder workOrder, Domain.Entities.ApplicationUser technician)
        {
            decimal score = 50; // Base score

            // Skill match scoring
            var matchingSkills = GetMatchingSkills(workOrder, technician);
            score += matchingSkills.Count * 10;

            // Location proximity scoring
            if (workOrder.Location == technician.City)
            {
                score += 20;
            }

            // Workload scoring (prefer technicians with lower workload)
            var currentWorkload = await GetCurrentWorkload(technician);
            var maxWorkload = GetMaxWorkload(technician);
            var workloadRatio = (decimal)currentWorkload / maxWorkload;
            score += (1 - workloadRatio) * 20;

            return Math.Min(score, 100);
        }

        private static string GenerateReason(Domain.Entities.WorkOrder workOrder, Domain.Entities.ApplicationUser technician)
        {
            var reasons = new List<string>();

            var matchingSkills = GetMatchingSkills(workOrder, technician);
            if (matchingSkills.Any())
            {
                reasons.Add($"Skills match: {string.Join(", ", matchingSkills)}");
            }

            if (workOrder.Location == technician.City)
            {
                reasons.Add("Same location");
            }

            return reasons.Any() ? string.Join(", ", reasons) : "General availability";
        }

        private static List<string> GetMatchingSkills(Domain.Entities.WorkOrder workOrder, Domain.Entities.ApplicationUser technician)
        {
            // Simplified skill matching - in real implementation, this would be more sophisticated
            var workOrderSkills = ParseSkills(workOrder.Tags);
            var technicianSkills = ParseSkills(technician.Skills);

            return workOrderSkills.Intersect(technicianSkills).ToList();
        }

        private async Task<bool> HasRequiredEquipment(Domain.Entities.WorkOrder workOrder, Domain.Entities.ApplicationUser technician)
        {
            // Check if work order requires specific equipment
            if (string.IsNullOrEmpty(workOrder.Equipment))
                return true; // No equipment required

            // Check if technician has the required equipment
            return await _technicianEquipmentRepository.HasEquipmentAsync(technician.Id, workOrder.Equipment);
        }

        private async Task<decimal> CalculateDistance(Domain.Entities.WorkOrder workOrder, Domain.Entities.ApplicationUser technician, CancellationToken cancellationToken)
        {
            // Use real distance calculation service
            return await _distanceCalculationService.CalculateDistanceAsync(
                workOrder.Location ?? "Unknown", 
                technician.City ?? "Unknown", 
                cancellationToken);
        }

        private async Task<int> GetCurrentWorkload(Domain.Entities.ApplicationUser technician)
        {
            // Get actual assigned work orders count from database
            var assignedWorkOrders = await _workOrderRepository.GetByAssignedUserAsync(technician.Id);
            return assignedWorkOrders.Count();
        }

        private static int GetMaxWorkload(Domain.Entities.ApplicationUser technician)
        {
            // Simplified max workload - in real implementation, this would be configurable per technician
            return 6;
        }

        private async Task<string> GetTechnicianStatus(Domain.Entities.ApplicationUser technician)
        {
            // Check if technician is active and not deleted
            if (!technician.IsActive || technician.IsDeleted)
                return "offline";

            // Get current workload
            var assignedWorkOrders = await _workOrderRepository.GetByAssignedUserAsync(technician.Id);
            var currentWorkload = assignedWorkOrders.Count();
            var maxWorkload = GetMaxWorkload(technician);

            // Check if technician is at capacity
            if (currentWorkload >= maxWorkload)
                return "busy";

            // Check shift schedule (simplified - in real implementation, this would check actual shift data)
            var currentTime = DateTime.Now.TimeOfDay;
            var shiftStart = TimeSpan.FromHours(8); // 8 AM
            var shiftEnd = TimeSpan.FromHours(17); // 5 PM

            if (currentTime < shiftStart || currentTime > shiftEnd)
                return "offline";

            // Check break schedule (simplified - in real implementation, this would check actual break data)
            var breakStart = TimeSpan.FromHours(12); // 12 PM
            var breakEnd = TimeSpan.FromHours(13); // 1 PM

            if (currentTime >= breakStart && currentTime <= breakEnd)
                return "break";

            return "available";
        }

        private static List<string> ParseSkills(string? skillsJson)
        {
            if (string.IsNullOrEmpty(skillsJson))
                return new List<string>();

            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<List<string>>(skillsJson) ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        private async Task<List<string>> GetTechnicianEquipment(Domain.Entities.ApplicationUser technician, CancellationToken cancellationToken)
        {
            // Get actual equipment from database
            var equipmentNames = await _technicianEquipmentRepository.GetTechnicianEquipmentNamesAsync(technician.Id, cancellationToken);
            return equipmentNames.ToList();
        }
    }
}
