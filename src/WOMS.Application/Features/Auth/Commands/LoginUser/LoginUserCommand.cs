using MediatR;
using WOMS.Application.Features.Auth.Dtos;

namespace WOMS.Application.Features.Auth.Commands.LoginUser
{
    public record LoginUserCommand(string Email, string Password) : IRequest<AuthResponseDto>;
}
