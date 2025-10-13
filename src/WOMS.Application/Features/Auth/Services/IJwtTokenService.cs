using Microsoft.AspNetCore.Identity;
using WOMS.Domain.Entities;

namespace WOMS.Application.Features.Auth.Services
{
    public interface IJwtTokenService
    {
        Task<(string token, DateTime expiresAtUtc)> GenerateTokenAsync<TUser>(
            TUser user,
            UserManager<TUser> userManager,
            IList<string> roles,
            string email,
            string userId,
            CancellationToken cancellationToken = default) where TUser : class;

        Task<string> GenerateRefreshTokenAsync();
        Task<RefreshToken> CreateRefreshTokenAsync(string userId, string jwtToken, CancellationToken cancellationToken = default);
    }
}
