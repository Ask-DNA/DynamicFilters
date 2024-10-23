using System.Linq.Expressions;

namespace DynamicFilters
{
    internal class AndFilterSpec<T>(IDynamicFilter<T> one, IDynamicFilter<T> other) : CompositeFilterSpec<T>
    {
        private readonly IDynamicFilter<T> _one = one;

        private readonly IDynamicFilter<T> _other = other;

        public override bool Valid => _one.Valid && _other.Valid;

        public override Expression<Func<T, bool>> AsExpression()
        {
            if (!Valid)
                throw new InvalidInnerFilterConfigurationException();

            Expression<Func<T, bool>> oneLambdaExpression = _one.AsExpression();
            Expression<Func<T, bool>> otherLambdaExpression = _other.AsExpression();

            ExpressionParameterReplacer replacer = new(otherLambdaExpression.Parameters[0], oneLambdaExpression.Parameters[0]);
            BinaryExpression newBodyExpression = Expression.AndAlso(oneLambdaExpression.Body, replacer.Visit(otherLambdaExpression.Body)!);

            return Expression.Lambda<Func<T, bool>>(newBodyExpression, oneLambdaExpression.Parameters[0]);
        }
    }
}
