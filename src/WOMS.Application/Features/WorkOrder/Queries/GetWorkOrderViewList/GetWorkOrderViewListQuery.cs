using MediatR;
using WOMS.Application.Features.WorkOrder.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.WorkOrder.Queries.GetWorkOrderViewList
{
    /// <summary>
    /// Query for getting WorkOrder view/list data - optimized for UI components
    /// </summary>
    public class GetWorkOrderViewListQuery : IRequest<WorkOrderViewListResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public WorkOrderStatus? Status { get; set; }
        public WorkOrderPriority? Priority { get; set; }
        public string? AssignedTechnicianId { get; set; }
        public bool? IsOverdue { get; set; }
        public bool? IsToday { get; set; }
        public string? SortBy { get; set; } = "CreatedOn";
        public bool SortDescending { get; set; } = true;
        public bool IncludeSummary { get; set; } = true;
    }
}
