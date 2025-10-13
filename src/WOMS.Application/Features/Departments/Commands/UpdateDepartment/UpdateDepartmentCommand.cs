using MediatR;
using WOMS.Application.Features.Departments.DTOs;

namespace WOMS.Application.Features.Departments.Commands.UpdateDepartment
{
    public record UpdateDepartmentCommand : IRequest<DepartmentDto>
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public string? Code { get; init; }
        public string Status { get; init; } = "Active";
        public bool IsActive { get; init; } = true;
        public string UpdatedBy { get; init; } = string.Empty;
    }
}
