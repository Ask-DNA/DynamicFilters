using System.Reflection;

namespace DynamicFilters
{
    internal class FilterOptionBuilder<T>(DynamicFilterBase<T> source)
    {
        private readonly DynamicFilterBase<T> _source = source;

        public bool TryCreate(PropertyOrFieldInfo optionMember, out FilterOption? option, out InvalidOperationException? exception)
        {
            option = null;
            exception = null;

            FilterOptionAttribute? attr = optionMember.Wrappee.GetCustomAttribute<FilterOptionAttribute>();
            if (attr is null)
                return false;

            PropertyOrFieldInfo? targetMember = PropertyOrFieldInfo.GetOrDefault(typeof(T), attr.TargetName, true);
            if (targetMember is null)
            {
                exception = new PropertyOrFieldNotFoundException(typeof(T), attr.TargetName);
                return false;
            }

            if (targetMember.PropertyOrFieldType != optionMember.PropertyOrFieldType)
            {
                exception = new FilterOptionTypeMismatchException(targetMember, optionMember);
                return false;
            }

            option = new(optionMember, attr.Option, attr.TargetName, _source);
            return true;
        }
    }
}
