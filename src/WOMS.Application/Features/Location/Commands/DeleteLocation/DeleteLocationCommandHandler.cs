using MediatR;
using WOMS.Domain.Repositories;
using WOMS.Application.Interfaces;

namespace WOMS.Application.Features.Location.Commands.DeleteLocation
{
    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, bool>
    {
        private readonly IRepository<WOMS.Domain.Entities.Location> _locationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLocationCommandHandler(IRepository<WOMS.Domain.Entities.Location> locationRepository, IUnitOfWork unitOfWork)
        {
            _locationRepository = locationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(request.Id);
            if (location == null)
            {
                return false;
            }

            // Soft delete
            location.IsDeleted = true;
            location.DeletedOn = DateTime.UtcNow;

            await _locationRepository.UpdateAsync(location);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
