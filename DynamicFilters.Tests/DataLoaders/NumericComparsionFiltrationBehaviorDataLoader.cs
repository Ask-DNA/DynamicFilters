namespace DynamicFilters.Tests.DataLoaders
{
    internal class NumericComparsionFiltrationBehaviorDataLoader : TheoryData<
            List<NumericComparsionFiltrationBehaviorDataLoader.Entity>,
            DynamicFilterBase<NumericComparsionFiltrationBehaviorDataLoader.Entity>,
            Func<NumericComparsionFiltrationBehaviorDataLoader.Entity, bool>>
    {
        private static readonly List<Entity> _entities = [
            new Entity() { IntegerField = 1, DateField = new(2024, 10, 10) },
            new Entity() { IntegerField = 2, DateField = new(2024, 10, 11) },
            new Entity() { IntegerField = 3, DateField = new(2024, 10, 12) },
            new Entity() { IntegerField = 1, DateField = new(2024, 10, 13) },
            new Entity() { IntegerField = 2, DateField = new(2024, 10, 14) },
            new Entity() { IntegerField = 3, DateField = new(2024, 10, 15) },
            new Entity() { IntegerField = 1, DateField = new(2024, 10, 16) },
            new Entity() { IntegerField = 2, DateField = new(2024, 10, 17) },
            new Entity() { IntegerField = 3, DateField = new(2024, 10, 18) },
            new Entity() { IntegerField = 1, DateField = new(2024, 10, 19) }];

        public NumericComparsionFiltrationBehaviorDataLoader()
        {
            Add(
                _entities,
                new Filter() {
                    IntegerFieldLeftExclusiveBorder = 1,
                    IntegerFieldRightExclusiveBorder = 3,
                    DateFieldLeftInclusiveBorder = new(2024, 10, 14),
                    DateFieldRightInclusiveBorder = new(2024, 10, 17)
                },
                o => o.IntegerField > 1
                    && o.IntegerField < 3
                    && o.DateField >= new DateOnly(2024, 10, 14)
                    && o.DateField <= new DateOnly(2024, 10, 17));
            Add(
                _entities,
                new Filter()
                {
                    IntegerFieldLeftExclusiveBorder = 0,
                    IntegerFieldRightExclusiveBorder = 3,
                    DateFieldLeftInclusiveBorder = new(2024, 10, 14),
                    DateFieldRightInclusiveBorder = new(2024, 10, 17)
                },
                o => o.IntegerField > 0
                    && o.IntegerField < 3
                    && o.DateField >= new DateOnly(2024, 10, 14)
                    && o.DateField <= new DateOnly(2024, 10, 17));
            Add(
                _entities,
                new Filter()
                {
                    IntegerFieldLeftExclusiveBorder = 1,
                    IntegerFieldRightExclusiveBorder = 3,
                    DateFieldLeftInclusiveBorder = new(2024, 10, 14),
                    DateFieldRightInclusiveBorder = new(2024, 10, 14)
                },
                o => o.IntegerField > 1
                    && o.IntegerField < 3
                    && o.DateField >= new DateOnly(2024, 10, 14)
                    && o.DateField <= new DateOnly(2024, 10, 14));
        }

        public class Entity
        {
            public int IntegerField = 0;
            public DateOnly DateField = new();
        }

        public class Filter : DynamicFilterBase<Entity>
        {
            [FilterOption(Option = FilterOptionType.GreaterThan, TargetName = nameof(Entity.IntegerField))]
            public int IntegerFieldLeftExclusiveBorder = 0;

            [FilterOption(Option = FilterOptionType.LessThan, TargetName = nameof(Entity.IntegerField))]
            public int IntegerFieldRightExclusiveBorder = 0;

            [FilterOption(Option = FilterOptionType.GreaterThanOrEqual, TargetName = nameof(Entity.DateField))]
            public DateOnly DateFieldLeftInclusiveBorder = new();

            [FilterOption(Option = FilterOptionType.LessThanOrEqual, TargetName = nameof(Entity.DateField))]
            public DateOnly DateFieldRightInclusiveBorder = new();
        }
    }
}
