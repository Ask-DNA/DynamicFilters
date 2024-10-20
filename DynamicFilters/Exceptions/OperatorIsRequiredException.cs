using System.Reflection;

namespace DynamicFilters.Exceptions
{
    public class OperatorIsRequiredException(MemberInfo filterOption, FilterOptionType optionType) 
        : InvalidFilterOptionConfigurationException(filterOption, $"Filter option type '{optionType}' requires an operator realisation")
    { }
}
