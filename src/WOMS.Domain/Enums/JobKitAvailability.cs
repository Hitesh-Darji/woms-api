using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum JobKitAvailability
    {
        [Description("Available")]
        Available = 0,
        [Description("In Use")]
        InUse = 1,
        [Description("Maintenance")]
        Maintenance = 2,
        [Description("Out of Service")]
        OutofService = 3,
        [Description("Retired")]
        Retired = 4,
    }
}
