using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum WorkOrderPriority
    {
        [Description("Low")]
        Low = 0,
        
        [Description("Medium")]
        Medium = 1,
        
        [Description("High")]
        High = 2,
        
        [Description("Critical")]
        Critical = 3
    }

    public enum WorkOrderStatus
    {
        [Description("Assigned")]
        Assigned = 0,
        
        [Description("Unassigned")]
        Unassigned = 1,
        
        [Description("Completed")]
        Completed = 2
    }

    public enum WorkOrderType
    {
        [Description("Meter Installation")]
        MeterInstallation = 0,
        
        [Description("Meter")]
        Meter = 1,
        
        [Description("Maintenance")]
        Maintenance = 2,
        
        [Description("Repair")]
        Repair = 3,
        
        [Description("Inspection")]
        Inspection = 4
    }
}
