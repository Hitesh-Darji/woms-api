using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum EquipmentStatus
    {
        [Description("Available")]
        Available = 0,
        
        [Description("In Use")]
        InUse = 1,
        
        [Description("Maintenance")]
        Maintenance = 2,
        
        [Description("Out of Service")]
        OutOfService = 3,
        
        [Description("Retired")]
        Retired = 4,
        
        [Description("Lost")]
        Lost = 5,
        
        [Description("Stolen")]
        Stolen = 6
    }
}
