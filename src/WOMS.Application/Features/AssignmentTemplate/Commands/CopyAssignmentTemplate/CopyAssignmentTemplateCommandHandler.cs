using MediatR;
using System.Security.Claims;
using WOMS.Application.Features.AssignmentTemplate.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace WOMS.Application.Features.AssignmentTemplate.Commands.CopyAssignmentTemplate
{
    public class CopyAssignmentTemplateCommandHandler : IRequestHandler<CopyAssignmentTemplateCommand, AssignmentTemplateDto>
    {
        private readonly IRepository<WOMS.Domain.Entities.AssignmentTemplate> _assignmentTemplateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AutoMapper.IMapper _mapper;

        public CopyAssignmentTemplateCommandHandler(
            IRepository<WOMS.Domain.Entities.AssignmentTemplate> assignmentTemplateRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            AutoMapper.IMapper mapper)
        {
            _assignmentTemplateRepository = assignmentTemplateRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<AssignmentTemplateDto> Handle(CopyAssignmentTemplateCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Get the original template
            var originalTemplate = await _assignmentTemplateRepository.GetByIdAsync(request.Id, cancellationToken);

            if (originalTemplate == null || originalTemplate.IsDeleted)
            {
                throw new ArgumentException($"Assignment template with ID {request.Id} not found.");
            }

            // Generate new name if not provided
            var newName = !string.IsNullOrEmpty(request.NewName) 
                ? request.NewName 
                : $"{originalTemplate.Name} (Copy)";

            // Create the copy
            var copiedTemplate = new WOMS.Domain.Entities.AssignmentTemplate
            {
                Name = newName,
                Description = originalTemplate.Description,
                Status = originalTemplate.Status,
                StartTime = originalTemplate.StartTime,
                EndTime = originalTemplate.EndTime,
                DaysOfWeek = originalTemplate.DaysOfWeek, // Copy JSON as-is
                WorkTypes = originalTemplate.WorkTypes, // Copy JSON as-is
                Zones = originalTemplate.Zones, // Copy JSON as-is
                PreferredTechnicians = originalTemplate.PreferredTechnicians, // Copy JSON as-is
                SkillsRequired = originalTemplate.SkillsRequired, // Copy JSON as-is
                AutoAssignmentRules = originalTemplate.AutoAssignmentRules, // Copy JSON as-is
                UsageCount = 0, // Reset usage count for copy
                LastUsed = null, // Reset last used for copy
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userId,
                IsDeleted = false
            };

            await _assignmentTemplateRepository.AddAsync(copiedTemplate, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return the copied template as DTO using AutoMapper
            return _mapper.Map<AssignmentTemplateDto>(copiedTemplate);
        }
    }
}
