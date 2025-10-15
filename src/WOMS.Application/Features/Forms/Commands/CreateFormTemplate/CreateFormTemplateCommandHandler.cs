using MediatR;
using WOMS.Application.Features.Forms.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Forms.Commands.CreateFormTemplate
{
    public class CreateFormTemplateCommandHandler : IRequestHandler<CreateFormTemplateCommand, FormTemplateDto>
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly IFormSectionRepository _formSectionRepository;
        private readonly IFormFieldRepository _formFieldRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFormTemplateCommandHandler(
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

        public async Task<FormTemplateDto> Handle(CreateFormTemplateCommand request, CancellationToken cancellationToken)
        {
            // Validate that the request body is not null
            if (request.CreateFormTemplateDto == null)
            {
                throw new ArgumentNullException(nameof(request), "Request body cannot be null. Please provide valid form template data.");
            }

            // Validate that the user ID is not null
            if (string.IsNullOrEmpty(request.UserIdClaim))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }

            // Check if form template with same name already exists
            var existingTemplate = await _formTemplateRepository.ExistsByNameAsync(request.Name, cancellationToken);
            if (existingTemplate)
            {
                throw new InvalidOperationException($"A form template with the name '{request.Name}' already exists.");
            }

            // Create the form template
            var formTemplate = new FormTemplate
            {
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                Status = request.Status,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            await _formTemplateRepository.AddAsync(formTemplate, cancellationToken);

            // Create sections and fields
            foreach (var sectionDto in request.Sections)
            {
                var section = new FormSection
                {
                    FormTemplateId = formTemplate.Id,
                    Title = sectionDto.Title,
                    Description = sectionDto.Description,
                    OrderIndex = sectionDto.OrderIndex,
                    IsRequired = sectionDto.IsRequired,
                    IsCollapsible = sectionDto.IsCollapsible,
                    IsCollapsed = sectionDto.IsCollapsed,
                    CreatedBy = request.CreatedBy,
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
                        CreatedBy = request.CreatedBy,
                        CreatedOn = DateTime.UtcNow
                    };

                    await _formFieldRepository.AddAsync(field, cancellationToken);
                }
            }

            // Save all changes to the database
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return the created form template with all related data
            var createdTemplate = await _formTemplateRepository.GetByIdWithSectionsAndFieldsAsync(formTemplate.Id, cancellationToken);
            
            if (createdTemplate == null)
            {
                throw new InvalidOperationException("Failed to retrieve the created form template.");
            }
            
            return _mapper.Map<FormTemplateDto>(createdTemplate);
        }
    }
}

