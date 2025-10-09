using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum EquipmentCategory
    {
        [Description("Hand Tool")]
        HandTool = 0,
        [Description("Power Tool")]
        PowerTool = 1,
        [Description("Safety Equipment")]
        SafetyEquipment = 2,
        [Description("Test Equipment")]
        TestEquipment = 3,
        [Description("Vehicle")]
        Vehicle = 4,
        [Description("Computer")]
        Computer = 5,
        [Description("Other")]
        Other = 6,
    }
}
