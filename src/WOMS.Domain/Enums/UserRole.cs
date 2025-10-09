using System.ComponentModel;

namespace WOMS.Domain.Enums
{
    public enum UserRole
    {
        [Description("Technician")]
        Technician = 0,
        
        [Description("Administrator")]
        Admin = 1,
        
        [Description("Manager")]
        Manager = 2,
        
        [Description("Supervisor")]
        Supervisor = 3
    }
}
