using System.Linq.Expressions;

namespace DynamicFilters
{
    internal abstract class CompositeFilterSpec<T> : IDynamicFilter<T>
    {
        public abstract bool Valid { get; }

        public abstract Expression<Func<T, bool>> AsExpression();
    }
}
