using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum InvoiceStatus
    {
        [Description("Draft")]
        Draft = 0,
        
        [Description("Pending Approval")]
        PendingApproval = 1,
        
        [Description("Approved")]
        Approved = 2,
        
        [Description("Sent")]
        Sent = 3,
        
        [Description("Paid")]
        Paid = 4,
        
        [Description("Overdue")]
        Overdue = 5
    }
}
