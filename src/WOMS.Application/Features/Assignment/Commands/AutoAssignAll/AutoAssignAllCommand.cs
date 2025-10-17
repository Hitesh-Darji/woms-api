using MediatR;
using WOMS.Application.Features.Assignment.DTOs;

namespace WOMS.Application.Features.Assignment.Commands.AutoAssignAll
{
    public class AutoAssignAllCommand : IRequest<AutoAssignAllResponse>
    {
        public bool ForceReassignment { get; set; }
        public string? PriorityFilter { get; set; }
    }
}
