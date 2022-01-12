namespace TsepBuilder.Models
{
    public struct SourcePathVariable
    {
        public string Id { get; }
        public string Value { get; }

        public SourcePathVariable(string id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
