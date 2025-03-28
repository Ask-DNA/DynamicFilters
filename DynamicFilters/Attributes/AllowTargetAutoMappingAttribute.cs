namespace DynamicFilters
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AllowTargetAutoMappingAttribute : Attribute
    {
        public bool Allow { get; init; } = true;
    }
}
