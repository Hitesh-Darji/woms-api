using AutoMapper;
using MediatR;
using WOMS.Application.Features.Location.DTOs;
using WOMS.Domain.Repositories;
using WOMS.Application.Interfaces;

namespace WOMS.Application.Features.Location.Commands.UpdateLocation
{
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, LocationDto>
    {
        private readonly IRepository<WOMS.Domain.Entities.Location> _locationRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLocationCommandHandler(IRepository<WOMS.Domain.Entities.Location> locationRepository, AutoMapper.IMapper mapper, IUnitOfWork unitOfWork)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<LocationDto> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(request.Id);
            if (location == null)
            {
                throw new ArgumentException($"Location with ID {request.Id} not found.");
            }

            // Validate parent location exists if provided
            if (request.ParentLocationId.HasValue)
            {
                var parentLocation = await _locationRepository.GetByIdAsync(request.ParentLocationId.Value);
                if (parentLocation == null || parentLocation.IsDeleted)
                {
                    throw new ArgumentException($"Parent location with ID {request.ParentLocationId.Value} not found or is deleted.");
                }
                
                // Prevent circular reference (location cannot be its own parent)
                if (request.ParentLocationId.Value == request.Id)
                {
                    throw new ArgumentException("A location cannot be its own parent.");
                }
            }

            location.Name = request.Name;
            location.Type = request.Type;
            location.Address = request.Address;
            location.Manager = request.Manager;
            location.ParentLocationId = request.ParentLocationId;
            location.IsActive = request.IsActive;
            location.UpdatedOn = DateTime.UtcNow;
            location.UpdatedBy = request.UpdatedBy;

            await _locationRepository.UpdateAsync(location);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<LocationDto>(location);
        }
    }
}
