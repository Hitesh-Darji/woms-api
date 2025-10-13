using AutoMapper;
using MediatR;
using WOMS.Application.Features.Departments.Commands.UpdateDepartment;
using WOMS.Application.Features.Departments.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, DepartmentDto>
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDepartmentCommandHandler(
            IRepository<Department> departmentRepository,
            AutoMapper.IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (department == null || department.IsDeleted)
            {
                throw new InvalidOperationException($"Department with ID '{request.Id}' not found.");
            }

            // Check if another department with same name already exists
            var existingDepartment = await _departmentRepository.GetFirstOrDefaultAsync(
                d => d.Name == request.Name && d.Id != request.Id && !d.IsDeleted, cancellationToken);
            
            if (existingDepartment != null)
            {
                throw new InvalidOperationException($"Department with name '{request.Name}' already exists.");
            }

            // Check if another department with same code already exists (if code is provided)
            if (!string.IsNullOrEmpty(request.Code))
            {
                var existingCodeDepartment = await _departmentRepository.GetFirstOrDefaultAsync(
                    d => d.Code == request.Code && d.Id != request.Id && !d.IsDeleted, cancellationToken);
                
                if (existingCodeDepartment != null)
                {
                    throw new InvalidOperationException($"Department with code '{request.Code}' already exists.");
                }
            }

            department.Name = request.Name;
            department.Description = request.Description;
            department.Code = request.Code;
            department.IsActive = request.IsActive;
            department.UpdatedBy = request.UpdatedBy;
            department.UpdatedOn = DateTime.UtcNow;

            await _departmentRepository.UpdateAsync(department, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DepartmentDto>(department);
        }
    }
}
