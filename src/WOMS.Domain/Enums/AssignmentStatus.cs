using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum AssignmentStatus
    {
        [Description("Assigned")]
        Assigned = 0,
        
        [Description("Accepted")]
        Accepted = 1,
        
        [Description("Rejected")]
        Rejected = 2,
        
        [Description("Completed")]
        Completed = 3,
        
        [Description("Cancelled")]
        Cancelled = 4
    }
}
