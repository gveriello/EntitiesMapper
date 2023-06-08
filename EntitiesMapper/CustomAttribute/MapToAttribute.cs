namespace EntitiesMapper.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MapToAttribute : Attribute
    {
        public Type SourceToMap { get; set; }
        public string Property { get; set; }
        public bool IgnoreMap { get; set; }
        public MapToAttribute(Type sourceToMap, string property, bool ignoreMap = false)
        {
            SourceToMap = sourceToMap;
            Property = property;
            IgnoreMap = ignoreMap;
        }
    }
}
