using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum SyncStatus
    {
        [Description("Pending")]
        Pending = 0,
        
        [Description("Synced")]
        Synced = 1,
        
        [Description("Failed")]
        Failed = 2
    }
}

