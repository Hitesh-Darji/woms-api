using MediatR;

namespace WOMS.Application.Features.Users.Commands.DeleteUser
{
    public record DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
    }
}
