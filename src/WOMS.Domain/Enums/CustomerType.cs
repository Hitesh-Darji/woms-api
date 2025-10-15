using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum CustomerType
    {
        [Description("Individual")]
        Individual = 0,
        
        [Description("Business")]
        Business = 1,
        
        [Description("Government")]
        Government = 2
    }
}

