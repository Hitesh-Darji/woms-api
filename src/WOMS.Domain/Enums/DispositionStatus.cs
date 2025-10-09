using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum DispositionStatus
    {
        [Description("None")]
        None = 0,
        
        [Description("Pending")]
        Pending = 1,
        
        [Description("Approved")]
        Approved = 2,
        
        [Description("Rejected")]
        Rejected = 3,
        
        [Description("In Progress")]
        InProgress = 4,
        
        [Description("Completed")]
        Completed = 5
    }
}
