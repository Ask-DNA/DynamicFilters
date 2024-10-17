using System.Linq.Expressions;

namespace DynamicFilters
{
    public abstract class DynamicFilterBase<T>
    {
        private FilterOptionsManager<T>? _optionsManager;

        private FilterExpressionBuilder<T>? _expressionBuilder;

        private FilterOptionsManager<T> OptionsManager
        {
            get
            {
                _optionsManager ??= new(this);
                return _optionsManager;
            }
        }

        private FilterExpressionBuilder<T> ExpressionBuilder
        {
            get
            {
                _expressionBuilder ??= new();
                return _expressionBuilder;
            }
        }

        public bool Valid { get => OptionsManager.Valid; }

        private Expression<Func<T, bool>> Build()
        {
            if (!Valid)
                throw new InvalidFilterConfigurationException(OptionsManager.Errors);

            ExpressionBuilder.Refresh();

            foreach (FilterOption option in OptionsManager.FilterOptions)
                ExpressionBuilder.AddFilterOption(option);

            return ExpressionBuilder.Create();
        }

        public static implicit operator Expression<Func<T, bool>>(DynamicFilterBase<T> filter)
        {
            return filter.Build();
        }

        public static implicit operator Func<T, bool>(DynamicFilterBase<T> filter)
        {
            return filter.Build().Compile();
        }
    }
}
