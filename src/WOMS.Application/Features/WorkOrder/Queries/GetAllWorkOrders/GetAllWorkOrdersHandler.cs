using MediatR;
using WOMS.Application.Features.WorkOrder.DTOs;
using WOMS.Application.Interfaces;
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

            // Convert to DTOs using AutoMapper or manual mapping
            var workOrderDtosList = workOrders.Select(wo => new WorkOrderDto
            {
                Id = wo.Id,
                WorkOrderNumber = wo.WorkOrderNumber,
                WorkflowId = wo.WorkflowId,
                Priority = wo.Priority.ToString(),
                Status = wo.Status.ToString(),
                ServiceAddress = wo.Address ?? string.Empty,
                AssignedTechnicianId = wo.Assignee,
                AssignedTechnicianName = wo.Assignee,
                Notes = wo.Notes,
                CreatedAt = wo.CreatedOn,
                UpdatedAt = wo.UpdatedOn,
                DueDate = wo.DueDate,
                Utility = wo.Utility,
                Make = wo.Make,
                Model = wo.Model,
                Size = wo.Size,
                Location = wo.Location
            }).ToList();

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
