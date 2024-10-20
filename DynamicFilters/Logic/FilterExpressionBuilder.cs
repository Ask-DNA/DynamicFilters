using System.Linq.Expressions;

namespace DynamicFilters
{
    internal class FilterExpressionBuilder<T>
    {
        private readonly List<BinaryExpression> _filterOptionExpressions = [];

        private ParameterExpression _parameter = Expression.Parameter(typeof(T));

        public void AddFilterOption(FilterOption option)
        {
            if (option.Ignore)
                return;

            MemberExpression left = Expression.PropertyOrField(_parameter, option.TargetName);
            ConstantExpression right = Expression.Constant(option.Value, option.ValueType);

            switch (option.OptionType)
            {
                case FilterOptionType.Equality:
                    _filterOptionExpressions.Add(Expression.Equal(left, right));
                    break;
                case FilterOptionType.Inequality:
                    _filterOptionExpressions.Add(Expression.NotEqual(left, right));
                    break;
                case FilterOptionType.LessThan:
                    _filterOptionExpressions.Add(Expression.LessThan(left, right));
                    break;
                case FilterOptionType.LessThanOrEqual:
                    _filterOptionExpressions.Add(Expression.LessThanOrEqual(left, right));
                    break;
                case FilterOptionType.GreaterThan:
                    _filterOptionExpressions.Add(Expression.GreaterThan(left, right));
                    break;
                case FilterOptionType.GreaterThanOrEqual:
                    _filterOptionExpressions.Add(Expression.GreaterThanOrEqual(left, right));
                    break;
            }
        }

        public Expression<Func<T, bool>> Create()
        {
            if (_filterOptionExpressions.Count == 0)
                return t => true;

            Expression aggregate = _filterOptionExpressions[0], tmp;
            for (int i = 0; i < _filterOptionExpressions.Count - 1; i++)
            {
                tmp = _filterOptionExpressions[i + 1];
                aggregate = Expression.AndAlso(aggregate, tmp);
            }
            return Expression.Lambda<Func<T, bool>>(aggregate, _parameter);
        }

        public void Refresh()
        {
            _filterOptionExpressions.Clear();
            _parameter = Expression.Parameter(typeof(T));
        }
    }
}
