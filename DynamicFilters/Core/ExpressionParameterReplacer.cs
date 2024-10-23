using System.Linq.Expressions;

namespace DynamicFilters
{
    internal class ExpressionParameterReplacer(ParameterExpression from, ParameterExpression to) : ExpressionVisitor
    {
        private readonly ParameterExpression _from = from, _to = to;

        public override Expression? Visit(Expression? node)
        {
            if (node is not null && node == _from)
                return _to;
            return base.Visit(node);
        }
    }
}
