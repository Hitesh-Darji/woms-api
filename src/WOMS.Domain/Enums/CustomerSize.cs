using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum CustomerSize
    {
        [Description("Small")]
        Small = 0,
        
        [Description("Medium")]
        Medium = 1,
        
        [Description("Large")]
        Large = 2,
        
        [Description("Enterprise")]
        Enterprise = 3
    }
}

