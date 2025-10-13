using MediatR;

namespace WOMS.Application.Features.Departments.Commands.DeleteDepartment
{
    public record DeleteDepartmentCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
        public string DeletedBy { get; init; } = string.Empty;
    }
}
