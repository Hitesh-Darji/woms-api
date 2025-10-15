using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.Workflow.Queries.GetAllWorkflows
{
    public class GetAllWorkflowsQuery : IRequest<WorkflowListGetResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public WorkflowCategory? Category { get; set; }
        public bool? IsActive { get; set; }
        public string? SortBy { get; set; } = "CreatedAt";
        public bool SortDescending { get; set; } = true;
    }
}
