using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum CycleCountStatus
    {
        [Description("Planned")]
        Planned = 0,
        [Description("In Progress")]
        InProgress = 1,
        [Description("Completed")]
        Completed = 2,
        [Description("Cancelled")]
        Cancelled = 3,
        [Description("Approved")]
        Approved = 4,
    }
}
