namespace DynamicFilters
{
    public class InvalidFilterConfigurationException(IEnumerable<InvalidOperationException> innerExceptions)
        : AggregateException("Invalid filter configuration", innerExceptions)
    { }
}