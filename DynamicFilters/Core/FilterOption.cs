namespace DynamicFilters
{
    internal class FilterOption
    {
        private readonly object _source;

        private readonly PropertyOrFieldInfo _optionMember;

        private readonly PropertyOrFieldInfo? _ignoreFlagMember = null;

        public object? Value
        {
            get => _optionMember.GetValue(_source);
        }

        public FilterOptionType OptionType { get; init; }

        public string TargetName { get; init; }

        public Type ValueType { get; init; }

        public bool Ignore
        {
            get => _ignoreFlagMember is not null && (bool)_ignoreFlagMember.GetValue(_source)!;
        }

        public FilterOption(PropertyOrFieldInfo optionMember, PropertyOrFieldInfo? ignoreFlagMember, FilterOptionType optionType, string targetName, object source)
        {
            _optionMember = optionMember;
            _ignoreFlagMember = ignoreFlagMember;
            OptionType = optionType;
            TargetName = targetName;
            ValueType = optionMember.PropertyOrFieldType;
            _source = source;
        }
    }
}
