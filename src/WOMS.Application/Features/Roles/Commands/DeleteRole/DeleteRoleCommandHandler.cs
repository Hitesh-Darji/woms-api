using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using WOMS.Domain.Entities;

namespace WOMS.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteRoleCommandHandler(
            RoleManager<ApplicationRole> roleManager, 
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var deletedBy))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }

            // Check if role exists
            var role = await _roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {
                return false;
            }

            // Check if role is being used by any users
            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
            if (usersInRole.Any())
            {
                throw new InvalidOperationException($"Cannot delete role '{role.Name}' because it is assigned to {usersInRole.Count} user(s).");
            }

            // Delete the role (DeletedBy: {deletedBy} - extracted from JWT token)
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to delete role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return true;
        }
    }
}
