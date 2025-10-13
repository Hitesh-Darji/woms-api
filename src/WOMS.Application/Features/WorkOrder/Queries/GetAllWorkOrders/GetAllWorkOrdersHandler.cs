using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WOMS.Application.Features.WorkOrder.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.WorkOrder.Queries.GetAllWorkOrders
{
    public class GetAllWorkOrdersHandler : IRequestHandler<GetAllWorkOrdersQuery, WorkOrderListResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllWorkOrdersHandler(
            IWorkOrderRepository workOrderRepository,
            IUnitOfWork unitOfWork)
        {
            _workOrderRepository = workOrderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkOrderListResponse> Handle(GetAllWorkOrdersQuery request, CancellationToken cancellationToken)
        {
            // Use projection-based repository method for maximum performance
            var (workOrderData, totalCount) = await _workOrderRepository.GetWorkOrderDtosWithProjectionAsync(
                request.PageNumber,
                request.PageSize,
                request.SearchTerm,
                request.Status,
                request.Priority,
                request.AssignedTechnicianId,
                request.WorkOrderTypeId,
                request.ScheduledDateFrom,
                request.ScheduledDateTo,
                request.SortBy,
                request.SortDescending,
                cancellationToken);

            // Convert dynamic objects to DTOs efficiently
            var workOrderDtosList = workOrderData.Select(item => new WorkOrderDto
            {
                Id = item.Id,
                WorkOrderNumber = item.WorkOrderNumber,
                WorkflowId = item.WorkflowId,
                WorkOrderTypeId = item.WorkOrderTypeId,
                WorkOrderTypeName = item.WorkOrderTypeName,
                Priority = item.Priority,
                Status = item.Status,
                ServiceAddress = item.ServiceAddress,
                MeterNumber = item.MeterNumber,
                CurrentReading = item.CurrentReading,
                AssignedTechnicianId = item.AssignedTechnicianId,
                AssignedTechnicianName = item.AssignedTechnicianName,
                Notes = item.Notes,
                ScheduledDate = item.ScheduledDate,
                StartedAt = item.StartedAt,
                CompletedAt = item.CompletedAt,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,
                DueDate = item.DueDate,
                Utility = item.Utility,
                Make = item.Make,
                Model = item.Model,
                Size = item.Size,
                Location = item.Location
            }).ToList();

            // Get additional work order data for fields not in projection
            var workOrderIds = workOrderDtosList.Select(dto => dto.Id).ToList();
            var additionalDataDict = await _workOrderRepository.GetQueryable()
                .AsNoTracking()
                .Where(wo => workOrderIds.Contains(wo.Id))
                .Select(wo => new { 
                    wo.Id, 
                    wo.Utility, 
                    wo.Make, 
                    wo.Model, 
                    wo.Size, 
                    wo.Location,
                    wo.Assignee
                })
                .ToDictionaryAsync(wo => wo.Id, cancellationToken);

            // Populate additional fields
            workOrderDtosList.AsParallel().ForAll(dto =>
            {
                if (additionalDataDict.TryGetValue(dto.Id, out var data))
                {
                    dto.Utility = data.Utility;
                    dto.Make = data.Make;
                    dto.Model = data.Model;
                    dto.Size = data.Size;
                    dto.Location = data.Location;
                    dto.AssignedTechnicianName = data.Assignee;
                    dto.AssignedTechnicianId = data.Assignee;
                }
            });

            return new WorkOrderListResponse
            {
                WorkOrders = workOrderDtosList,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
