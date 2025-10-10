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

            // Get metadata for all work orders in a single query
            var workOrderIds = workOrderDtosList.Select(dto => dto.Id).ToList();
            var metadataDict = await _workOrderRepository.GetQueryable()
                .AsNoTracking()
                .Where(wo => workOrderIds.Contains(wo.Id))
                .Select(wo => new { wo.Id, wo.Metadata })
                .ToDictionaryAsync(wo => wo.Id, wo => wo.Metadata, cancellationToken);

            // Populate metadata fields in parallel
            workOrderDtosList.AsParallel().ForAll(dto =>
            {
                if (metadataDict.TryGetValue(dto.Id, out var metadata) && !string.IsNullOrEmpty(metadata))
                {
                    dto.Utility = ExtractFromMetadata(metadata, "utility");
                    dto.Make = ExtractFromMetadata(metadata, "make");
                    dto.Model = ExtractFromMetadata(metadata, "model");
                    dto.Size = ExtractFromMetadata(metadata, "size");
                    dto.Location = ExtractFromMetadata(metadata, "location");
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

        // Optimized method to get assigned technician name
        private static string? GetAssignedTechnicianName(WOMS.Domain.Entities.WorkOrder wo)
        {
            if (!string.IsNullOrEmpty(wo.AssignedTechnicianName))
                return wo.AssignedTechnicianName;

            if (wo.AssignedTechnician != null)
                return $"{wo.AssignedTechnician.FirstName} {wo.AssignedTechnician.LastName}";

            return null;
        }

        // Optimized metadata extraction with caching
        private static readonly Dictionary<string, Dictionary<string, string?>> _metadataCache = new();
        
        private static string? ExtractFromMetadata(string? metadata, string key)
        {
            if (string.IsNullOrEmpty(metadata))
                return null;

            // Use caching for frequently accessed metadata
            if (_metadataCache.TryGetValue(metadata, out var cachedDict))
            {
                return cachedDict.TryGetValue(key, out var cachedValue) ? cachedValue : null;
            }

            try
            {
                var metadataDict = JsonSerializer.Deserialize<Dictionary<string, object>>(metadata);
                var resultDict = new Dictionary<string, string?>();
                
                if (metadataDict != null)
                {
                    foreach (var kvp in metadataDict)
                    {
                        resultDict[kvp.Key] = kvp.Value?.ToString();
                    }
                }

                // Cache the result
                _metadataCache[metadata] = resultDict;
                
                return resultDict.TryGetValue(key, out var value) ? value : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
