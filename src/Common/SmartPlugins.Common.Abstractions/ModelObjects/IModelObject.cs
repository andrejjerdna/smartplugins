namespace SmartPlugins.Common.Abstractions.ModelObjects
{
    public interface IModelObject
    {
        T GetProperty<T>(string propertyName);

        void Modify();

        T GetOriginObject<T>();

        void SetProperty(string propertyName, string value);

        void SetProperty(string propertyName, int value);

        void SetProperty(string propertyName, double value);
    }
}
