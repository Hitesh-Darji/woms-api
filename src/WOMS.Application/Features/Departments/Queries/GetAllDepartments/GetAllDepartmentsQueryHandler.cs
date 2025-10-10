using AutoMapper;
using MediatR;
using WOMS.Application.Features.Departments.DTOs;
using WOMS.Application.Features.Departments.Queries.GetAllDepartments;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Departments.Queries.GetAllDepartments
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentDto>>
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly AutoMapper.IMapper _mapper;

        public GetAllDepartmentsQueryHandler(
            IRepository<Department> departmentRepository,
            AutoMapper.IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.GetAsync(
                d => !d.IsDeleted, cancellationToken);

            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }
    }
}
