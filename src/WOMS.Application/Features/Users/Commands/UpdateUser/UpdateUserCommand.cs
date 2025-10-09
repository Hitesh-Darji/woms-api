using MediatR;
using WOMS.Application.Features.Users.DTOs;

namespace WOMS.Application.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand : IRequest<UserDto>
    {
        public Guid Id { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string Address { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string PostalCode { get; init; } = string.Empty;
        public string? Phone { get; init; }
        public string Email { get; init; } = string.Empty;
        public Guid RoleId { get; init; }
        public Guid UpdatedBy { get; init; }
    }
}
