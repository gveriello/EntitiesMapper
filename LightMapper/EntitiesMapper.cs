using EntitiesMapper.CustomAttribute;
using EntitiesMapper.EntitiesWorkers;
using System.Reflection;

namespace EntitiesMapper
{
    public class Mapper
    {
        public static void CopyTo<TSource, TDestination>(TSource source, TDestination destination)
        {
            EntitiesData.LoadEntity(typeof(TSource));
            EntitiesData.LoadEntity(typeof(TDestination));

            if (EntitiesData.EntityHasProperties(typeof(TSource)) && EntitiesData.EntityHasProperties(typeof(TDestination)))
            {
                var destinationProperties = EntitiesData.GetPropertiesByType(typeof(TDestination));
                foreach (var kvp in EntitiesData.GetPropertiesByType(typeof(TSource)))
                {
                    var destinationSetters = destinationProperties?.Where(prop =>
                                prop.Name == kvp.Name && prop.PropertyType == kvp.PropertyType ||
                                prop.GetCustomAttributes<MapToAttribute>()?.FirstOrDefault(attribute => attribute.Property == kvp.Name &&
                                                                                            attribute.SourceToMap == typeof(TSource) &&
                                                                                            !attribute.IgnoreMap)
                                                                                            is not null);

                    if (destinationSetters?.Any() ?? false)
                    {
                        var sourceValue = kvp.GetValue(source);
                        foreach (var destinationSetter in destinationSetters)
                            destinationSetter.SetValue(destination, sourceValue);
                    }
                }
            }
        }
    }
}