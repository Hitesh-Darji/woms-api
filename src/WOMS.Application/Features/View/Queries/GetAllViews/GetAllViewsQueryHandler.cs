using MediatR;
using WOMS.Application.Features.View.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.View.Queries.GetAllViews
{
    public class GetAllViewsQueryHandler : IRequestHandler<GetAllViewsQuery, List<ViewDto>>
    {
        private readonly IRepository<WOMS.Domain.Entities.View> _viewRepository;

        public GetAllViewsQueryHandler(IRepository<WOMS.Domain.Entities.View> viewRepository)
        {
            _viewRepository = viewRepository;
        }

        public async Task<List<ViewDto>> Handle(GetAllViewsQuery request, CancellationToken cancellationToken)
        {
            var views = await _viewRepository.FindAsync(
                v => !v.IsDeleted && (request.UserId == null || v.CreatedBy == request.UserId),
                cancellationToken);

            var result = new List<ViewDto>();

            foreach (var view in views)
            {
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

                result.Add(new ViewDto
                {
                    Id = view.Id,
                    Name = view.Name,
                    SelectedColumns = selectedColumns
                });
            }

            return result;
        }
    }
}
