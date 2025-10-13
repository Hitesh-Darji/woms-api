using MediatR;
using WOMS.Application.Features.Users.DTOs;

namespace WOMS.Application.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand : IRequest<UserDto>
    {
        public UpdateUserDto UpdateUserDto { get; init; } = new();
    }
}
