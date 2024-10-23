namespace DynamicFilters.Tests.DataLoaders
{
    public class InvalidInnerFilterConfigurationDataLoader : TheoryData<IDynamicFilter<InvalidInnerFilterConfigurationDataLoader.Entity>>
    {
        public InvalidInnerFilterConfigurationDataLoader()
        {
            Add(!new InvalidFilter());
            Add(new ValidFilter() | new InvalidFilter());
            Add(new ValidFilter() & new InvalidFilter());
            Add(new ValidFilter() | new ValidFilter() & new InvalidFilter());
        }

        public class Entity
        {
            public int Integer = 0;
        }

        private class ValidFilter : DynamicFilterBase<Entity>
        {
            [FilterOption(Option = FilterOptionType.Equality, TargetName = nameof(Entity.Integer))]
            public int Integer = 0;
        }

        private class InvalidFilter : DynamicFilterBase<Entity>
        {
            [FilterOption(Option = FilterOptionType.Equality, TargetName = "InvalidTargetName")]
            public int Integer = 0;
        }
    }
}
