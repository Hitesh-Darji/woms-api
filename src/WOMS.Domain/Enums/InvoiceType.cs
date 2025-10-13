using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum InvoiceType
    {
        [Description("Itemized")]
        Itemized = 0,
        
        [Description("Summary")]
        Summary = 1
    }
}
