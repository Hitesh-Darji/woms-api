using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum IntegrationStatus
    {
        [Description("Available")]
        Available = 0,
        
        [Description("Connected")]
        Connected = 1,
        
        [Description("Coming Soon")]
        ComingSoon = 2,
        
        [Description("Error")]
        Error = 3
    }
}

