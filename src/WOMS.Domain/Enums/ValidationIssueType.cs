using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum ValidationIssueType
    {
        [Description("Error")]
        Error = 0,
        
        [Description("Data Validation")]
        DataValidation = 1,
        
        [Description("Business Rule")]
        BusinessRule = 2,
        
        [Description("System Error")]
        SystemError = 3,
        
        [Description("User Input")]
        UserInput = 4,
        
        [Description("External Service")]
        ExternalService = 5
    }
}
