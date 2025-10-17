using MediatR;
using WOMS.Application.Features.Assignment.DTOs;

namespace WOMS.Application.Features.Assignment.Queries.GetTechnicianStatus
{
    public class GetTechnicianStatusQuery : IRequest<TechnicianStatusResponse>
    {
        public string? Status { get; set; }
        public string? Location { get; set; }
    }
}
