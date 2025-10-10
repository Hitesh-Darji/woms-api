using MediatR;
using WOMS.Application.Features.Departments.DTOs;

namespace WOMS.Application.Features.Departments.Queries.GetAllDepartments
{
    public record GetAllDepartmentsQuery : IRequest<IEnumerable<DepartmentDto>>;
}
