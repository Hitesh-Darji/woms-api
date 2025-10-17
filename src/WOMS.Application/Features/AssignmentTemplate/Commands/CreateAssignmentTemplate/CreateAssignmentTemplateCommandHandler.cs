using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;
using WOMS.Application.Features.AssignmentTemplate.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.AssignmentTemplate.Commands.CreateAssignmentTemplate
{
    public class CreateAssignmentTemplateCommandHandler : IRequestHandler<CreateAssignmentTemplateCommand, AssignmentTemplateDto>
    {
        private readonly IRepository<Domain.Entities.AssignmentTemplate> _assignmentTemplateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateAssignmentTemplateCommandHandler(
            IRepository<Domain.Entities.AssignmentTemplate> assignmentTemplateRepository,
            IUnitOfWork unitOfWork,
            AutoMapper.IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _assignmentTemplateRepository = assignmentTemplateRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AssignmentTemplateDto> Handle(CreateAssignmentTemplateCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var assignmentTemplate = new Domain.Entities.AssignmentTemplate
            {
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                DaysOfWeek = JsonSerializer.Serialize(request.DaysOfWeek.Select(d => (int)d).ToList()),
                WorkTypes = JsonSerializer.Serialize(new List<string>()),
                Zones = JsonSerializer.Serialize(new List<string>()),
                PreferredTechnicians = JsonSerializer.Serialize(new List<string>()),
                SkillsRequired = JsonSerializer.Serialize(new List<string>()),
                AutoAssignmentRules = JsonSerializer.Serialize(request.AutoAssignmentRules),
                UsageCount = 0,
                LastUsed = null,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userId,
                IsDeleted = false
            };

            await _assignmentTemplateRepository.AddAsync(assignmentTemplate, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AssignmentTemplateDto>(assignmentTemplate);
        }
    }
}
