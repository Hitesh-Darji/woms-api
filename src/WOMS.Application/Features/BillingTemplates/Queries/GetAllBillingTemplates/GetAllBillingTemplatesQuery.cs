using MediatR;
using WOMS.Application.Features.BillingTemplates.DTOs;

namespace WOMS.Application.Features.BillingTemplates.Queries.GetAllBillingTemplates
{
    public record GetAllBillingTemplatesQuery : IRequest<IEnumerable<BillingTemplateDto>>
    {
        public string? CustomerId { get; init; }
    }
}
