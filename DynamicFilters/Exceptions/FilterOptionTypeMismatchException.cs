using System.Reflection;

namespace DynamicFilters
{
    public class FilterOptionTypeMismatchException(MemberInfo targetMember, MemberInfo optionMember)
        : InvalidOperationException($"Type mismatch between target member '{targetMember}' and filter option '{optionMember}'")
    { }
}
