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
            IEnumerable<T> result1 = subjectToFiltering.Where(filter);
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
            IEnumerable<T> result1 = subjectToFiltering.Where(filter);
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
            IEnumerable<T> result1 = subjectToFiltering.Where(filter);
            IEnumerable<T> result2 = subjectToFiltering.Where(analogousPredicate);

            Assert.Equal(result1, result2);
        }

        [Theory]
        [ClassData(typeof(InvalidTargetMappingDataLoader))]
        public void TargetMappingBehavior_ThrowsFilterConfigurationException<T>(DynamicFilterBase<T> filter)
        {
            Func<T, bool> tmp;

            Assert.Throws<InvalidFilterConfigurationException>(() => tmp = filter);
        }
    }
}