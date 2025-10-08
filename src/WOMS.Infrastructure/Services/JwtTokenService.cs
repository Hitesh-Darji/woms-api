using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WOMS.Application.Features.Auth.Services;

namespace WOMS.Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<(string token, DateTime expiresAtUtc)> GenerateTokenAsync<TUser>(
            TUser user,
            UserManager<TUser> userManager,
            IList<string> roles,
            string email,
            Guid userId,
            CancellationToken cancellationToken = default) where TUser : class
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var key = jwtSection.GetValue<string>("Key") ?? string.Empty;
            var issuer = jwtSection.GetValue<string>("Issuer") ?? string.Empty;
            var audience = jwtSection.GetValue<string>("Audience") ?? string.Empty;
            var duration = jwtSection.GetValue<int>("DurationInMinutes");

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Email, email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(duration);

            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: credentials
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwt);
            //var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return Task.FromResult((token, expires));
        }
    }
}
