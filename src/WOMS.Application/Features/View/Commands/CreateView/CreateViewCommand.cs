using MediatR;
using WOMS.Application.Features.View.DTOs;

namespace WOMS.Application.Features.View.Commands.CreateView
{
    public record CreateViewCommand : IRequest<ViewDto>
    {
        public string Name { get; init; } = string.Empty;
        public List<string> SelectedColumns { get; init; } = new List<string>();
        public string CreatedBy { get; init; } = string.Empty;
    }
}
