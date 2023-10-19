using System.Reflection;

namespace EntitiesMapper
{
    public static class MapperEntities
    {
        private static readonly Dictionary<Type, PropertyInfo[]>? EntitiesData = new();

        public static void LoadEntities(params Type[] entitiesToLoad)
        {
            entitiesToLoad?.ToList().ForEach(type => LoadEntity(type));
        }

        public static void LoadEntity(Type entityToLoad)
        {
            if (EntitiesData.Keys.Contains(entityToLoad))
                return;

            var properties = entityToLoad.GetProperties();

            EntitiesData.Add(entityToLoad, properties);
        }

        private static bool EntityIsLoadedOrThrowException(Type type)
            => EntitiesData.Keys?.Contains(type) ?? throw new ArgumentException($"{type.Name} is not loaded. Please call before {nameof(LoadEntity)} method.");

        internal static Dictionary<string, PropertyInfo> GetPropertiesByType(Type type)
        {
            EntityIsLoadedOrThrowException(type);

            EntitiesData.TryGetValue(type, out var properties);
            return properties.ToDictionary(prop => prop.Name, prop => prop);
        }

        internal static bool EntityHasProperties(Type type)
        {
            EntityIsLoadedOrThrowException(type);

            EntitiesData.TryGetValue(type, out var properties);
            return properties?.Any() ?? false;
        }
    }
}
