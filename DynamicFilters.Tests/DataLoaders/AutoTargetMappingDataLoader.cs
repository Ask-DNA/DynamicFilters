namespace DynamicFilters.Tests.DataLoaders
{
    public class AutoTargetMappingDataLoader : TheoryData<
            List<AutoTargetMappingDataLoader.Entity>,
            DynamicFilterBase<AutoTargetMappingDataLoader.Entity>,
            Func<AutoTargetMappingDataLoader.Entity, bool>>
    {
        private static readonly List<Entity> _entities = [
            new Entity() { IntegerField = 1, StringField = "1" },
            new Entity() { IntegerField = 2, StringField = "2" },
            new Entity() { IntegerField = 3, StringField = "3" },
            new Entity() { IntegerField = 4, StringField = "4" },
            new Entity() { IntegerField = 5, StringField = "5" },
            new Entity() { IntegerField = 1, StringField = "5" },
            new Entity() { IntegerField = 2, StringField = "4" },
            new Entity() { IntegerField = 3, StringField = "3" },
            new Entity() { IntegerField = 4, StringField = "2" },
            new Entity() { IntegerField = 5, StringField = "1" }];

        public AutoTargetMappingDataLoader()
        {
            Add(
                _entities,
                new Filter() { IntegerField = 1, StringField = "5" },
                o => o.IntegerField == 1 && o.StringField != "5");
            Add(
                _entities,
                new Filter() { IntegerField = 3, StringField = "3" },
                o => o.IntegerField == 3 && o.StringField != "3");
            Add(
                _entities,
                new Filter() { IntegerField = 5, StringField = "2" },
                o => o.IntegerField == 5 && o.StringField != "2");
        }

        public class Entity
        {
            public int IntegerField = 0;
            public string StringField = "";
        }

        public class Filter : DynamicFilterBase<Entity>
        {
            [FilterOption(FilterOptionType.Equality)]
            public int IntegerField = 0;

            [FilterOption(FilterOptionType.Inequality)]
            public string StringField = "";
        }
    }
}
