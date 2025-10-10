using MediatR;
using WOMS.Application.Features.View.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.View.Commands.CreateView
{
    public class CreateViewCommandHandler : IRequestHandler<CreateViewCommand, ViewDto>
    {
        private readonly IRepository<WOMS.Domain.Entities.View> _viewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;

        public CreateViewCommandHandler(
            IRepository<WOMS.Domain.Entities.View> viewRepository,
            IUnitOfWork unitOfWork,
            AutoMapper.IMapper mapper)
        {
            _viewRepository = viewRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ViewDto> Handle(CreateViewCommand request, CancellationToken cancellationToken)
        {
            // Check if view with same name already exists
            var existingView = await _viewRepository.GetFirstOrDefaultAsync(
                v => v.Name == request.Name && !v.IsDeleted,
                cancellationToken);

            if (existingView != null)
            {
                throw new InvalidOperationException($"View with name '{request.Name}' already exists.");
            }

            // Serialize selected columns to JSON
            var selectedColumnsJson = System.Text.Json.JsonSerializer.Serialize(request.SelectedColumns);

            var newView = new WOMS.Domain.Entities.View
            {
                Name = request.Name,
                SelectedColumns = selectedColumnsJson,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            await _viewRepository.AddAsync(newView, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Create ViewDto with the selected columns
            return new ViewDto
            {
                Id = newView.Id,
                Name = newView.Name,
                SelectedColumns = request.SelectedColumns
            };
        }
    }
}
