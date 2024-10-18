namespace DynamicFilters
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class FilterOptionAttribute : Attribute
    {
        internal FilterOptionType Option { get; init; }

        internal string? TargetName { get; init; } = null;

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
