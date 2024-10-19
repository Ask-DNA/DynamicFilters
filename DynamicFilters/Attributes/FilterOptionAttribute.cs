namespace DynamicFilters
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class FilterOptionAttribute : Attribute
    {
        internal FilterOptionType Option { get; init; }

        internal string? TargetName { get; init; } = null;

        internal string? IgnoreFlagName { get; init; } = null;

        public FilterOptionAttribute(FilterOptionType option, string targetName, string ignoreFlagName)
        {
            Option = option;
            TargetName = targetName;
            IgnoreFlagName = ignoreFlagName;
        }

        public FilterOptionAttribute(FilterOptionType option, string targetName)
        {
            Option = option;
            TargetName = targetName;
        }

        public FilterOptionAttribute(FilterOptionType option)
        {
            Option = option;
        }
    }
}
