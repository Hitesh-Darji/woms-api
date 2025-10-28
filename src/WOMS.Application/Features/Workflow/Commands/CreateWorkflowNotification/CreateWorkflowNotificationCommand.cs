using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.Workflow.Commands.CreateWorkflowNotification
{
    public class CreateWorkflowNotificationCommand : IRequest<WorkflowNotificationDto>
    {
        public Guid WorkflowId { get; set; }
        public string Name { get; set; } = string.Empty;
        public WorkflowNotificationType Type { get; set; } = WorkflowNotificationType.Email;
        public List<string> Recipients { get; set; } = new List<string>();
        public string Template { get; set; } = string.Empty;
        public List<string> Triggers { get; set; } = new List<string>();
        public bool IsActive { get; set; } = true;
        public int OrderIndex { get; set; }
    }
}

