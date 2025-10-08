using MediatR;
using WOMS.Application.Features.Auth.Dtos;

namespace WOMS.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponseDto>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
