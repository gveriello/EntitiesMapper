using EntitiesMapper.CustomAttribute;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Reflection;

namespace EntitiesMapper
{
    public class Mapper
    {
        private static readonly ConcurrentDictionary<(Type source, Type dest), Dictionary<PropertyInfo, PropertyInfo>> _propertyMappingCache = new();

        public static void CopyObject<TSource, TDestination>(TSource source, out TDestination? destination)
            where TDestination : class, new()
        {
            if (source is null)
            {
                destination = null;
                return;
            }

            destination = new TDestination();
            CopyObjectInternal(source, destination);
        }

        public static void CopyObject<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
        {
            if (source is null || destination is null)
                return;

            CopyObjectInternal(source, destination);
        }

        public static TDestination? CopyObject<TSource, TDestination>(TSource source)
            where TDestination : class, new()
        {
            CopyObject(source, out TDestination? toReturn);
            return toReturn;
        }

        private static void CopyObjectInternal<TSource, TDestination>(TSource source, TDestination destination)
            where TDestination : class
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            var propertyMap = GetPropertyMap(sourceType, destinationType);
            foreach (var (destProp, sourceProp) in propertyMap)
            {
                try
                {
                    var value = sourceProp.GetValue(source);
                    destProp.SetValue(destination, value);
                }
                catch (Exception ex) when (ex is ArgumentException || ex is TargetException)
                {
#if DEBUG
                    throw;
#else
                    Console.WriteLine($"EntitiesMapper: si è verificato un errore durante la copia della proprietà {sourceType.Name}:{sourceProp.Name} in {destinationType.Name}:{destProp.Name}. L'errore è il seguente: {ex.Message}.");
#endif
                }
            }
        }

        public static void CopyList<TSource, TDestination>(ICollection<TSource> source, out ICollection<TDestination> destination)
            where TDestination : class, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), "Source list is null.");
            }

            destination = [];
            foreach (var sourceValue in source)
            {
                CopyObject(sourceValue, out TDestination? toAdd);
                destination.Add(toAdd);
            }
        }

        public static void CopyList<TSource, TDestination>(ICollection<TSource> source, ICollection<TDestination> destination)
            where TDestination : class, new()
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source), "Source list is null.");
            if (destination is null)
                throw new ArgumentNullException(nameof(destination), "Destination list is null.");

            foreach (var sourceValue in source)
            {
                CopyObject(sourceValue, out TDestination? toAdd);
                destination.Add(toAdd);
            }
        }

        public static ICollection<TDestination> CopyList<TSource, TDestination>(ICollection<TSource> source)
            where TDestination : class, new()
        {
            CopyList(source, out ICollection<TDestination> toReturn);
            return toReturn;
        }

        public static async Task CopyListAsync<TSource, TDestination>(
            ICollection<TSource> source,
            ICollection<TDestination> destination,
            int batchSize = 100,
            CancellationToken cancellationToken = default)
            where TDestination : class, new()
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);

            var batches = source.Chunk(batchSize);

            foreach (var batch in batches)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var tasks = batch.Select(item => Task.Run(() =>
                {
                    CopyObject(item, out TDestination? dest);
                    return dest;
                }, cancellationToken));

                var batchResults = await Task.WhenAll(tasks);
                foreach (var result in batchResults)
                {
                    destination.Add(result);
                }
            }
        }

        private static Dictionary<PropertyInfo, PropertyInfo> GetPropertyMap(Type sourceType, Type destinationType)
        {
            return _propertyMappingCache.GetOrAdd((sourceType, destinationType), key =>
            {
                MapperEntities.LoadEntity(sourceType);
                MapperEntities.LoadEntity(destinationType);

                var map = new Dictionary<PropertyInfo, PropertyInfo>();
                if (!MapperEntities.EntityHasProperties(sourceType) || !MapperEntities.EntityHasProperties(destinationType))
                    return map;

                var destinationProperties = MapperEntities.GetPropertiesByType(destinationType);
                var sourceProperties = MapperEntities.GetPropertiesByType(sourceType);

                foreach (var destProp in destinationProperties)
                {
                    if (!destProp.Value.CanWrite)
                        continue;

                    // Cerca prima per nome identico
                    if (sourceProperties.TryGetValue(destProp.Key, out var sourceProp) && sourceProp.CanRead)
                    {
                        map[destProp.Value] = sourceProp;
                        continue;
                    }

                    // Cerca tramite attributo
                    var mapToAttribute = GetMapToAttribute(destProp.Value, sourceType);
                    if (mapToAttribute != null &&
                        sourceProperties.TryGetValue(mapToAttribute.Property, out var mappedSourceProp) &&
                        mappedSourceProp.CanRead)
                    {
                        map[destProp.Value] = mappedSourceProp;
                    }
                }

                return map;
            });
        }

        private static MapToAttributeBase? GetMapToAttribute(PropertyInfo propertyInfo, Type sourceType)
        {
            return propertyInfo.GetCustomAttributes(false)
                .Select(attr => attr as MapToAttributeBase)
                .Where(attr => attr != null &&
                             (!attr.GetType().IsGenericType || attr.GetType().GetGenericArguments()[0] == sourceType) &&
                             attr.SourceToMap == sourceType &&
                             !attr.IgnoreMap)
                .FirstOrDefault();
        }
    }
}