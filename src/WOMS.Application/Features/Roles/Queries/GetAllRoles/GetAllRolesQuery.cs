using MediatR;
using WOMS.Application.Features.Roles.DTOs;

namespace WOMS.Application.Features.Roles.Queries.GetAllRoles
{
    public record GetAllRolesQuery : IRequest<IEnumerable<RoleDto>>
    {
    }
}
