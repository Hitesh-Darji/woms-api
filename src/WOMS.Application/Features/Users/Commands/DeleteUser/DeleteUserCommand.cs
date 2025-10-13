using MediatR;

namespace WOMS.Application.Features.Users.Commands.DeleteUser
{
    public record DeleteUserCommand : IRequest<bool>
    {
        public string Id { get; init; } = string.Empty;
    }
}
