using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum CycleCountType
    {
        [Description("Full Count")]
        FullCount = 0,
        [Description("Partial Count")]
        PartialCount = 1,
        [Description("Spot Check")]
        SpotCheck = 2,
        [Description("Cycle Count")]
        CycleCount = 3,
    }
}
