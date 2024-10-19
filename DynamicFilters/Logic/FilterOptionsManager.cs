using System.Reflection;

namespace DynamicFilters
{
    internal class FilterOptionsManager<T>
    {
        private readonly List<FilterOption> _filterOptions = [];

        private readonly List<InvalidFilterOptionConfigurationException> _errors = [];

        public IReadOnlyList<FilterOption> FilterOptions { get => _filterOptions; }

        public IReadOnlyList<InvalidFilterOptionConfigurationException> Errors { get => _errors; }

        public bool Valid { get => _errors.Count == 0; }

        public FilterOptionsManager(DynamicFilterBase<T> filter)
        {
            FilterOptionBuilder<T> builder = new(filter);

            foreach (PropertyInfo property in filter.GetType().GetProperties())
            {
                if (builder.TryCreate(property, out FilterOption? option, out InvalidFilterOptionConfigurationException? exception))
                    _filterOptions.Add(option!);
                else if (exception is not null)
                    _errors.Add(exception);
            }

            foreach (FieldInfo field in filter.GetType().GetFields())
            {
                if (builder.TryCreate(field, out FilterOption? option, out InvalidFilterOptionConfigurationException? exception))
                    _filterOptions.Add(option!);
                else if (exception is not null)
                    _errors.Add(exception);
            }
        }
    }
}
