using MediatR;

namespace WOMS.Application.Features.Forms.Commands.DeleteFormTemplate
{
    public record DeleteFormTemplateCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
        public string? UserIdClaim { get; init; }

        public Guid DeletedBy => !string.IsNullOrEmpty(UserIdClaim) ? Guid.Parse(UserIdClaim) : Guid.Empty;
    }
}

