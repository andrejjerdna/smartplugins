namespace TsepBuilder.Models
{
    public struct PathVariable
    {
        public string Id { get; }
        public string Value { get; }

        public PathVariable(string id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
