﻿namespace DynamicFilters.Tests.DataLoaders
{
    internal class InvalidTypeConfigurationDataLoader : TheoryData<DynamicFilterBase<InvalidTypeConfigurationDataLoader.Entity>>
    {
        public InvalidTypeConfigurationDataLoader()
        {
            Add(new Filter1());
            Add(new Filter2());
            Add(new Filter3());
        }

        public class Entity
        {
            public int IntegerField = 0;
            public int? NullableIntegerField;
            public string StringField = "";
        }

        private class Filter1 : DynamicFilterBase<Entity>
        {
            [FilterOption(FilterOptionType.Equality, nameof(Entity.IntegerField))]
            public int? Integer = 0;
        }

        private class Filter2 : DynamicFilterBase<Entity>
        {
            [FilterOption(FilterOptionType.Equality, nameof(Entity.NullableIntegerField))]
            public int Integer = 0;
        }

        private class Filter3 : DynamicFilterBase<Entity>
        {
            [FilterOption(FilterOptionType.Equality, nameof(Entity.StringField))]
            public int Integer = 0;
        }
    }
}