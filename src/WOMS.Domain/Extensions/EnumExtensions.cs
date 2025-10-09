using System.ComponentModel;
using System.Reflection;

namespace WOMS.Domain.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description attribute value for an enum value
        /// </summary>
        /// <param name="enumValue">The enum value</param>
        /// <returns>The description attribute value, or the enum name if no description is found</returns>
        public static string GetDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? enumValue.ToString();
        }

        /// <summary>
        /// Gets all enum values with their descriptions
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <returns>A dictionary of enum values and their descriptions</returns>
        public static Dictionary<T, string> GetEnumDescriptions<T>() where T : struct, Enum
        {
            return Enum.GetValues<T>().ToDictionary(
                value => value,
                value => value.GetDescription()
            );
        }

        /// <summary>
        /// Gets all enum descriptions as a list of strings
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <returns>A list of description strings</returns>
        public static List<string> GetEnumDescriptionList<T>() where T : struct, Enum
        {
            return Enum.GetValues<T>().Select(value => value.GetDescription()).ToList();
        }
    }
}
