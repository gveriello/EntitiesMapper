using System.Reflection;

namespace EntitiesMapper.EntitiesWorkers
{
    public static class EntitiesData
    {
        private static Dictionary<Type, PropertyInfo[]>? Entities;

        public static void LoadEntity(Type entity)
        {
            Entities ??= new Dictionary<Type, PropertyInfo[]>();

            if (Entities.Keys.Contains(entity))
                return;

            var properties = entity.GetProperties();

            Entities.Add(entity, properties);
        }

        private static bool EntityIsLoadedOrThrowException(Type type)
            => Entities?.Keys?.Contains(type) ?? throw new ArgumentException($"{type.Name} is not loaded. Please call before {nameof(LoadEntity)} method.");

        internal static PropertyInfo[] GetPropertiesByType(Type type)
        {
            EntityIsLoadedOrThrowException(type);

            Entities.TryGetValue(type, out var properties);
            return properties;
        }

        internal static bool EntityHasProperties(Type type)
        {
            EntityIsLoadedOrThrowException(type);

            Entities.TryGetValue(type, out var properties);
            return properties?.Any() ?? false;
        }
    }
}
