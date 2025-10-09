using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum InventoryCategory
    {
        [Description("Raw Material")]
        RawMaterial = 0,
        
        [Description("Finished Good")]
        FinishedGood = 1,
        
        [Description("Component")]
        Component = 2,
        
        [Description("Tool")]
        Tool = 3,
        
        [Description("Equipment")]
        Equipment = 4,
        
        [Description("Consumable")]
        Consumable = 5,
        
        [Description("Service")]
        Service = 6
    }
}
