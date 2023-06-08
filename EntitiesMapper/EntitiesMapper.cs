using EntitiesMapper.CustomAttribute;
using System.Reflection;

namespace EntitiesMapper
{
    public class Mapper
    {
        public static void CopyTo<TSource, TDestination>(TSource source, TDestination destination)
        {
            MapperEntities.LoadEntity(typeof(TSource));
            MapperEntities.LoadEntity(typeof(TDestination));

            if (MapperEntities.EntityHasProperties(typeof(TSource)) && MapperEntities.EntityHasProperties(typeof(TDestination)))
            {
                var destinationProperties = MapperEntities.GetPropertiesByType(typeof(TDestination));
                foreach (var kvp in MapperEntities.GetPropertiesByType(typeof(TSource)))
                {
                    var destinationSetters = destinationProperties?.Where(prop =>
                                prop.Name == kvp.Name && prop.PropertyType == kvp.PropertyType ||
                                prop.GetCustomAttributes<MapToAttribute>()?.FirstOrDefault(attribute => attribute.Property == kvp.Name &&
                                                                                                (attribute.SourceToMap != null &&
                                                                                                attribute.SourceToMap == typeof(TSource)) &&
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