using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum DispositionType
    {
        [Description("Reinstall")]
        Reinstall = 0,
        
        [Description("Refurbish")]
        Refurbish = 1,
        
        [Description("Return to Vendor")]
        ReturnVendor = 2,
        
        [Description("Scrap")]
        Scrap = 3,
        
        [Description("Sell")]
        Sell = 4,
        
        [Description("Donate")]
        Donate = 5,
        
        [Description("Dispose")]
        Dispose = 6
    }
}
