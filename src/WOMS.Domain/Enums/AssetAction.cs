using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum AssetAction
    {
        [Description("Install")]
        Install = 0,
        [Description("Remove")]
        Remove = 1,
        [Description("Transfer")]
        Transfer = 2,
        [Description("Maintenance")]
        Maintenance = 3,
        [Description("Repair")]
        Repair = 4,
        [Description("Replace")]
        Replace = 5,
        [Description("Dispose")]
        Dispose = 6,
    }
}
