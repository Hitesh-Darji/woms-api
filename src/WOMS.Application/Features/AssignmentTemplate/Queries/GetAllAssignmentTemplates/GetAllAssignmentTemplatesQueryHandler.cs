using MediatR;
using System.Text.Json;
using WOMS.Application.Features.AssignmentTemplate.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.AssignmentTemplate.Queries.GetAllAssignmentTemplates
{
    public class GetAllAssignmentTemplatesQueryHandler : IRequestHandler<GetAllAssignmentTemplatesQuery, AssignmentTemplateListResponse>
    {
        private readonly IRepository<WOMS.Domain.Entities.AssignmentTemplate> _assignmentTemplateRepository;

        public GetAllAssignmentTemplatesQueryHandler(IRepository<WOMS.Domain.Entities.AssignmentTemplate> assignmentTemplateRepository)
        {
            _assignmentTemplateRepository = assignmentTemplateRepository;
        }

        public async Task<AssignmentTemplateListResponse> Handle(GetAllAssignmentTemplatesQuery request, CancellationToken cancellationToken)
        {
            var query = _assignmentTemplateRepository.GetQueryable()
                .Where(at => !at.IsDeleted);

            // Apply search filter
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(at => at.Name.Contains(request.SearchTerm) || 
                                        (at.Description != null && at.Description.Contains(request.SearchTerm)));
            }

            // Apply status filter
            if (request.Status.HasValue)
            {
                query = query.Where(at => at.Status == request.Status.Value);
            }

            // Apply sorting
            query = request.SortBy.ToLower() switch
            {
                "name" => request.SortDescending ? query.OrderByDescending(at => at.Name) : query.OrderBy(at => at.Name),
                "status" => request.SortDescending ? query.OrderByDescending(at => at.Status) : query.OrderBy(at => at.Status),
                "usagecount" => request.SortDescending ? query.OrderByDescending(at => at.UsageCount) : query.OrderBy(at => at.UsageCount),
                "lastused" => request.SortDescending ? query.OrderByDescending(at => at.LastUsed) : query.OrderBy(at => at.LastUsed),
                _ => request.SortDescending ? query.OrderByDescending(at => at.CreatedOn) : query.OrderBy(at => at.CreatedOn)
            };

            // Get all results and apply pagination manually
            var allTemplates = await _assignmentTemplateRepository.FindAsync(
                at => !at.IsDeleted && 
                     (string.IsNullOrEmpty(request.SearchTerm) || at.Name.Contains(request.SearchTerm) || (at.Description != null && at.Description.Contains(request.SearchTerm))) &&
                     (!request.Status.HasValue || at.Status == request.Status.Value),
                cancellationToken);

            // Apply sorting to the results
            var sortedTemplates = request.SortBy.ToLower() switch
            {
                "name" => request.SortDescending ? allTemplates.OrderByDescending(at => at.Name) : allTemplates.OrderBy(at => at.Name),
                "status" => request.SortDescending ? allTemplates.OrderByDescending(at => at.Status) : allTemplates.OrderBy(at => at.Status),
                "usagecount" => request.SortDescending ? allTemplates.OrderByDescending(at => at.UsageCount) : allTemplates.OrderBy(at => at.UsageCount),
                "lastused" => request.SortDescending ? allTemplates.OrderByDescending(at => at.LastUsed) : allTemplates.OrderBy(at => at.LastUsed),
                _ => request.SortDescending ? allTemplates.OrderByDescending(at => at.CreatedOn) : allTemplates.OrderBy(at => at.CreatedOn)
            };

            var totalCount = sortedTemplates.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            // Apply pagination
            var assignmentTemplates = sortedTemplates
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var templateDtos = new List<AssignmentTemplateDto>();

            foreach (var template in assignmentTemplates)
            {
                var dto = new AssignmentTemplateDto
                {
                    Id = template.Id,
                    Name = template.Name,
                    Description = template.Description,
                    Status = template.Status,
                    StartTime = template.StartTime,
                    EndTime = template.EndTime,
                        DaysOfWeek = JsonSerializer.Deserialize<List<int>>(template.DaysOfWeek)?.Select(d => (DayOfWeekEnum)d).ToList() ?? new List<DayOfWeekEnum>(),
                    WorkTypes = JsonSerializer.Deserialize<List<string>>(template.WorkTypes) ?? new List<string>(),
                    Zones = JsonSerializer.Deserialize<List<string>>(template.Zones) ?? new List<string>(),
                    PreferredTechnicians = JsonSerializer.Deserialize<List<string>>(template.PreferredTechnicians) ?? new List<string>(),
                    SkillsRequired = JsonSerializer.Deserialize<List<string>>(template.SkillsRequired) ?? new List<string>(),
                    AutoAssignmentRules = JsonSerializer.Deserialize<List<string>>(template.AutoAssignmentRules) ?? new List<string>(),
                    UsageCount = template.UsageCount,
                    LastUsed = template.LastUsed,
                    CreatedOn = template.CreatedOn,
                    UpdatedOn = template.UpdatedOn ?? template.CreatedOn,
                    CreatedBy = template.CreatedBy
                };
                templateDtos.Add(dto);
            }

            return new AssignmentTemplateListResponse
            {
                Templates = templateDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages
            };
        }
    }
}
