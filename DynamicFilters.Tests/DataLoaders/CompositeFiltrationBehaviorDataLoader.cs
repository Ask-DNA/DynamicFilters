namespace DynamicFilters.Tests.DataLoaders
{
    public class CompositeFiltrationBehaviorDataLoader : TheoryData<
            List<CompositeFiltrationBehaviorDataLoader.Entity>,
            IDynamicFilter<CompositeFiltrationBehaviorDataLoader.Entity>,
            Func<CompositeFiltrationBehaviorDataLoader.Entity, bool>>
    {
        private static readonly List<Entity> _entities = [
            new Entity() { IntegerField = 1, StringField = "3" },
            new Entity() { IntegerField = 2, StringField = "1" },
            new Entity() { IntegerField = 3, StringField = "2" },
            new Entity() { IntegerField = 1, StringField = "3" },
            new Entity() { IntegerField = 2, StringField = "1" },
            new Entity() { IntegerField = 3, StringField = "2" },
            new Entity() { IntegerField = 1, StringField = "3" },
            new Entity() { IntegerField = 2, StringField = "1" },
            new Entity() { IntegerField = 3, StringField = "2" },
            new Entity() { IntegerField = 1, StringField = "3" }];

        public CompositeFiltrationBehaviorDataLoader()
        {
            IntegerRangeFilter integerRange_1_3 = new()
            {
                IntegerFieldLeftExclusiveBorder = 1,
                IntegerFieldRightExclusiveBorder = 3,
            };
            IntegerRangeFilter integerRange_0_3 = new()
            {
                IntegerFieldLeftExclusiveBorder = 0,
                IntegerFieldRightExclusiveBorder = 3,
            };
            StringFilter stringIs1 = new() { StringField = "1" };
            StringFilter stringIs3 = new() { StringField = "3" };

            Add(
                _entities,
                !integerRange_0_3,
                o => !(o.IntegerField > 0 && o.IntegerField < 3));
            Add(
                _entities,
                integerRange_1_3 | stringIs1,
                o => (o.IntegerField > 1 && o.IntegerField < 3) || o.StringField == "1");
            Add(
                _entities,
                (integerRange_1_3 | stringIs1) & !stringIs3,
                o => ((o.IntegerField > 1 && o.IntegerField < 3) || o.StringField == "1") && o.StringField != "3");
        }

        public class Entity
        {
            public int IntegerField = 0;
            public string StringField = "";
        }

        public class IntegerRangeFilter : DynamicFilterBase<Entity>
        {
            [FilterOption(Option = FilterOptionType.GreaterThan, TargetName = nameof(Entity.IntegerField))]
            public int IntegerFieldLeftExclusiveBorder = 0;

            [FilterOption(Option = FilterOptionType.LessThan, TargetName = nameof(Entity.IntegerField))]
            public int IntegerFieldRightExclusiveBorder = 0;
        }

        public class StringFilter : DynamicFilterBase<Entity>
        {
            [FilterOption(Option = FilterOptionType.Equality, TargetName = nameof(Entity.StringField))]
            public string StringField = "";
        }
    }
}
