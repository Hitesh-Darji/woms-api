using AutoMapper;
using MediatR;
using WOMS.Application.Features.Location.DTOs;
using WOMS.Domain.Repositories;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;

namespace WOMS.Application.Features.Location.Commands.CreateLocation
{
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, LocationDto>
    {
        private readonly IRepository<WOMS.Domain.Entities.Location> _locationRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLocationCommandHandler(IRepository<WOMS.Domain.Entities.Location> locationRepository, AutoMapper.IMapper mapper, IUnitOfWork unitOfWork)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<LocationDto> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            // Validate parent location exists if provided
            if (request.ParentLocationId.HasValue)
            {
                var parentLocation = await _locationRepository.GetByIdAsync(request.ParentLocationId.Value);
                if (parentLocation == null || parentLocation.IsDeleted)
                {
                    throw new ArgumentException($"Parent location with ID {request.ParentLocationId.Value} not found or is deleted.");
                }
            }

            var location = new WOMS.Domain.Entities.Location
            {
                Name = request.Name,
                Type = request.Type,
                Address = request.Address,
                Manager = request.Manager,
                ParentLocationId = request.ParentLocationId,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy
            };

            await _locationRepository.AddAsync(location);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<LocationDto>(location);
        }
    }
}
