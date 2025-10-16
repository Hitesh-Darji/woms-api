using MediatR;
using System.Text.Json;
using WOMS.Application.Features.AssignmentTemplate.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.AssignmentTemplate.Commands.UpdateAssignmentTemplate
{
    public class UpdateAssignmentTemplateCommandHandler : IRequestHandler<UpdateAssignmentTemplateCommand, AssignmentTemplateDto>
    {
        private readonly IRepository<WOMS.Domain.Entities.AssignmentTemplate> _assignmentTemplateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;

        public UpdateAssignmentTemplateCommandHandler(
            IRepository<WOMS.Domain.Entities.AssignmentTemplate> assignmentTemplateRepository,
            IUnitOfWork unitOfWork,
            AutoMapper.IMapper mapper)
        {
            _assignmentTemplateRepository = assignmentTemplateRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AssignmentTemplateDto> Handle(UpdateAssignmentTemplateCommand request, CancellationToken cancellationToken)
        {
            var assignmentTemplate = await _assignmentTemplateRepository.GetByIdAsync(request.Id, cancellationToken);

            if (assignmentTemplate == null || assignmentTemplate.IsDeleted)
            {
                throw new ArgumentException($"Assignment template with ID {request.Id} not found.");
            }

            assignmentTemplate.Name = request.Name;
            assignmentTemplate.Description = request.Description;
            assignmentTemplate.Status = request.Status;
            assignmentTemplate.StartTime = request.StartTime;
            assignmentTemplate.EndTime = request.EndTime;
            assignmentTemplate.DaysOfWeek = JsonSerializer.Serialize(request.DaysOfWeek.Select(d => (int)d).ToList());
            assignmentTemplate.WorkTypes = JsonSerializer.Serialize(request.WorkTypes);
            assignmentTemplate.Zones = JsonSerializer.Serialize(request.Zones);
            assignmentTemplate.PreferredTechnicians = JsonSerializer.Serialize(request.PreferredTechnicians);
            assignmentTemplate.SkillsRequired = JsonSerializer.Serialize(request.SkillsRequired);
            assignmentTemplate.AutoAssignmentRules = JsonSerializer.Serialize(request.AutoAssignmentRules);
            assignmentTemplate.UpdatedOn = DateTime.UtcNow;

            await _assignmentTemplateRepository.UpdateAsync(assignmentTemplate, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AssignmentTemplateDto>(assignmentTemplate);
        }
    }
}
