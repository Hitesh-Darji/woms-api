using MediatR;
using WOMS.Application.Features.Departments.DTOs;

namespace WOMS.Application.Features.Departments.Commands.CreateDepartment
{
    public record CreateDepartmentCommand : IRequest<DepartmentDto>
    {
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public string? Code { get; init; }
        public string Status { get; init; } = "Active";
        public bool IsActive { get; init; } = true;
        public Guid CreatedBy { get; init; }
    }
}
