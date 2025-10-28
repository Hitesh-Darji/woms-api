using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum IntegrationCategory
    {
        [Description("Communication")]
        Communication = 0,
        
        [Description("Project Management")]
        ProjectManagement = 1,
        
        [Description("Productivity")]
        Productivity = 2,
        
        [Description("Document Management")]
        DocumentManagement = 3,
        
        [Description("Security")]
        Security = 4
    }
}

