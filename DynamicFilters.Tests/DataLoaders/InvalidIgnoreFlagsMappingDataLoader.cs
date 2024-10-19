namespace DynamicFilters.Tests.DataLoaders
{
    public class InvalidIgnoreFlagsMappingDataLoader : TheoryData<DynamicFilterBase<InvalidIgnoreFlagsMappingDataLoader.Entity>>
    {
        public InvalidIgnoreFlagsMappingDataLoader()
        {
            Add(new Filter1());
            Add(new Filter2());
        }

        public class Entity
        {
            public int Integer = 0;
        }

        private class Filter1 : DynamicFilterBase<Entity>
        {
            [FilterOption(FilterOptionType.Equality, nameof(Entity.Integer), "IgnoreInteger")]
            public int Integer = 0;
        }

        private class Filter2 : DynamicFilterBase<Entity>
        {
            [FilterOption(FilterOptionType.Equality, nameof(Entity.Integer), "IgnoreInteger")]
            public int Integer = 0;

            public int IgnoreInteger = 0;
        }
    }
}
