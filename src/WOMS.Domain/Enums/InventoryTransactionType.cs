using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum InventoryTransactionType
    {
        [Description("Receipt")]
        Receipt = 0,
        
        [Description("Issue")]
        Issue = 1,
        
        [Description("Transfer")]
        Transfer = 2,
        
        [Description("Adjustment")]
        Adjustment = 3,
        
        [Description("Return")]
        Return = 4,
        
        [Description("Count")]
        Count = 5,
        
        [Description("Disposal")]
        Disposal = 6,
        
        [Description("Scrap")]
        Scrap = 7,
        
        [Description("Sale")]
        Sale = 8,
        
        [Description("Purchase")]
        Purchase = 9
    }
}
