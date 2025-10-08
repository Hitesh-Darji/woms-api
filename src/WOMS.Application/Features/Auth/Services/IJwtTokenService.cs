using Microsoft.AspNetCore.Identity;

namespace WOMS.Application.Features.Auth.Services
{
    public interface IJwtTokenService
    {
        Task<(string token, DateTime expiresAtUtc)> GenerateTokenAsync<TUser>(
            TUser user,
            UserManager<TUser> userManager,
            IList<string> roles,
            string email,
            Guid userId,
            CancellationToken cancellationToken = default) where TUser : class;
    }
}
