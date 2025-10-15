using MediatR;
using System.Text.Json.Serialization;
using WOMS.Application.Converters;
using WOMS.Application.Features.WorkOrder.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.WorkOrder.Commands.UpdateWorkOrder
{
    public class UpdateWorkOrderCommand : IRequest<WorkOrderDto>
    {
        public Guid Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public string? CustomerContact { get; set; }
        public WorkOrderType Type { get; set; } = WorkOrderType.MeterInstallation;
        public WorkOrderPriority Priority { get; set; } = WorkOrderPriority.Medium;
        public WorkOrderStatus Status { get; set; } = WorkOrderStatus.Pending;
        public string? Assignee { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? ActualHours { get; set; }
        public decimal? Cost { get; set; }
        public string? Tags { get; set; }
        public string? Equipment { get; set; }
        public string? Notes { get; set; }
        public string? Utility { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Size { get; set; }
        public string? ManagerTechnician { get; set; }
        [JsonConverter(typeof(NullableGuidConverter))]
        public Guid? WorkflowId { get; set; }
        [JsonConverter(typeof(NullableGuidConverter))]
        public Guid? FormTemplateId { get; set; }
        [JsonConverter(typeof(NullableGuidConverter))]
        public Guid? BillingTemplateId { get; set; }
    }
}
