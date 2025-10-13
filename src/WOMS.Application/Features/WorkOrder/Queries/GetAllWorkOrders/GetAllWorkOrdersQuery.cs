using MediatR;
using WOMS.Application.Features.WorkOrder.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.WorkOrder.Queries.GetAllWorkOrders
{
    public class GetAllWorkOrdersQuery : IRequest<WorkOrderListResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public WorkOrderStatus? Status { get; set; }
        public WorkOrderPriority? Priority { get; set; }
        public string? AssignedTechnicianId { get; set; }
        public Guid? WorkOrderTypeId { get; set; }
        public DateTime? ScheduledDateFrom { get; set; }
        public DateTime? ScheduledDateTo { get; set; }
        public string? SortBy { get; set; } = "CreatedAt";
        public bool SortDescending { get; set; } = true;
    }
}
