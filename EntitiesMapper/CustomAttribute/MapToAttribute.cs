namespace EntitiesMapper.CustomAttribute
{
    // Base class per entrambi gli attributi
    public abstract class MapToAttributeBase : Attribute
    {
        public abstract Type SourceToMap { get; }
        public string Property { get; protected set; }
        public bool IgnoreMap { get; protected set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MapToAttribute<TSource> : MapToAttributeBase
    {
        public override Type SourceToMap => typeof(TSource);

        public MapToAttribute(string property, bool ignoreMap = false)
        {
            Property = property;
            IgnoreMap = ignoreMap;
        }
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MapToAttribute : MapToAttributeBase
    {
        public override Type SourceToMap { get; }

        public MapToAttribute(Type sourceToMap, string property, bool ignoreMap = false)
        {
            SourceToMap = sourceToMap;
            Property = property;
            IgnoreMap = ignoreMap;
        }

        public MapToAttribute(string property, bool ignoreMap = false)
        {
            Property = property;
            IgnoreMap = ignoreMap;
        }
    }
}