using MediatR;

namespace WOMS.Application.Features.BillingTemplates.Commands.DeleteBillingTemplate
{
    public record DeleteBillingTemplateCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
    }
}
