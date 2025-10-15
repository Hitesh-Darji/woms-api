using System.ComponentModel.DataAnnotations;

namespace WOMS.Application.Features.Workflow.Commands.ToggleWorkflowStatus
{
    public class ToggleWorkflowStatusRequest
    {
        [Required]
        public bool IsActive { get; set; }
    }
}
