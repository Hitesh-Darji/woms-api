using MediatR;

namespace WOMS.Application.Features.Auth.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommand : IRequest<bool>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
