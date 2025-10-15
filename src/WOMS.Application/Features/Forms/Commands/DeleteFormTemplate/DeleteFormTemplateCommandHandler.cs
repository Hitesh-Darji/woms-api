using MediatR;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Forms.Commands.DeleteFormTemplate
{
    public class DeleteFormTemplateCommandHandler : IRequestHandler<DeleteFormTemplateCommand, bool>
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly IFormSectionRepository _formSectionRepository;
        private readonly IFormFieldRepository _formFieldRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFormTemplateCommandHandler(
            IFormTemplateRepository formTemplateRepository,
            IFormSectionRepository formSectionRepository,
            IFormFieldRepository formFieldRepository,
            IUnitOfWork unitOfWork)
        {
            _formTemplateRepository = formTemplateRepository;
            _formSectionRepository = formSectionRepository;
            _formFieldRepository = formFieldRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteFormTemplateCommand request, CancellationToken cancellationToken)
        {
            // Validate that the user ID is not null
            if (string.IsNullOrEmpty(request.UserIdClaim))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }

            var formTemplate = await _formTemplateRepository.GetByIdAsync(request.Id, cancellationToken);
            if (formTemplate == null || formTemplate.IsDeleted)
            {
                return false;
            }

            // Soft delete the form template
            formTemplate.IsDeleted = true;
            formTemplate.DeletedBy = request.DeletedBy;
            formTemplate.DeletedOn = DateTime.UtcNow;

            // Soft delete all related sections
            var sections = await _formSectionRepository.GetByFormTemplateIdAsync(request.Id, cancellationToken);
            foreach (var section in sections)
            {
                section.IsDeleted = true;
                section.DeletedBy = request.DeletedBy;
                section.DeletedOn = DateTime.UtcNow;
                await _formSectionRepository.UpdateAsync(section, cancellationToken);

                // Soft delete all fields in this section
                var fields = await _formFieldRepository.GetByFormSectionIdAsync(section.Id, cancellationToken);
                foreach (var field in fields)
                {
                    field.IsDeleted = true;
                    field.DeletedBy = request.DeletedBy;
                    field.DeletedOn = DateTime.UtcNow;
                    await _formFieldRepository.UpdateAsync(field, cancellationToken);
                }
            }

            // Update the form template
            await _formTemplateRepository.UpdateAsync(formTemplate, cancellationToken);

            // Save all changes to the database
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

