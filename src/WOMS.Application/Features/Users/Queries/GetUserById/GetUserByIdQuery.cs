using MediatR;
using WOMS.Application.Features.Users.DTOs;

namespace WOMS.Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery : IRequest<UserDto?>
    {
        public Guid Id { get; init; }
    }
}