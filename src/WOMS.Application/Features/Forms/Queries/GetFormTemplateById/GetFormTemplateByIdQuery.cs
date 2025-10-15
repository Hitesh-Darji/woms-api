using MediatR;
using WOMS.Application.Features.Forms.DTOs;

namespace WOMS.Application.Features.Forms.Queries.GetFormTemplateById
{
    public record GetFormTemplateByIdQuery : IRequest<FormTemplateDto?>
    {
        public Guid Id { get; init; }
    }
}

