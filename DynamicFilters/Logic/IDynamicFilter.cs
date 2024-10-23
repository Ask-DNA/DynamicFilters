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
    }
}
