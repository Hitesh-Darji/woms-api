using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum BillingScheduleFrequency
    {
        [Description("Daily")]
        Daily = 0,
        
        [Description("Weekly")]
        Weekly = 1,
        
        [Description("Monthly")]
        Monthly = 2,
        
        [Description("Quarterly")]
        Quarterly = 3
    }
}

