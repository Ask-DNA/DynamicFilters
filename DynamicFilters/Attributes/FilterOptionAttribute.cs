namespace DynamicFilters
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class FilterOptionAttribute(FilterOptionType option, string targetName) : Attribute
    {
        internal FilterOptionType Option { get; init; } = option;

        internal string TargetName { get; init; } = targetName;
    }
}
