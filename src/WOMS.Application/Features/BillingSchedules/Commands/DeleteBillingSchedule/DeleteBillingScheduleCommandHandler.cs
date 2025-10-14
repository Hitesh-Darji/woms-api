using MediatR;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;
using WOMS.Domain.Entities;

namespace WOMS.Application.Features.BillingSchedules.Commands.DeleteBillingSchedule
{
    public class DeleteBillingScheduleCommandHandler : IRequestHandler<DeleteBillingScheduleCommand, Unit>
    {
        private readonly IBillingScheduleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBillingScheduleCommandHandler(IBillingScheduleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteBillingScheduleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity != null)
            {
                // Soft delete pattern consistent with other features
                if (entity is BaseEntity be)
                {
                    be.IsDeleted = true;
                    be.DeletedOn = DateTime.UtcNow;
                }
                await _repository.UpdateAsync(entity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}


