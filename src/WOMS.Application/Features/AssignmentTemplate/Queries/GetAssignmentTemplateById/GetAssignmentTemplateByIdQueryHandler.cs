using MediatR;
using System.Text.Json;
using WOMS.Application.Features.AssignmentTemplate.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.AssignmentTemplate.Queries.GetAssignmentTemplateById
{
    public class GetAssignmentTemplateByIdQueryHandler : IRequestHandler<GetAssignmentTemplateByIdQuery, AssignmentTemplateDto?>
    {
        private readonly IRepository<WOMS.Domain.Entities.AssignmentTemplate> _assignmentTemplateRepository;

        public GetAssignmentTemplateByIdQueryHandler(IRepository<WOMS.Domain.Entities.AssignmentTemplate> assignmentTemplateRepository)
        {
            _assignmentTemplateRepository = assignmentTemplateRepository;
        }

        public async Task<AssignmentTemplateDto?> Handle(GetAssignmentTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            var assignmentTemplate = await _assignmentTemplateRepository.GetByIdAsync(request.Id, cancellationToken);

            if (assignmentTemplate == null || assignmentTemplate.IsDeleted)
            {
                return null;
            }

            return new AssignmentTemplateDto
            {
                Id = assignmentTemplate.Id,
                Name = assignmentTemplate.Name,
                Description = assignmentTemplate.Description,
                Status = assignmentTemplate.Status,
                StartTime = assignmentTemplate.StartTime,
                EndTime = assignmentTemplate.EndTime,
                    DaysOfWeek = JsonSerializer.Deserialize<List<int>>(assignmentTemplate.DaysOfWeek)?.Select(d => (DayOfWeekEnum)d).ToList() ?? new List<DayOfWeekEnum>(),
                WorkTypes = JsonSerializer.Deserialize<List<string>>(assignmentTemplate.WorkTypes) ?? new List<string>(),
                Zones = JsonSerializer.Deserialize<List<string>>(assignmentTemplate.Zones) ?? new List<string>(),
                PreferredTechnicians = JsonSerializer.Deserialize<List<string>>(assignmentTemplate.PreferredTechnicians) ?? new List<string>(),
                SkillsRequired = JsonSerializer.Deserialize<List<string>>(assignmentTemplate.SkillsRequired) ?? new List<string>(),
                AutoAssignmentRules = JsonSerializer.Deserialize<List<string>>(assignmentTemplate.AutoAssignmentRules) ?? new List<string>(),
                UsageCount = assignmentTemplate.UsageCount,
                LastUsed = assignmentTemplate.LastUsed,
                CreatedOn = assignmentTemplate.CreatedOn,
                UpdatedOn = assignmentTemplate.UpdatedOn ?? assignmentTemplate.CreatedOn,
                CreatedBy = assignmentTemplate.CreatedBy
            };
        }
    }
}
