using System.Reflection;

namespace DynamicFilters
{
    public class IgnoreFlagNotFoundException(Type filterType, string ignoreFlagName, MemberInfo filterOption)
        : InvalidFilterOptionConfigurationException(
            filterOption,
            $"Entity '{filterType}' does not contain a boolean property or field with the given name '{ignoreFlagName}'")
    { }
}
