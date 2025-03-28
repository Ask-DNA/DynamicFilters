namespace DynamicFilters
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AllowIgnoreFlagAutoMappingAttribute : Attribute
    {
        public bool Allow { get; init; } = true;
    }
}
