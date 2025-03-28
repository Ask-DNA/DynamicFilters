namespace DynamicFilters.Tests.DataLoaders
{
    public class DisabledIgnoreFlagAutoMappingDataLoader : TheoryData<
            List<DisabledIgnoreFlagAutoMappingDataLoader.Entity>,
            DynamicFilterBase<DisabledIgnoreFlagAutoMappingDataLoader.Entity>,
            Func<DisabledIgnoreFlagAutoMappingDataLoader.Entity, bool>>
    {

        private static readonly List<Entity> _entities = [
            new Entity() { IntegerField = 1 },
            new Entity() { IntegerField = 2 },
            new Entity() { IntegerField = 3 },
            new Entity() { IntegerField = 4 },
            new Entity() { IntegerField = 5 }];

        public DisabledIgnoreFlagAutoMappingDataLoader()
        {
            Add(
                _entities,
                new Filter1() { IntegerField = 1, IgnoreIntegerField = false },
                o => o.IntegerField == 1);
            Add(
                _entities,
                new Filter1() { IntegerField = 3, IgnoreIntegerField = false },
                o => o.IntegerField == 3);
            Add(
                _entities,
                new Filter1() { IntegerField = 1, IgnoreIntegerField = true },
                o => true);
            Add(
                _entities,
                new Filter1() { IntegerField = 3, IgnoreIntegerField = true },
                o => true);
            Add(
                _entities,
                new Filter2() { IntegerField = 1, IgnoreIntegerField = false },
                o => o.IntegerField == 1);
            Add(
                _entities,
                new Filter2() { IntegerField = 3, IgnoreIntegerField = false },
                o => o.IntegerField == 3);
            Add(
                _entities,
                new Filter2() { IntegerField = 1, IgnoreIntegerField = true },
                o => o.IntegerField == 1);
            Add(
                _entities,
                new Filter2() { IntegerField = 3, IgnoreIntegerField = true },
                o => o.IntegerField == 3);
        }

        public class Entity
        {
            public int IntegerField = 0;
        }

        [AllowIgnoreFlagAutoMapping(Allow = true)]
        public class Filter1 : DynamicFilterBase<Entity>
        {
            public bool IgnoreIntegerField = false;

            [FilterOption(Option = FilterOptionType.Equality, TargetName = nameof(Entity.IntegerField))]
            public int IntegerField = 0;
        }

        [AllowIgnoreFlagAutoMapping(Allow = false)]
        public class Filter2 : DynamicFilterBase<Entity>
        {
            public bool IgnoreIntegerField = false;

            [FilterOption(Option = FilterOptionType.Equality, TargetName = nameof(Entity.IntegerField))]
            public int IntegerField = 0;
        }
    }
}
