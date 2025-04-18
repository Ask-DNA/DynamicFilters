using DynamicFilters.Tests.DataLoaders;

namespace DynamicFilters.Tests
{
    public class DynamicFiltersUnitTests
    {
        [Theory]
        [ClassData(typeof(EqualityFiltrationBehaviorDataLoader))]
        public void FiltrationBehavior_Equality_Success<T>(
            List<T> subjectToFiltering,
            DynamicFilterBase<T> filter,
            Func<T, bool> analogousPredicate)
        {
            IEnumerable<T> result1 = subjectToFiltering.Where(filter.AsDelegate());
            IEnumerable<T> result2 = subjectToFiltering.Where(analogousPredicate);

            Assert.Equal(result1, result2);
        }

        [Theory]
        [ClassData(typeof(InequalityFiltrationBehaviorDataLoader))]
        public void FiltrationBehavior_Inequality_Success<T>(
            List<T> subjectToFiltering,
            DynamicFilterBase<T> filter,
            Func<T, bool> analogousPredicate)
        {
            IEnumerable<T> result1 = subjectToFiltering.Where(filter.AsDelegate());
            IEnumerable<T> result2 = subjectToFiltering.Where(analogousPredicate);

            Assert.Equal(result1, result2);
        }

        [Theory]
        [ClassData(typeof(MultipleOptionFiltartionBehaviorDataLoader))]
        public void FiltrationBehavior_MultipleOption_Success<T>(
            List<T> subjectToFiltering,
            DynamicFilterBase<T> filter,
            Func<T, bool> analogousPredicate)
        {
            IEnumerable<T> result1 = subjectToFiltering.Where(filter.AsDelegate());
            IEnumerable<T> result2 = subjectToFiltering.Where(analogousPredicate);

            Assert.Equal(result1, result2);
        }

        [Theory]
        [ClassData(typeof(InvalidTargetMappingDataLoader))]
        public void TargetMappingBehavior_ThrowsFilterConfigurationException<T>(DynamicFilterBase<T> filter)
        {
            Func<T, bool> tmp;

            Assert.Throws<InvalidFilterConfigurationException>(() => tmp = filter.AsDelegate());
        }

        [Theory]
        [ClassData(typeof(InvalidTypeConfigurationDataLoader))]
        public void TypeValidationBehavior_ThrowsFilterConfigurationException<T>(DynamicFilterBase<T> filter)
        {
            Func<T, bool> tmp;

            Assert.Throws<InvalidFilterConfigurationException>(() => tmp = filter.AsDelegate());
        }

        [Theory]
        [ClassData(typeof(AutoTargetMappingDataLoader))]
        public void FiltrationBehavior_AutoTargetMapping_Success<T>(
            List<T> subjectToFiltering,
            DynamicFilterBase<T> filter,
            Func<T, bool> analogousPredicate)
        {
            IEnumerable<T> result1 = subjectToFiltering.Where(filter.AsDelegate());
            IEnumerable<T> result2 = subjectToFiltering.Where(analogousPredicate);

            Assert.Equal(result1, result2);
        }

        [Theory]
        [ClassData(typeof(IgnoreFlagsUsageDataLoader))]
        public void FiltrationBehavior_IgnoreFlagsUsage_Success<T>(
            List<T> subjectToFiltering,
            DynamicFilterBase<T> filter,
            Func<T, bool> analogousPredicate)
        {
            IEnumerable<T> result1 = subjectToFiltering.Where(filter.AsDelegate());
            IEnumerable<T> result2 = subjectToFiltering.Where(analogousPredicate);

            Assert.Equal(result1, result2);
        }

        [Theory]
        [ClassData(typeof(InvalidIgnoreFlagsMappingDataLoader))]
        public void IgnoreFlagMappingBehavior_ThrowsFilterConfigurationException<T>(DynamicFilterBase<T> filter)
        {
            Func<T, bool> tmp;

            Assert.Throws<InvalidFilterConfigurationException>(() => tmp = filter.AsDelegate());
        }

        [Theory]
        [ClassData(typeof(IgnoreFlagsAutoMappingUsageDataLoader))]
        public void FiltrationBehavior_IgnoreFlagsAutoMappingUsage_Success<T>(
            List<T> subjectToFiltering,
            DynamicFilterBase<T> filter,
            Func<T, bool> analogousPredicate)
        {
            IEnumerable<T> result1 = subjectToFiltering.Where(filter.AsDelegate());
            IEnumerable<T> result2 = subjectToFiltering.Where(analogousPredicate);

            Assert.Equal(result1, result2);
        }

        [Theory]
        [ClassData(typeof(DisabledIgnoreFlagAutoMappingDataLoader))]
        public void FiltrationBehavior_DisabledIgnoreFlagsAutoMappingUsage_Success<T>(
            List<T> subjectToFiltering,
            DynamicFilterBase<T> filter,
            Func<T, bool> analogousPredicate)
        {
            IEnumerable<T> result1 = subjectToFiltering.Where(filter.AsDelegate());
            IEnumerable<T> result2 = subjectToFiltering.Where(analogousPredicate);

            Assert.Equal(result1, result2);
        }

        [Theory]
        [ClassData(typeof(NumericComparsionFiltrationBehaviorDataLoader))]
        public void FiltrationBehavior_NumericComparsion_Success<T>(
            List<T> subjectToFiltering,
            DynamicFilterBase<T> filter,
            Func<T, bool> analogousPredicate)
        {
            IEnumerable<T> result1 = subjectToFiltering.Where(filter.AsDelegate());
            IEnumerable<T> result2 = subjectToFiltering.Where(analogousPredicate);

            Assert.Equal(result1, result2);
        }

        [Theory]
        [ClassData(typeof(CompositeFiltrationBehaviorDataLoader))]
        public void FiltrationBehavior_Composite_Success<T>(
            List<T> subjectToFiltering,
            IDynamicFilter<T> filter,
            Func<T, bool> analogousPredicate)
        {
            IEnumerable<T> result1 = subjectToFiltering.Where(filter.AsDelegate());
            IEnumerable<T> result2 = subjectToFiltering.Where(analogousPredicate);

            Assert.Equal(result1, result2);
        }

        [Theory]
        [ClassData(typeof(InvalidInnerFilterConfigurationDataLoader))]
        public void IgnoreFlagMappingBehavior_ThrowsInvalidInnerFilterConfigurationException<T>(IDynamicFilter<T> filter)
        {
            Func<T, bool> tmp;

            Assert.Throws<InvalidInnerFilterConfigurationException>(() => tmp = filter.AsDelegate());
        }

        [Theory]
        [ClassData(typeof(DisabledTargetAutoMappingDataLoader))]
        public void DisabledTargetMappingBehavior_ThrowsInvalidFilterConfigurationException<T>(IDynamicFilter<T> filter)
        {
            Func<T, bool> tmp;

            Assert.Throws<InvalidFilterConfigurationException>(() => tmp = filter.AsDelegate());
        }
    }
}