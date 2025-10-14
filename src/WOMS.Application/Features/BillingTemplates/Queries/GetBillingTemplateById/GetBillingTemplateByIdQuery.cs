using MediatR;
using WOMS.Application.Features.BillingTemplates.DTOs;

namespace WOMS.Application.Features.BillingTemplates.Queries.GetBillingTemplateById
{
    public record GetBillingTemplateByIdQuery : IRequest<BillingTemplateDto?>
    {
        public Guid Id { get; init; }
    }
}
