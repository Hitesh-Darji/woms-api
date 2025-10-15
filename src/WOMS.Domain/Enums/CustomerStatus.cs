using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum CustomerStatus
    {
        [Description("Active")]
        Active = 0,
        
        [Description("Inactive")]
        Inactive = 1,
        
        [Description("Suspended")]
        Suspended = 2
    }
}

