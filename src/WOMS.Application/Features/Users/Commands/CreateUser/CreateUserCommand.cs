using MediatR;
using WOMS.Application.Features.Users.DTOs;

namespace WOMS.Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<UserDto>
    {
        public string FullName { get; init; } = string.Empty;
        public string Address { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string PostalCode { get; init; } = string.Empty;
        public string? Phone { get; init; }
        public string Email { get; init; } = string.Empty;
        public Guid RoleId { get; init; }
        public Guid CreatedBy { get; init; }
    }
}
