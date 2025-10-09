using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum TransactionStatus
    {
        [Description("Pending")]
        Pending = 0,
        
        [Description("Completed")]
        Completed = 1,
        
        [Description("Cancelled")]
        Cancelled = 2,
        
        [Description("Failed")]
        Failed = 3,
        
        [Description("In Progress")]
        InProgress = 4
    }
}
