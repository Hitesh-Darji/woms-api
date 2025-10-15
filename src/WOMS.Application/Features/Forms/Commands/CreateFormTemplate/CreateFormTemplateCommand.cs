using MediatR;
using WOMS.Application.Features.Forms.DTOs;

namespace WOMS.Application.Features.Forms.Commands.CreateFormTemplate
{
    public record CreateFormTemplateCommand : IRequest<FormTemplateDto>
    {
        public CreateFormTemplateDto? CreateFormTemplateDto { get; init; }
        public string? UserIdClaim { get; init; }

        public string Name => CreateFormTemplateDto?.Name ?? string.Empty;
        public string? Description => CreateFormTemplateDto?.Description;
        public string Category => CreateFormTemplateDto?.Category ?? string.Empty;
        public string Status => CreateFormTemplateDto?.Status ?? "draft";
        public bool IsActive => CreateFormTemplateDto?.IsActive ?? true;
        public List<CreateFormSectionDto> Sections => CreateFormTemplateDto?.Sections ?? new List<CreateFormSectionDto>();
        public Guid CreatedBy => !string.IsNullOrEmpty(UserIdClaim) ? Guid.Parse(UserIdClaim) : Guid.Empty;
    }
}

