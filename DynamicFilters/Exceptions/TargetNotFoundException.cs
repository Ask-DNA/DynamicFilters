using System.Reflection;

namespace DynamicFilters
{
    public class TargetNotFoundException(Type targetEntityType, string propertyOrFieldName, MemberInfo filterOption)
        : InvalidFilterOptionConfigurationException(
            filterOption,
            $"Entity '{targetEntityType}' does not contain a readable property or field with the given name '{propertyOrFieldName}'")
    { }
}
