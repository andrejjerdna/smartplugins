using SmartPlugins.Common.TeklaLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Entities
{
    public abstract class SmartBaseObject
    {
        protected ModelObject ModelObject;

        public T GetOriginObject<T>()
        {
            return (T)(object)ModelObject;
        }

        public T GetProperty<T>(string propertyName)
        {
            if (typeof(T) == typeof(double))
                return (T)(object)ModelObject.SmartGetPropertyDouble(propertyName);

            if (typeof(T) == typeof(int))
                return (T)(object)ModelObject.SmartGetPropertyInt(propertyName);

            if (typeof(T) == typeof(string))
                return (T)(object)ModelObject.SmartGetPropertyString(propertyName);


            throw new NotImplementedException();
        }

        public void SetProperty(string propertyName, string value)
        {
            ModelObject.SetUserProperty(propertyName, value);
        }

        public void SetProperty(string propertyName, int value)
        {
            ModelObject.SetUserProperty(propertyName, value);
        }

        public void SetProperty(string propertyName, double value)
        {
            ModelObject.SetUserProperty(propertyName, value);
        }
    }
}
