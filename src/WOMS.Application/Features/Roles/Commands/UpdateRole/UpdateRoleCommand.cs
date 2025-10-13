using MediatR;
using WOMS.Application.Features.Roles.DTOs;

namespace WOMS.Application.Features.Roles.Commands.UpdateRole
{
    public record UpdateRoleCommand : IRequest<RoleDto>
    {
        public string Id { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public string UpdatedBy { get; init; } = string.Empty;
    }
}
