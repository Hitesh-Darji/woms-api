using MediatR;
using WOMS.Application.DTOs;

namespace WOMS.Application.Queries
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserDto?>;
}
