using MediatR;
using WOMS.Application.Features.Forms.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Forms.Commands.UpdateFormTemplate
{
    public class UpdateFormTemplateCommandHandler : IRequestHandler<UpdateFormTemplateCommand, FormTemplateDto>
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly IFormSectionRepository _formSectionRepository;
        private readonly IFormFieldRepository _formFieldRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFormTemplateCommandHandler(
            IFormTemplateRepository formTemplateRepository,
            IFormSectionRepository formSectionRepository,
            IFormFieldRepository formFieldRepository,
            AutoMapper.IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _formTemplateRepository = formTemplateRepository;
            _formSectionRepository = formSectionRepository;
            _formFieldRepository = formFieldRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<FormTemplateDto> Handle(UpdateFormTemplateCommand request, CancellationToken cancellationToken)
        {
            // Validate that the request body is not null
            if (request.UpdateFormTemplateDto == null)
            {
                throw new ArgumentNullException(nameof(request), "Request body cannot be null. Please provide valid form template data.");
            }

            // Validate that the user ID is not null
            if (string.IsNullOrEmpty(request.UserIdClaim))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }

            // Get the existing form template
            var existingTemplate = await _formTemplateRepository.GetByIdWithSectionsAndFieldsAsync(request.Id, cancellationToken);
            if (existingTemplate == null)
            {
                throw new KeyNotFoundException($"Form template with ID '{request.Id}' not found.");
            }

            // Check if another form template with the same name exists (excluding current one)
            var nameExists = await _formTemplateRepository.ExistsByNameExcludingIdAsync(request.Name, request.Id, cancellationToken);
            if (nameExists)
            {
                throw new InvalidOperationException($"A form template with the name '{request.Name}' already exists.");
            }

            // Update the form template
            existingTemplate.Name = request.Name;
            existingTemplate.Description = request.Description;
            existingTemplate.Category = request.Category;
            existingTemplate.Status = request.Status;
            existingTemplate.IsActive = request.IsActive;
            existingTemplate.UpdatedBy = request.UpdatedBy;
            existingTemplate.UpdatedOn = DateTime.UtcNow;

            await _formTemplateRepository.UpdateAsync(existingTemplate, cancellationToken);

            // Delete existing sections and fields
            await _formSectionRepository.DeleteByFormTemplateIdAsync(request.Id, cancellationToken);

            // Create new sections and fields
            foreach (var sectionDto in request.Sections)
            {
                var section = new FormSection
                {
                    FormTemplateId = existingTemplate.Id,
                    Title = sectionDto.Title,
                    Description = sectionDto.Description,
                    OrderIndex = sectionDto.OrderIndex,
                    IsRequired = sectionDto.IsRequired,
                    IsCollapsible = sectionDto.IsCollapsible,
                    IsCollapsed = sectionDto.IsCollapsed,
                    CreatedBy = request.UpdatedBy,
                    CreatedOn = DateTime.UtcNow
                };

                await _formSectionRepository.AddAsync(section, cancellationToken);

                // Create fields for this section
                foreach (var fieldDto in sectionDto.Fields)
                {
                    var field = new FormField
                    {
                        FormSectionId = section.Id,
                        FieldType = fieldDto.FieldType,
                        Label = fieldDto.Label,
                        Placeholder = fieldDto.Placeholder,
                        HelpText = fieldDto.HelpText,
                        IsRequired = fieldDto.IsRequired,
                        IsReadOnly = fieldDto.IsReadOnly,
                        IsVisible = fieldDto.IsVisible,
                        OrderIndex = fieldDto.OrderIndex,
                        ValidationRules = fieldDto.ValidationRules,
                        Options = fieldDto.Options,
                        DefaultValue = fieldDto.DefaultValue,
                        MinValue = fieldDto.MinValue,
                        MaxValue = fieldDto.MaxValue,
                        MinLength = fieldDto.MinLength,
                        MaxLength = fieldDto.MaxLength,
                        Pattern = fieldDto.Pattern,
                        Step = fieldDto.Step,
                        Rows = fieldDto.Rows,
                        Columns = fieldDto.Columns,
                        CreatedBy = request.UpdatedBy,
                        CreatedOn = DateTime.UtcNow
                    };

                    await _formFieldRepository.AddAsync(field, cancellationToken);
                }
            }

            // Save all changes to the database
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return the updated form template with all related data
            var updatedTemplate = await _formTemplateRepository.GetByIdWithSectionsAndFieldsAsync(existingTemplate.Id, cancellationToken);
            
            if (updatedTemplate == null)
            {
                throw new InvalidOperationException("Failed to retrieve the updated form template.");
            }
            
            return _mapper.Map<FormTemplateDto>(updatedTemplate);
        }
    }
}

