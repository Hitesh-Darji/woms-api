using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WOMS.Application.Features.WorkOrder.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.WorkOrder.Queries.GetWorkOrderViewList
{
    /// <summary>
    /// Handler for GetWorkOrderViewListQuery - optimized for UI components
    /// </summary>
    public class GetWorkOrderViewListHandler : IRequestHandler<GetWorkOrderViewListQuery, WorkOrderViewListResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetWorkOrderViewListHandler(
            IWorkOrderRepository workOrderRepository,
            IUnitOfWork unitOfWork)
        {
            _workOrderRepository = workOrderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkOrderViewListResponse> Handle(GetWorkOrderViewListQuery request, CancellationToken cancellationToken)
        {
            // Use the existing GetPaginatedAsync method from the repository
            var (workOrders, totalCount) = await _workOrderRepository.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                request.SearchTerm,
                request.Status,
                request.Priority,
                request.AssignedTechnicianId,
                request.SortBy,
                request.SortDescending,
                cancellationToken);

            // Convert to WorkOrderViewDto
            var workOrderViews = workOrders.Select(wo => new WorkOrderViewDto
            {
                Id = wo.Id,
                WorkOrderNumber = wo.WorkOrderNumber,
                Status = wo.Status.ToString(),
                Priority = wo.Priority.ToString(),
                ServiceAddress = wo.Address ?? string.Empty,
                AssignedTechnicianName = wo.Assignee,
                CreatedAt = wo.CreatedOn,
                CustomerName = wo.Customer,
                CustomerPhone = wo.CustomerContact,
                IsOverdue = wo.DueDate.HasValue && wo.DueDate < DateTime.UtcNow,
                DaysSinceCreated = (int)(DateTime.UtcNow - wo.CreatedOn).TotalDays,
                StatusColor = GetStatusColor(wo.Status),
                PriorityColor = GetPriorityColor(wo.Priority)
            }).ToList();

            return new WorkOrderViewListResponse
            {
                WorkOrders = workOrderViews,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                StatusCounts = new Dictionary<string, int>(),
                PriorityCounts = new Dictionary<string, int>(),
                OverdueCount = 0,
                TodayCount = 0
            };
        }

        // Helper methods for UI styling
        private static string GetStatusColor(WOMS.Domain.Enums.WorkOrderStatus status)
        {
            return status switch
            {
                WOMS.Domain.Enums.WorkOrderStatus.Pending => "yellow",
                WOMS.Domain.Enums.WorkOrderStatus.InProgress => "blue",
                WOMS.Domain.Enums.WorkOrderStatus.Completed => "green",
                WOMS.Domain.Enums.WorkOrderStatus.Cancelled => "red",
                _ => "gray"
            };
        }

        private static string GetPriorityColor(WOMS.Domain.Enums.WorkOrderPriority priority)
        {
            return priority switch
            {
                WOMS.Domain.Enums.WorkOrderPriority.High => "red",
                WOMS.Domain.Enums.WorkOrderPriority.Medium => "orange",
                WOMS.Domain.Enums.WorkOrderPriority.Low => "green",
                _ => "gray"
            };
        }
    }
}
