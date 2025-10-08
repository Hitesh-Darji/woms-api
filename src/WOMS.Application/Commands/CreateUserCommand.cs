using MediatR;
using WOMS.Application.DTOs;

namespace WOMS.Application.Commands
{
    public record CreateUserCommand : IRequest<UserDto>
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
    }
}
