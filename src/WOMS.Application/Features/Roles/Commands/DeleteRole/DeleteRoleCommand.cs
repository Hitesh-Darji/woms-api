using MediatR;

namespace WOMS.Application.Features.Roles.Commands.DeleteRole
{
    public record DeleteRoleCommand : IRequest<bool>
    {
        public string Id { get; init; } = string.Empty;
    }
}
