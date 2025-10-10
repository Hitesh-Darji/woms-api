using MediatR;
using WOMS.Application.Features.Departments.DTOs;

namespace WOMS.Application.Features.Departments.Queries.GetDepartmentById
{
    public record GetDepartmentByIdQuery : IRequest<DepartmentDto?>
    {
        public Guid Id { get; init; }
    }
}
