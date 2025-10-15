using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum StockRequestStatus
    {
        [Description("Pending")]
        Pending = 0,
        
        [Description("Approved")]
        Approved = 1,
        
        [Description("Rejected")]
        Rejected = 2,
        
        [Description("Partially Fulfilled")]
        PartiallyFulfilled = 3,
        
        [Description("Completed")]
        Completed = 4
    }
}

