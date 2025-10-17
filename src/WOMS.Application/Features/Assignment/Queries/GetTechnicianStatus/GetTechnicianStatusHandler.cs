using AutoMapper;
using MediatR;
using WOMS.Application.Features.Assignment.DTOs;
using WOMS.Application.Features.Assignment.Queries.GetTechnicianStatus;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Assignment.Queries.GetTechnicianStatus
{
    public class GetTechnicianStatusHandler : IRequestHandler<GetTechnicianStatusQuery, TechnicianStatusResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITechnicianEquipmentRepository _technicianEquipmentRepository;
        private readonly IMapper _mapper;

        public GetTechnicianStatusHandler(IUserRepository userRepository, IWorkOrderRepository workOrderRepository, ITechnicianEquipmentRepository technicianEquipmentRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _workOrderRepository = workOrderRepository;
            _technicianEquipmentRepository = technicianEquipmentRepository;
            _mapper = mapper;
        }

        public async Task<TechnicianStatusResponse> Handle(GetTechnicianStatusQuery request, CancellationToken cancellationToken)
        {
            // Get technicians (users with technician role)
            var technicians = await _userRepository.GetTechniciansAsync(request.Status, request.Location, cancellationToken);

            var technicianDtos = new List<TechnicianStatusDto>();

            foreach (var technician in technicians)
            {
                // Get assigned work orders for this technician
                var assignedWorkOrders = await _workOrderRepository.GetByAssignedUserAsync(technician.Id, cancellationToken);

                var technicianDto = new TechnicianStatusDto
                {
                    TechnicianId = technician.Id,
                    TechnicianName = $"{technician.FirstName} {technician.LastName}",
                    Status = await GetTechnicianStatus(technician, cancellationToken),
                    Location = technician.City ?? "Unknown",
                    CurrentWorkload = assignedWorkOrders.Count(),
                    MaxWorkload = GetMaxWorkload(technician),
                    ShiftEndTime = GetShiftEndTime(technician),
                    Skills = ParseSkills(technician.Skills),
                    Equipment = await GetTechnicianEquipment(technician, cancellationToken),
                    AssignedWorkOrders = assignedWorkOrders.Select(wo => new AssignedWorkOrderDto
                    {
                        WorkOrderId = wo.Id,
                        WorkOrderNumber = wo.WorkOrderNumber,
                        Customer = wo.Customer,
                        WorkType = wo.Type.ToString(),
                        Priority = wo.Priority.ToString(),
                        ScheduledStartTime = null, // Would need scheduling logic
                        ScheduledEndTime = null,
                        Status = wo.Status.ToString()
                    }).ToList()
                };

                technicianDtos.Add(technicianDto);
            }

            return new TechnicianStatusResponse
            {
                Technicians = technicianDtos,
                TotalCount = technicianDtos.Count,
                AvailableCount = technicianDtos.Count(t => t.Status == "available"),
                BusyCount = technicianDtos.Count(t => t.Status == "busy"),
                OnBreakCount = technicianDtos.Count(t => t.Status == "break"),
                OfflineCount = technicianDtos.Count(t => t.Status == "offline")
            };
        }

        private async Task<string> GetTechnicianStatus(Domain.Entities.ApplicationUser technician, CancellationToken cancellationToken)
        {
            // Check if technician is active and not deleted
            if (!technician.IsActive || technician.IsDeleted)
                return "offline";

            // Get current workload
            var assignedWorkOrders = await _workOrderRepository.GetByAssignedUserAsync(technician.Id, cancellationToken);
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

        private static int GetMaxWorkload(Domain.Entities.ApplicationUser technician)
        {
            // In a real implementation, this would be configurable per technician
            return 6;
        }

        private static DateTime? GetShiftEndTime(Domain.Entities.ApplicationUser technician)
        {
            // In a real implementation, this would come from shift management
            return DateTime.Today.AddHours(17); // 5 PM default
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
