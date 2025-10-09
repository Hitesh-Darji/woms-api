using MediatR;
using WOMS.Application.Features.Roles.DTOs;

namespace WOMS.Application.Features.Roles.Queries.GetRoleById
{
    public record GetRoleByIdQuery : IRequest<RoleDto?>
    {
        public string Id { get; init; } = string.Empty;
    }
}
