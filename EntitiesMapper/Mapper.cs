using EntitiesMapper.CustomAttribute;
using System.Collections.ObjectModel;
using System.Reflection;

namespace EntitiesMapper
{
    public class Mapper
    {
        public static void CopyObject<TSource, TDestination>(TSource source, out TDestination destination)
            where TDestination : new()
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            destination = new();

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

        public static void CopyList<TSource, TDestination>(ICollection<TSource> source, out ICollection<TDestination> destination)
            where TDestination : new()
        {
            if (source is null) throw new ArgumentNullException("Source list is null.");
            destination = new Collection<TDestination>();

            foreach (var sourceValue in source)
            {
                CopyObject(sourceValue, out TDestination toAdd);
                destination.Add(toAdd);
            }
        }
    }
}