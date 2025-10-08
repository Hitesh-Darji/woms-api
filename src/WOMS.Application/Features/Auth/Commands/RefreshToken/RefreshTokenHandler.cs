using MediatR;
using Microsoft.AspNetCore.Identity;
using WOMS.Application.Features.Auth.Dtos;
using WOMS.Application.Features.Auth.Services;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDto>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public RefreshTokenHandler(
            IRefreshTokenRepository refreshTokenRepository,
            UserManager<ApplicationUser> userManager,
            IJwtTokenService jwtTokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<RefreshTokenResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);

            if (refreshToken == null)
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            if (refreshToken.RefreshTokenExpirationTime < DateTime.UtcNow)
            {
                // Remove expired refresh token
                await _refreshTokenRepository.DeleteAsync(refreshToken, cancellationToken);
                throw new UnauthorizedAccessException("Refresh token has expired");
            }

            var user = refreshToken.User;
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            // Generate new JWT token
            var roles = await _userManager.GetRolesAsync(user);
            var (newToken, expiresAt) = await _jwtTokenService.GenerateTokenAsync(user, _userManager, roles, user.Email!, user.Id, cancellationToken);

            // Generate new refresh token
            var newRefreshToken = Guid.NewGuid().ToString();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(7); // 7 days expiration

            // Update the existing refresh token
            refreshToken.Refresh_Token = newRefreshToken;
            refreshToken.JwtToken = newToken;
            refreshToken.RefreshTokenExpirationTime = refreshTokenExpiration;
            refreshToken.UpdatedOn = DateTime.UtcNow;

            await _refreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);

            return new RefreshTokenResponseDto
            {
                Token = newToken,
                ExpiresAtUtc = expiresAt,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiresAtUtc = refreshTokenExpiration,
                UserId = user.Id,
                Email = user.Email!,
                Roles = roles
            };
        }
    }
}
