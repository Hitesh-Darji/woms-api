using MediatR;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Auth.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenHandler : IRequestHandler<RevokeRefreshTokenCommand, bool>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RevokeRefreshTokenHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<bool> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);

            if (refreshToken == null)
            {
                return false;
            }

            await _refreshTokenRepository.DeleteAsync(refreshToken, cancellationToken);

            return true;
        }
    }
}
