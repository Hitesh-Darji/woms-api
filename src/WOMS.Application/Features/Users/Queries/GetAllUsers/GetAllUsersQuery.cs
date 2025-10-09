using MediatR;
using WOMS.Application.Features.Users.DTOs;

namespace WOMS.Application.Features.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }
}
