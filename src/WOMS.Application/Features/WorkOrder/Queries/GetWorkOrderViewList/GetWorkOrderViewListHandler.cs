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
            // Get view/list data with optimized projection
            var (workOrderData, totalCount) = await _workOrderRepository.GetWorkOrderViewListAsync(
                request.PageNumber,
                request.PageSize,
                request.SearchTerm,
                request.Status,
                request.Priority,
                request.AssignedTechnicianId,
                request.IsOverdue,
                request.IsToday,
                request.SortBy,
                request.SortDescending,
                cancellationToken);

            // Convert dynamic objects to DTOs efficiently
            var workOrderViews = workOrderData.Select(item => new WorkOrderViewDto
            {
                Id = item.Id,
                WorkOrderNumber = item.WorkOrderNumber,
                Status = item.Status,
                Priority = item.Priority,
                ServiceAddress = item.ServiceAddress,
                AssignedTechnicianName = item.AssignedTechnicianName,
                ScheduledDate = item.ScheduledDate,
                CreatedAt = item.CreatedAt,
                WorkOrderTypeName = item.WorkOrderTypeName,
                CustomerName = item.CustomerName,
                CustomerPhone = item.CustomerPhone,
                IsOverdue = item.IsOverdue,
                DaysSinceCreated = item.DaysSinceCreated,
                StatusColor = item.StatusColor,
                PriorityColor = item.PriorityColor
            }).ToList();

            // Get summary statistics if requested
            Dictionary<string, int> statusCounts = new();
            Dictionary<string, int> priorityCounts = new();
            int overdueCount = 0;
            int todayCount = 0;

            if (request.IncludeSummary)
            {
                var summary = await _workOrderRepository.GetWorkOrderViewSummaryAsync(cancellationToken);
                
                statusCounts = new Dictionary<string, int>
                {
                    ["pending"] = (int)summary["PendingCount"],
                    ["assigned"] = (int)summary["AssignedCount"],
                    ["in_progress"] = (int)summary["InProgressCount"],
                    ["completed"] = (int)summary["CompletedCount"],
                    ["cancelled"] = (int)summary["CancelledCount"]
                };

                priorityCounts = new Dictionary<string, int>
                {
                    ["high"] = (int)summary["HighPriorityCount"],
                    ["medium"] = (int)summary["MediumPriorityCount"],
                    ["low"] = (int)summary["LowPriorityCount"]
                };

                overdueCount = (int)summary["OverdueCount"];
                todayCount = (int)summary["TodayCount"];
            }

            return new WorkOrderViewListResponse
            {
                WorkOrders = workOrderViews,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                StatusCounts = statusCounts,
                PriorityCounts = priorityCounts,
                OverdueCount = overdueCount,
                TodayCount = todayCount
            };
        }

        // Helper method for metadata extraction
        private static string? ExtractFromMetadata(string? metadata, string key)
        {
            if (string.IsNullOrEmpty(metadata))
                return null;

            try
            {
                var metadataDict = JsonSerializer.Deserialize<Dictionary<string, object>>(metadata);
                return metadataDict?.ContainsKey(key) == true ? metadataDict[key]?.ToString() : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
