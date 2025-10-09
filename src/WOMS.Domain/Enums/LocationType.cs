using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum LocationType
    {
        [Description("Warehouse")]
        Warehouse = 0,
        [Description("Office")]
        Office = 1,
        [Description("Field")]
        Field = 2,
        [Description("Vehicle")]
        Vehicle = 3,
        [Description("Customer Site")]
        CustomerSite = 4,
        [Description("Supplier")]
        Supplier = 5,
        [Description("Other")]
        Other = 6,
    }
}
