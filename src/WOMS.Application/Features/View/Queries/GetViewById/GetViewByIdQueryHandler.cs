using MediatR;
using WOMS.Application.Features.View.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.View.Queries.GetViewById
{
    public class GetViewByIdQueryHandler : IRequestHandler<GetViewByIdQuery, ViewDto?>
    {
        private readonly IRepository<WOMS.Domain.Entities.View> _viewRepository;

        public GetViewByIdQueryHandler(IRepository<WOMS.Domain.Entities.View> viewRepository)
        {
            _viewRepository = viewRepository;
        }

        public async Task<ViewDto?> Handle(GetViewByIdQuery request, CancellationToken cancellationToken)
        {
            var view = await _viewRepository.GetFirstOrDefaultAsync(
                v => v.Id == request.Id && !v.IsDeleted,
                cancellationToken);

            if (view == null)
            {
                return null;
            }

            // Deserialize the selected columns from JSON
            var selectedColumns = new List<string>();
            if (!string.IsNullOrEmpty(view.SelectedColumns))
            {
                try
                {
                    selectedColumns = System.Text.Json.JsonSerializer.Deserialize<List<string>>(view.SelectedColumns) ?? new List<string>();
                }
                catch
                {
                    // If deserialization fails, return empty list
                    selectedColumns = new List<string>();
                }
            }

            return new ViewDto
            {
                Id = view.Id,
                Name = view.Name,
                SelectedColumns = selectedColumns
            };
        }
    }
}
