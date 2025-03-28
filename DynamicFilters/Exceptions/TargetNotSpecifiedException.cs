using System.Reflection;

namespace DynamicFilters
{
    public class TargetNotSpecifiedException(MemberInfo filterOption)
        : InvalidFilterOptionConfigurationException(
            filterOption,
            $"Target field or property name is not specified")
    { }
}
