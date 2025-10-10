using MediatR;

namespace WOMS.Application.Features.Departments.Commands.DeleteDepartment
{
    public record DeleteDepartmentCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
        public Guid DeletedBy { get; init; }
    }
}
