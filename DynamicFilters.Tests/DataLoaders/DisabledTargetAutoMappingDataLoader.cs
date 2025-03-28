namespace DynamicFilters.Tests.DataLoaders
{
    public class DisabledTargetAutoMappingDataLoader : TheoryData<IDynamicFilter<DisabledTargetAutoMappingDataLoader.Entity>>
    {
        public DisabledTargetAutoMappingDataLoader()
        {
            Add(new InvalidFilter() { Integer = 0 });
            Add(new InvalidFilter() { Integer = 1 });
        }

        public class Entity
        {
            public int Integer = 0;
        }

        [AllowTargetAutoMapping(Allow = false)]
        private class InvalidFilter : DynamicFilterBase<Entity>
        {
            [FilterOption(Option = FilterOptionType.Equality)]
            public int Integer = 0;
        }
    }
}
