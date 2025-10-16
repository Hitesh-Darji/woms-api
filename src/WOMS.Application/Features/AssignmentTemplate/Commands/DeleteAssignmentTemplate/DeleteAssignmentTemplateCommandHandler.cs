using MediatR;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;
using WOMS.Domain.Entities;

namespace WOMS.Application.Features.AssignmentTemplate.Commands.DeleteAssignmentTemplate
{
    public class DeleteAssignmentTemplateCommandHandler : IRequestHandler<DeleteAssignmentTemplateCommand, bool>
    {
        private readonly IRepository<WOMS.Domain.Entities.AssignmentTemplate> _assignmentTemplateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAssignmentTemplateCommandHandler(
            IRepository<WOMS.Domain.Entities.AssignmentTemplate> assignmentTemplateRepository,
            IUnitOfWork unitOfWork)
        {
            _assignmentTemplateRepository = assignmentTemplateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAssignmentTemplateCommand request, CancellationToken cancellationToken)
        {
            var assignmentTemplate = await _assignmentTemplateRepository.GetByIdAsync(request.Id, cancellationToken);

            if (assignmentTemplate == null || assignmentTemplate.IsDeleted)
            {
                return false;
            }

            assignmentTemplate.IsDeleted = true;
            assignmentTemplate.UpdatedOn = DateTime.UtcNow;

            await _assignmentTemplateRepository.UpdateAsync(assignmentTemplate, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
