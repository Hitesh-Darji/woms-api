using MediatR;
using WOMS.Application.Features.Departments.Commands.DeleteDepartment;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDepartmentCommandHandler(
            IRepository<Department> departmentRepository,
            IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (department == null || department.IsDeleted)
            {
                return false;
            }

            // Soft delete
            department.IsDeleted = true;
            department.DeletedBy = request.DeletedBy;
            department.DeletedOn = DateTime.UtcNow;

            await _departmentRepository.UpdateAsync(department, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
