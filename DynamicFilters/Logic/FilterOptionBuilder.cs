using System.Reflection;

namespace DynamicFilters
{
    internal class FilterOptionBuilder<T>(DynamicFilterBase<T> source)
    {
        private readonly DynamicFilterBase<T> _source = source;

        public bool TryCreate(PropertyOrFieldInfo optionMember, out FilterOption? option, out InvalidFilterOptionConfigurationException? exception)
        {
            option = null;
            exception = null;

            FilterOptionAttribute? attr = optionMember.Wrappee.GetCustomAttribute<FilterOptionAttribute>();
            if (attr is null)
                return false;

            string targetName = GetTargetName(optionMember, attr);

            PropertyOrFieldInfo? targetMember = PropertyOrFieldInfo.GetOrDefault(typeof(T), targetName, true);
            if (targetMember is null)
            {
                exception = new TargetNotFoundException(typeof(T), targetName, optionMember);
                return false;
            }

            if (targetMember.PropertyOrFieldType != optionMember.PropertyOrFieldType)
            {
                exception = new TypeMismatchException(targetMember, optionMember);
                return false;
            }

            if (!ProcessIgnoreFlag(attr.IgnoreFlagName, optionMember, out PropertyOrFieldInfo? ignoreFlagMember, out IgnoreFlagNotFoundException? ex))
            {
                exception = ex;
                return false;
            }

            option = new(optionMember, ignoreFlagMember, attr.Option, targetName, _source);
            return true;
        }

        private static string GetTargetName(PropertyOrFieldInfo optionMember, FilterOptionAttribute attr)
        {
            string? targetName = attr.TargetName;
            targetName ??= optionMember.Wrappee.Name;
            return targetName;
        }

        private bool ProcessIgnoreFlag(
            string? name,
            PropertyOrFieldInfo optionMember,
            out PropertyOrFieldInfo? ignoreFlagMember,
            out IgnoreFlagNotFoundException? exception)
        {
            exception = null;

            if (name is not null)
            {
                ignoreFlagMember = PropertyOrFieldInfo.GetOrDefault(_source.GetType(), name);
                if (ignoreFlagMember is null || ignoreFlagMember.PropertyOrFieldType != typeof(bool))
                    exception = new IgnoreFlagNotFoundException(_source.GetType(), name, optionMember);
                return exception is null;
            }

            ignoreFlagMember = PropertyOrFieldInfo.GetOrDefault(_source.GetType(), "Ignore" + optionMember.Wrappee.Name);

            return true;
        }
    }
}
