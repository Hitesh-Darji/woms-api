using MediatR;

namespace WOMS.Application.Features.Workflow.Commands.SendWorkflowNotification
{
    public class SendWorkflowNotificationCommand : IRequest<bool>
    {
        public Guid WorkflowId { get; set; }
        public string Trigger { get; set; } = string.Empty;
        public Dictionary<string, object>? TemplateData { get; set; }
    }
}

