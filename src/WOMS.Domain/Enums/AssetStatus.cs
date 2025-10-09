using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum AssetStatus
    {
        [Description("Available")]
        Available = 0,
        
        [Description("In Use")]
        InUse = 1,
        
        [Description("Maintenance")]
        Maintenance = 2,
        
        [Description("Decommissioned")]
        Decommissioned = 3,
        
        [Description("Do Not Install")]
        DoNotInstall = 4,
        
        [Description("Reserved")]
        Reserved = 5,
        
        [Description("Quarantine")]
        Quarantine = 6,
        
        [Description("Lost")]
        Lost = 7,
        
        [Description("Stolen")]
        Stolen = 8
    }
}
