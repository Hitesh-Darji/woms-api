using AutoMapper;
using MediatR;
using WOMS.Application.Features.Departments.DTOs;
using WOMS.Application.Features.Departments.Queries.GetDepartmentById;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto?>
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly AutoMapper.IMapper _mapper;

        public GetDepartmentByIdQueryHandler(
            IRepository<Department> departmentRepository,
            AutoMapper.IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<DepartmentDto?> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetFirstOrDefaultAsync(
                d => d.Id == request.Id && !d.IsDeleted, cancellationToken);

            if (department == null)
            {
                return null;
            }

            return _mapper.Map<DepartmentDto>(department);
        }
    }
}
