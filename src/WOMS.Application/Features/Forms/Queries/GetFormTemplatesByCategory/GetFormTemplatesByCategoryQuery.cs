using MediatR;
using WOMS.Application.Features.Forms.DTOs;

namespace WOMS.Application.Features.Forms.Queries.GetFormTemplatesByCategory
{
    public record GetFormTemplatesByCategoryQuery : IRequest<IEnumerable<FormTemplateDto>>
    {
        public string Category { get; init; } = string.Empty;
    }
}
