using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum JobKitItemStatus
    {
        [Description("Required")]
        Required = 0,
        
        [Description("Optional")]
        Optional = 1,
        
        [Description("Excluded")]
        Excluded = 2,
        
        [Description("Unavailable")]
        Unavailable = 3
    }
}
