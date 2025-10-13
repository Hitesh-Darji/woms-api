using AutoMapper;
using MediatR;
using WOMS.Application.Features.Departments.Commands.CreateDepartment;
using WOMS.Application.Features.Departments.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDepartmentCommandHandler(
            IRepository<Department> departmentRepository,
            AutoMapper.IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            // Check if department with same name already exists
            var existingDepartment = await _departmentRepository.GetFirstOrDefaultAsync(
                d => d.Name == request.Name && !d.IsDeleted, cancellationToken);
            
            if (existingDepartment != null)
            {
                throw new InvalidOperationException($"Department with name '{request.Name}' already exists.");
            }

            // Check if department with same code already exists (if code is provided)
            if (!string.IsNullOrEmpty(request.Code))
            {
                var existingCodeDepartment = await _departmentRepository.GetFirstOrDefaultAsync(
                    d => d.Code == request.Code && !d.IsDeleted, cancellationToken);
                
                if (existingCodeDepartment != null)
                {
                    throw new InvalidOperationException($"Department with code '{request.Code}' already exists.");
                }
            }

            var department = new Department
            {
                Name = request.Name,
                Description = request.Description,
                Code = request.Code,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            await _departmentRepository.AddAsync(department, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DepartmentDto>(department);
        }
    }
}
