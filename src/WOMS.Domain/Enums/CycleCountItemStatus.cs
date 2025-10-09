using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum CycleCountItemStatus
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Counted")]
        Counted = 1,
        [Description("Variance")]
        Variance = 2,
        [Description("Approved")]
        Approved = 3,
        [Description("Rejected")]
        Rejected = 4,
    }
}
