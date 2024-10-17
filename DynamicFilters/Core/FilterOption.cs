namespace DynamicFilters
{
    internal class FilterOption
    {
        private readonly object _source;

        private readonly PropertyOrFieldInfo _optionMember;

        public object? Value
        {
            get => _optionMember.GetValue(_source);
        }

        public FilterOptionType OptionType { get; init; }

        public string TargetName { get; init; }

        public Type ValueType { get; init; }

        public FilterOption(PropertyOrFieldInfo optionMember, FilterOptionType optionType, string targetName, object source)
        {
            _optionMember = optionMember;
            OptionType = optionType;
            TargetName = targetName;
            ValueType = optionMember.PropertyOrFieldType;
            _source = source;
        }
    }
}
