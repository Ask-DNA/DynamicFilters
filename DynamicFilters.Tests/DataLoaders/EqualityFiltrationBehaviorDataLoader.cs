namespace DynamicFilters.Tests.DataLoaders
{
    public class EqualityFiltrationBehaviorDataLoader : TheoryData<
            List<EqualityFiltrationBehaviorDataLoader.Entity>,
            DynamicFilterBase<EqualityFiltrationBehaviorDataLoader.Entity>,
            Func<EqualityFiltrationBehaviorDataLoader.Entity, bool>>
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

        public EqualityFiltrationBehaviorDataLoader()
        {
            Add(
                _entities,
                new Filter() { SomeInteger = 1, SomeString = "5" },
                o => o.IntegerField == 1 && o.StringField == "5");
            Add(
                _entities,
                new Filter() { SomeInteger = 3, SomeString = "3" },
                o => o.IntegerField == 3 && o.StringField == "3");
            Add(
                _entities,
                new Filter() { SomeInteger = 5, SomeString = "2" },
                o => o.IntegerField == 5 && o.StringField == "2");
        }

        public class Entity
        {
            public int IntegerField = 0;
            public string StringField = "";
        }

        public class Filter : DynamicFilterBase<Entity>
        {
            [FilterOption(FilterOptionType.Equality, nameof(Entity.IntegerField))]
            public int SomeInteger = 0;

            [FilterOption(FilterOptionType.Equality, nameof(Entity.StringField))]
            public string SomeString = "";
        }
    }
}
