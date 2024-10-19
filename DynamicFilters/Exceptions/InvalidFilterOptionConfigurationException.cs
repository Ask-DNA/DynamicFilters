using System.Reflection;

namespace DynamicFilters
{
    public abstract class InvalidFilterOptionConfigurationException(MemberInfo filterOption, string message)
        : InvalidOperationException($"Invalid filter option '{filterOption}' configuration: {message}")
    { }
}
