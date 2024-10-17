namespace DynamicFilters.Tests.DataLoaders
{
    internal class InvalidTargetMappingDataLoader : TheoryData<DynamicFilterBase<InvalidTargetMappingDataLoader.Entity>>
    {
        public InvalidTargetMappingDataLoader()
        {
            Add(new Filter1());
            Add(new Filter2());
            Add(new Filter3());
        }

        public class Entity
        {
            private int _privateIntegerField = 0;
            public int NonReadableIntegerProperty { private get; set; }
        }

        private class Filter1 : DynamicFilterBase<Entity>
        {
            [FilterOption(FilterOptionType.Equality, "_privateIntegerField")]
            public int Integer = 0;
        }

        private class Filter2 : DynamicFilterBase<Entity>
        {
            [FilterOption(FilterOptionType.Equality, nameof(Entity.NonReadableIntegerProperty))]
            public int Integer = 0;
        }

        private class Filter3 : DynamicFilterBase<Entity>
        {
            [FilterOption(FilterOptionType.Equality, "InvalidName")]
            public int Integer = 0;
        }
    }
}
