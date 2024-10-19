using System.Reflection;

namespace DynamicFilters
{
    public class TypeMismatchException(MemberInfo targetMember, MemberInfo filterOption)
        : InvalidFilterOptionConfigurationException(filterOption, $"Type mismatch between target member '{targetMember}' and filter option")
    { }
}
