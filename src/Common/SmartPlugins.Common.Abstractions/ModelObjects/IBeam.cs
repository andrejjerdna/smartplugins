namespace SmartPlugins.Common.Abstractions.ModelObjects
{
    public interface IBeam : IPart
    {
        IPoint StartPoint { get; }
        IPoint EndPoint { get; }
    }
}
