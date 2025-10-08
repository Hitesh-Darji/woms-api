using MediatR;
using WOMS.Application.Features.Auth.Dtos;

namespace WOMS.Application.Features.Auth.Commands.RegisterUser
{
    public record RegisterUserCommand(string Email, string Password, string ConfirmPassword) : IRequest<AuthResponseDto>;
}
