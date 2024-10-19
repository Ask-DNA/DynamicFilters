namespace DynamicFilters.Tests.DataLoaders
{
    public class IgnoreFlagsAutoMappingUsageDataLoader : TheoryData<
            List<IgnoreFlagsAutoMappingUsageDataLoader.Entity>,
            DynamicFilterBase<IgnoreFlagsAutoMappingUsageDataLoader.Entity>,
            Func<IgnoreFlagsAutoMappingUsageDataLoader.Entity, bool>>
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

        public IgnoreFlagsAutoMappingUsageDataLoader()
        {
            Add(
                _entities,
                new Filter() { IntegerField = 1, IgnoreIntegerField = false, StringField = "5", IgnoreStringField = false },
                o => o.IntegerField == 1 && o.StringField == "5");
            Add(
                _entities,
                new Filter() { IntegerField = 3, IgnoreIntegerField = false, StringField = "5", IgnoreStringField = true },
                o => o.IntegerField == 3);
            Add(
                _entities,
                new Filter() { IntegerField = 3, IgnoreIntegerField = true, StringField = "4", IgnoreStringField = false },
                o => o.StringField == "4");
            Add(
                _entities,
                new Filter() { IntegerField = 3, IgnoreIntegerField = true, StringField = "4", IgnoreStringField = true },
                o => true);
        }

        public class Entity
        {
            public int IntegerField = 0;
            public string StringField = "";
        }

        public class Filter : DynamicFilterBase<Entity>
        {
            [FilterOption(Option = FilterOptionType.Equality, TargetName = nameof(Entity.IntegerField))]
            public int IntegerField = 0;

            public bool IgnoreIntegerField = false;

            [FilterOption(Option = FilterOptionType.Equality, TargetName = nameof(Entity.StringField))]
            public string StringField = "";

            public bool IgnoreStringField = false;
        }
    }
}
