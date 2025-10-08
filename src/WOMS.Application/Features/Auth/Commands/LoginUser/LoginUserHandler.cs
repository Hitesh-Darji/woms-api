using MediatR;
using Microsoft.AspNetCore.Identity;
using WOMS.Application.Features.Auth.Dtos;
using WOMS.Application.Features.Auth.Services;
using WOMS.Domain.Entities;
namespace WOMS.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, AuthResponseDto>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginUserHandler(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IJwtTokenService jwtTokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)

                throw new UnauthorizedAccessException("Invalid credentials");
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