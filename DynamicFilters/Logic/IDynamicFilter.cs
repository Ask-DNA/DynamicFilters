using System.Linq.Expressions;

namespace DynamicFilters
{
    public interface IDynamicFilter<T>
    {
        bool Valid { get; }

        Expression<Func<T, bool>> AsExpression();

        Func<T, bool> AsDelegate()
        {
            return AsExpression().Compile();
        }

        static IDynamicFilter<T> operator !(IDynamicFilter<T> filter)
        {
            return new NotFilterSpec<T>(filter);
        }

        static IDynamicFilter<T> operator &(IDynamicFilter<T> one, IDynamicFilter<T> other)
        {
            return new AndFilterSpec<T>(one, other);
        }

        static IDynamicFilter<T> operator |(IDynamicFilter<T> one, IDynamicFilter<T> other)
        {
            return new OrFilterSpec<T>(one, other);
        }
    }
}
