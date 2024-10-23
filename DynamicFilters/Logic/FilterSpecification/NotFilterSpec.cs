using System.Linq.Expressions;

namespace DynamicFilters
{
    internal class NotFilterSpec<T>(IDynamicFilter<T> wrapped) : CompositeFilterSpec<T>
    {
        private readonly IDynamicFilter<T> _wrapped = wrapped;

        public override bool Valid => _wrapped.Valid;

        public override Expression<Func<T, bool>> AsExpression()
        {
            if (!Valid)
                throw new InvalidInnerFilterConfigurationException();

            Expression<Func<T, bool>> originalLambda = _wrapped.AsExpression();
            Expression newBody = Expression.Not(originalLambda.Body);
            return Expression.Lambda<Func<T, bool>>(newBody, originalLambda.Parameters[0]);
        }
    }
}
