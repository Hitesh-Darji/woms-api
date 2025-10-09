using WOMS.Domain.Enums;
using WOMS.Domain.Extensions;

namespace WOMS.Domain.Examples
{
    public class EnumDescriptionExample
    {
        public static void DemonstrateEnumDescriptions()
        {
            // Example 1: Get description for a single enum value
            var userRole = UserRole.Admin;
            var roleDescription = userRole.GetDescription(); // Returns "Administrator"
            
            // Example 2: Get all descriptions for an enum type
            var allRoleDescriptions = EnumExtensions.GetEnumDescriptionList<UserRole>();
            // Returns: ["Technician", "Administrator", "Manager", "Supervisor"]
            
            // Example 3: Get enum values with their descriptions as a dictionary
            var roleDictionary = EnumExtensions.GetEnumDescriptions<UserRole>();
            // Returns: { Technician: "Technician", Admin: "Administrator", Manager: "Manager", Supervisor: "Supervisor" }
            
            // Example 4: Using with different enum types
            var assetStatus = AssetStatus.InUse;
            var statusDescription = assetStatus.GetDescription(); // Returns "In Use"
            
            var workOrderPriority = WorkOrderPriority.Critical;
            var priorityDescription = workOrderPriority.GetDescription(); // Returns "Critical"
        }
    }
}
