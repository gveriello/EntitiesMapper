using EntitiesMapper.CustomAttribute;
using System.Reflection;

namespace EntitiesMapper
{
    public class Mapper
    {
        public static void CopyTo<TSource, TDestination>(TSource source, TDestination destination)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            MapperEntities.LoadEntity(sourceType);
            MapperEntities.LoadEntity(destinationType);

            if (MapperEntities.EntityHasProperties(sourceType) && MapperEntities.EntityHasProperties(destinationType))
            {
                var destinationProperties = MapperEntities.GetPropertiesByType(destinationType);
                foreach (var kvp in MapperEntities.GetPropertiesByType(sourceType))
                {
                    var destinationSetters = destinationProperties?.Where(prop =>
                                prop.CanWrite &&
                                prop.Name == kvp.Name && prop.PropertyType == kvp.PropertyType ||
                                prop.GetCustomAttributes<MapToAttribute>()?.FirstOrDefault(attribute => attribute.Property == kvp.Name &&
                                                                                                (attribute.SourceToMap != null &&
                                                                                                attribute.SourceToMap == sourceType) &&
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