namespace DynamicFilters
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class FilterOptionAttribute : Attribute
    {
        public FilterOptionType Option { get; init; } = FilterOptionType.Equality;

        public string? TargetName { get; init; } = null;

        public string? IgnoreFlagName { get; init; } = null;
    }
}
