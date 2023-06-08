using System.Linq.Expressions;

namespace EntitiesMapper.Contracts
{
    internal interface IEntitiesMappings<TSource, TDestination>
    {
        void RegisterMap<TSourceProperty, TDestinationProperty>(Expression<Func<TSource, TSourceProperty>> propertySourceExpression,
                                                                       Expression<Func<TDestination, TDestinationProperty>> propertyDestinationExpression);
    }
}
