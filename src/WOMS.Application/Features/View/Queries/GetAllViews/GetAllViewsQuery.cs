using MediatR;
using WOMS.Application.Features.View.DTOs;

namespace WOMS.Application.Features.View.Queries.GetAllViews
{
    public class GetAllViewsQuery : IRequest<List<ViewDto>>
    {
        public string? UserId { get; set; }
    }
}
