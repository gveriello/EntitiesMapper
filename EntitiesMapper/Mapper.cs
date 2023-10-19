using EntitiesMapper.CustomAttribute;
using System.Collections.ObjectModel;
using System.Reflection;

namespace EntitiesMapper
{
    public class Mapper
    {
        public static void CopyObject<TSource, TDestination>(TSource source, out TDestination destination)
            where TDestination : class, new()
        {
            if (source is null)
            {
                destination = null;
                return;
            }

            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            destination = new TDestination();

            MapperEntities.LoadEntity(sourceType);
            MapperEntities.LoadEntity(destinationType);

            if (MapperEntities.EntityHasProperties(sourceType) && MapperEntities.EntityHasProperties(destinationType))
            {
                var destinationProperties = MapperEntities.GetPropertiesByType(destinationType);
                var sourceProperties = MapperEntities.GetPropertiesByType(sourceType);

                var copyValues = new Dictionary<string, string>();
                foreach (var destinationProperty in destinationProperties)
                {
                    var sourcePropertyName = string.Empty;
                    object? sourceValue, destinationValue = null;

                    //Properties are equals in both objects
                    if (sourceProperties.Keys.Contains(destinationProperty.Key))
                    {
                        sourcePropertyName = destinationProperty.Key;
                    }
                    else
                    {
                        //Properties are different but exists MapToAttribute to Map relationship
                        var destinationPropertyMapToAttribute = destinationProperty.Value.GetCustomAttributes<MapToAttribute>().FirstOrDefault(attribute => attribute.SourceToMap == sourceType && !attribute.IgnoreMap);
                        if (destinationPropertyMapToAttribute is not null)
                        {
                            sourcePropertyName = destinationPropertyMapToAttribute.Property;
                        }
                    }

                    if (string.IsNullOrEmpty(sourcePropertyName))
                    {
                        //Properties is not relationships
                        continue;
                    }

                    sourceProperties.TryGetValue(sourcePropertyName, out var sourceProperty);
                    if (!sourceProperty?.CanRead ?? true)
                        continue;

                    sourceValue = sourceProperty.GetValue(source);

                    if (!destinationProperty.Value?.CanWrite ?? true)
                        continue;

                    destinationProperty.Value.SetValue(destination, sourceValue);
                }
            }
        }

        public static void CopyList<TSource, TDestination>(ICollection<TSource> source, out ICollection<TDestination> destination)
            where TDestination : class, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source list is null.");
            }

            destination = new Collection<TDestination>();

            foreach (var sourceValue in source)
            {
                CopyObject(sourceValue, out TDestination toAdd);
                destination.Add(toAdd);
            }
        }
    }
}