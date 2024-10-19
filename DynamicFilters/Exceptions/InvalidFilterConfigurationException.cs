namespace DynamicFilters
{
    public class InvalidFilterConfigurationException(IEnumerable<InvalidFilterOptionConfigurationException> innerExceptions)
        : AggregateException("Invalid filter configuration", innerExceptions)
    { }
}