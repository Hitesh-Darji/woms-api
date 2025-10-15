namespace WOMS.Application.Features.Workflow.Commands.ToggleWorkflowStatus
{
    public class ToggleWorkflowStatusResponse
    {
        public Guid WorkflowId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
