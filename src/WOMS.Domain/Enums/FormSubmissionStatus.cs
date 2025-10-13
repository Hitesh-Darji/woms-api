using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum FormSubmissionStatus
    {
        [Description("Draft")]
        Draft = 0,
        
        [Description("Submitted")]
        Submitted = 1,
        
        [Description("Approved")]
        Approved = 2,
        
        [Description("Rejected")]
        Rejected = 3,
        
        [Description("Pending Review")]
        PendingReview = 4
    }
}
