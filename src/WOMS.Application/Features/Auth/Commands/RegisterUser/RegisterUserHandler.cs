using MediatR;
using Microsoft.AspNetCore.Identity;
using WOMS.Application.Features.Auth.Dtos;
using WOMS.Application.Features.Auth.Services;
using WOMS.Domain.Entities;

namespace WOMS.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, AuthResponseDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public RegisterUserHandler(UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existing = await _userManager.FindByEmailAsync(request.Email);
            if (existing != null)
            {
                throw new InvalidOperationException("Email already registered");
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true,
                FirstName = string.Empty,
                LastName = string.Empty,
                FullName = string.Empty,
                Address = string.Empty,
                City = string.Empty,
                PostalCode = string.Empty,
                Phone = string.Empty
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException(errors);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var (token, expiresAt) = await _jwtTokenService.GenerateTokenAsync(user, _userManager, roles, user.Email!, user.Id, cancellationToken);
            var refreshToken = await _jwtTokenService.CreateRefreshTokenAsync(user.Id, token, cancellationToken);

            return new AuthResponseDto
            {
                Token = token,
                ExpiresAtUtc = expiresAt,
                RefreshToken = refreshToken.Refresh_Token,
                RefreshTokenExpiresAtUtc = refreshToken.RefreshTokenExpirationTime,
                UserId = user.Id,
                Email = user.Email!,
                Roles = roles
            };
        }
    }
}
