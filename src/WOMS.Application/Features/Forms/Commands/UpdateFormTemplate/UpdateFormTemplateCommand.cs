using MediatR;
using WOMS.Application.Features.Forms.DTOs;

namespace WOMS.Application.Features.Forms.Commands.UpdateFormTemplate
{
    public record UpdateFormTemplateCommand : IRequest<FormTemplateDto>
    {
        public Guid Id { get; init; }
        public UpdateFormTemplateDto? UpdateFormTemplateDto { get; init; }
        public string? UserIdClaim { get; init; }

        public string Name => UpdateFormTemplateDto?.Name ?? string.Empty;
        public string? Description => UpdateFormTemplateDto?.Description;
        public string Category => UpdateFormTemplateDto?.Category ?? string.Empty;
        public string Status => UpdateFormTemplateDto?.Status ?? "draft";
        public bool IsActive => UpdateFormTemplateDto?.IsActive ?? true;
        public List<UpdateFormSectionDto> Sections => UpdateFormTemplateDto?.Sections ?? new List<UpdateFormSectionDto>();
        public Guid UpdatedBy => !string.IsNullOrEmpty(UserIdClaim) ? Guid.Parse(UserIdClaim) : Guid.Empty;
    }
}

