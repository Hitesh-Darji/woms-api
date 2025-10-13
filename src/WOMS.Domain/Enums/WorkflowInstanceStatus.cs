using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum WorkflowInstanceStatus
    {
        [Description("Running")]
        Running = 0,
        
        [Description("Completed")]
        Completed = 1,
        
        [Description("Failed")]
        Failed = 2,
        
        [Description("Paused")]
        Paused = 3
    }
}
