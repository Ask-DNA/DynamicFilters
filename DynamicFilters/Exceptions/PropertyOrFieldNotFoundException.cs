namespace DynamicFilters
{
    public class PropertyOrFieldNotFoundException(Type targetEntityType, string propertyOrFieldName)
        : InvalidOperationException($"Entity '{targetEntityType}' does not contain a readable property or field with the given name '{propertyOrFieldName}'")
    { }
}
