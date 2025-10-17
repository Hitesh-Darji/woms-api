using MediatR;
using WOMS.Application.Features.AssignmentTemplate.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;
using WOMS.Domain.Entities;

namespace WOMS.Application.Features.AssignmentTemplate.Commands.ToggleAssignmentTemplateStatus
{
    public class ToggleAssignmentTemplateStatusCommandHandler : IRequestHandler<ToggleAssignmentTemplateStatusCommand, AssignmentTemplateDto>
    {
        private readonly IRepository<WOMS.Domain.Entities.AssignmentTemplate> _assignmentTemplateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;

        public ToggleAssignmentTemplateStatusCommandHandler(
            IRepository<WOMS.Domain.Entities.AssignmentTemplate> assignmentTemplateRepository,
            IUnitOfWork unitOfWork,
            AutoMapper.IMapper mapper)
        {
            _assignmentTemplateRepository = assignmentTemplateRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AssignmentTemplateDto> Handle(ToggleAssignmentTemplateStatusCommand request, CancellationToken cancellationToken)
        {
            var assignmentTemplate = await _assignmentTemplateRepository.GetByIdAsync(request.Id, cancellationToken);

            if (assignmentTemplate == null || assignmentTemplate.IsDeleted)
            {
                throw new ArgumentException($"Assignment template with ID {request.Id} not found.");
            }

            assignmentTemplate.Status = request.Status;
            assignmentTemplate.UpdatedOn = DateTime.UtcNow;

            await _assignmentTemplateRepository.UpdateAsync(assignmentTemplate, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AssignmentTemplateDto>(assignmentTemplate);
        }
    }
}
