using MediatR;
using WOMS.Application.Features.Assignment.Commands.AutoAssignAll;
using WOMS.Application.Features.Assignment.DTOs;
using WOMS.Application.Features.Assignment.Queries.GetAssignmentRecommendations;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Enums;
using WOMS.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WOMS.Application.Features.Assignment.Commands.AutoAssignAll
{
    public class AutoAssignAllHandler : IRequestHandler<AutoAssignAllCommand, AutoAssignAllResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWorkOrderAssignmentRepository _workOrderAssignmentRepository;
        private readonly ITechnicianEquipmentRepository _technicianEquipmentRepository;
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AutoAssignAllHandler(
            IWorkOrderRepository workOrderRepository, 
            IUserRepository userRepository,
            IWorkOrderAssignmentRepository workOrderAssignmentRepository,
            ITechnicianEquipmentRepository technicianEquipmentRepository,
            IDistanceCalculationService distanceCalculationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _workOrderRepository = workOrderRepository;
            _userRepository = userRepository;
            _workOrderAssignmentRepository = workOrderAssignmentRepository;
            _technicianEquipmentRepository = technicianEquipmentRepository;
            _distanceCalculationService = distanceCalculationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AutoAssignAllResponse> Handle(AutoAssignAllCommand request, CancellationToken cancellationToken)
        {
            // Extract user ID from JWT token claims
            var assignedBy = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "system";

            var response = new AutoAssignAllResponse();

            // Get unassigned work orders
            var unassignedWorkOrders = await _workOrderRepository.GetUnassignedWorkOrdersAsync(
                request.PriorityFilter, null, null, cancellationToken);

            // Get available technicians
            var technicians = await _userRepository.GetTechniciansAsync(null, null, cancellationToken);

            // Sort work orders by priority (Critical > High > Medium > Low)
            var sortedWorkOrders = unassignedWorkOrders.OrderByDescending(wo => wo.Priority).ToList();

            foreach (var workOrder in sortedWorkOrders)
            {
                var bestTechnician = await FindBestTechnicianAsync(workOrder, technicians, cancellationToken);

                if (bestTechnician != null)
                {
                    // Check if technician can handle this assignment
                    var canAssign = await CanAssignWorkOrderAsync(workOrder, bestTechnician, cancellationToken);
                    
                    if (canAssign)
                    {
                        // Create assignment record
                        var assignment = new WorkOrderAssignment
                        {
                            Id = Guid.NewGuid(),
                            WorkOrderId = workOrder.Id,
                            TechnicianId = bestTechnician.Id,
                            AssignedAt = DateTime.UtcNow,
                            AssignedBy = assignedBy,
                            Status = AssignmentStatus.Assigned,
                            Notes = "Auto-assigned",
                            CreatedOn = DateTime.UtcNow,
                            UpdatedOn = DateTime.UtcNow,
                            CreatedBy = Guid.TryParse(assignedBy, out var createdByGuid) ? createdByGuid : null,
                            UpdatedBy = Guid.TryParse(assignedBy, out var updatedByGuid) ? updatedByGuid : null
                        };

                        // Update work order
                        workOrder.Assignee = bestTechnician.Id;
                        workOrder.Status = WorkOrderStatus.Assigned;
                        workOrder.UpdatedOn = DateTime.UtcNow;
                        workOrder.UpdatedBy = Guid.TryParse(assignedBy, out var assignedByGuid) ? assignedByGuid : null;

                        // Save changes
                        await _workOrderAssignmentRepository.AddAsync(assignment, cancellationToken);
                        await _workOrderRepository.UpdateAsync(workOrder, cancellationToken);

                        response.AssignmentResults.Add(new AssignmentResultDto
                        {
                            WorkOrderId = workOrder.Id,
                            WorkOrderNumber = workOrder.WorkOrderNumber,
                            TechnicianId = bestTechnician.Id,
                            TechnicianName = $"{bestTechnician.FirstName} {bestTechnician.LastName}",
                            Success = true,
                            MatchScore = await CalculateMatchScoreAsync(workOrder, bestTechnician, cancellationToken)
                        });

                        response.WorkOrdersAssigned++;
                        response.Messages.Add($"Auto-assigned {workOrder.WorkOrderNumber} to {bestTechnician.FirstName} {bestTechnician.LastName}");
                    }
                    else
                    {
                        response.AssignmentResults.Add(new AssignmentResultDto
                        {
                            WorkOrderId = workOrder.Id,
                            WorkOrderNumber = workOrder.WorkOrderNumber,
                            TechnicianId = bestTechnician.Id,
                            TechnicianName = $"{bestTechnician.FirstName} {bestTechnician.LastName}",
                            Success = false,
                            Reason = "Technician at capacity or unavailable"
                        });

                        response.WorkOrdersFailed++;
                        response.Messages.Add($"Could not assign {workOrder.WorkOrderNumber} - technician unavailable");
                    }
                }
                else
                {
                    response.AssignmentResults.Add(new AssignmentResultDto
                    {
                        WorkOrderId = workOrder.Id,
                        WorkOrderNumber = workOrder.WorkOrderNumber,
                        TechnicianId = "",
                        TechnicianName = "",
                        Success = false,
                        Reason = "No suitable technician found"
                    });

                    response.WorkOrdersFailed++;
                    response.Messages.Add($"No suitable technician found for {workOrder.WorkOrderNumber}");
                }
            }

            return response;
        }

        private async Task<ApplicationUser?> FindBestTechnicianAsync(Domain.Entities.WorkOrder workOrder, IEnumerable<ApplicationUser> technicians, CancellationToken cancellationToken)
        {
            var availableTechnicians = new List<(ApplicationUser technician, decimal score)>();

            foreach (var technician in technicians)
            {
                if (await IsTechnicianAvailableAsync(technician, cancellationToken))
                {
                    var score = await CalculateMatchScoreAsync(workOrder, technician, cancellationToken);
                    availableTechnicians.Add((technician, score));
                }
            }

            // Return technician with highest score
            return availableTechnicians.OrderByDescending(t => t.score).FirstOrDefault().technician;
        }

        private async Task<bool> IsTechnicianAvailableAsync(ApplicationUser technician, CancellationToken cancellationToken)
        {
            // Check if technician is active and not deleted
            if (!technician.IsActive || technician.IsDeleted)
                return false;

            // Check current workload
            var assignedWorkOrders = await _workOrderRepository.GetByAssignedUserAsync(technician.Id, cancellationToken);
            var currentWorkload = assignedWorkOrders.Count();
            var maxWorkload = 6; // Default max workload

            if (currentWorkload >= maxWorkload)
                return false;

            // Check shift schedule (simplified)
            var currentTime = DateTime.Now.TimeOfDay;
            var shiftStart = TimeSpan.FromHours(8); // 8 AM
            var shiftEnd = TimeSpan.FromHours(17); // 5 PM

            if (currentTime < shiftStart || currentTime > shiftEnd)
                return false;

            return true;
        }

        private async Task<bool> CanAssignWorkOrderAsync(Domain.Entities.WorkOrder workOrder, ApplicationUser technician, CancellationToken cancellationToken)
        {
            // Check if technician has required equipment
            if (!string.IsNullOrEmpty(workOrder.Equipment))
            {
                var hasEquipment = await _technicianEquipmentRepository.HasEquipmentAsync(technician.Id, workOrder.Equipment, cancellationToken);
                if (!hasEquipment)
                    return false;
            }

            // Check workload capacity
            var assignedWorkOrders = await _workOrderRepository.GetByAssignedUserAsync(technician.Id, cancellationToken);
            var currentWorkload = assignedWorkOrders.Count();
            var maxWorkload = 6; // Default max workload

            return currentWorkload < maxWorkload;
        }

        private async Task<decimal> CalculateMatchScoreAsync(Domain.Entities.WorkOrder workOrder, ApplicationUser technician, CancellationToken cancellationToken)
        {
            decimal score = 50; // Base score

            // Skill matching
            var matchingSkills = GetMatchingSkills(workOrder, technician);
            score += matchingSkills.Count * 10;

            // Location proximity
            if (workOrder.Location == technician.City)
            {
                score += 20;
            }
            else
            {
                // Calculate distance penalty
                var distance = await _distanceCalculationService.CalculateDistanceAsync(
                    workOrder.Location ?? "Unknown", 
                    technician.City ?? "Unknown", 
                    cancellationToken);
                
                // Reduce score based on distance (max 20 points penalty)
                var distancePenalty = Math.Min(distance / 10, 20);
                score -= distancePenalty;
            }

            // Workload factor (prefer technicians with lower workload)
            var assignedWorkOrders = await _workOrderRepository.GetByAssignedUserAsync(technician.Id, cancellationToken);
            var currentWorkload = assignedWorkOrders.Count();
            var maxWorkload = 6;
            var workloadRatio = (decimal)currentWorkload / maxWorkload;
            score += (1 - workloadRatio) * 20;

            return Math.Min(Math.Max(score, 0), 100);
        }

        private static List<string> GetMatchingSkills(Domain.Entities.WorkOrder workOrder, ApplicationUser technician)
        {
            var matchingSkills = new List<string>();
            
            if (string.IsNullOrEmpty(technician.Skills) || string.IsNullOrEmpty(workOrder.Equipment))
                return matchingSkills;

            var technicianSkills = technician.Skills.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim().ToLowerInvariant()).ToList();

            var workOrderSkills = workOrder.Equipment.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim().ToLowerInvariant()).ToList();

            foreach (var skill in workOrderSkills)
            {
                if (technicianSkills.Any(ts => ts.Contains(skill) || skill.Contains(ts)))
                {
                    matchingSkills.Add(skill);
                }
            }

            return matchingSkills;
        }
    }
}
