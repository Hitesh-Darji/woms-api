using MediatR;
using WOMS.Application.Features.Roles.DTOs;

namespace WOMS.Application.Features.Roles.Commands.CreateRole
{
    public record CreateRoleCommand : IRequest<RoleDto>
    {
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public Guid CreatedBy { get; init; }
    }
}
