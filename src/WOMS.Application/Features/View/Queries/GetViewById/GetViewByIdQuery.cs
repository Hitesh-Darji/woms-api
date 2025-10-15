using MediatR;
using WOMS.Application.Features.View.DTOs;

namespace WOMS.Application.Features.View.Queries.GetViewById
{
    public class GetViewByIdQuery : IRequest<ViewDto?>
    {
        public Guid Id { get; set; }
    }
}
