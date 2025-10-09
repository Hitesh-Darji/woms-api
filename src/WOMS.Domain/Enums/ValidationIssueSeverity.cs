using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum ValidationIssueSeverity
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
}
