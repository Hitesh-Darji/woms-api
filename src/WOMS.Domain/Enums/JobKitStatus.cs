using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum JobKitStatus
    {
        [Description("Active")]
        Active = 0,
        [Description("Inactive")]
        Inactive = 1,
        [Description("Maintenance")]
        Maintenance = 2,
        [Description("Retired")]
        Retired = 3,
    }
}
