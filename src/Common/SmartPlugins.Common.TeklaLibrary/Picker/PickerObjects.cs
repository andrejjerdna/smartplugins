using SmartPlugins.Common.Abstractions.Picker;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.TeklaLibrary.Picker
{
    public class PickerObjects : IPickerObjects
    {
        private readonly ISmartModel _smartModel;

        public PickerObjects(ISmartModel smartModel)
        {
            _smartModel = smartModel;
        }

        public IEnumerable<T> GetSelectedObjects<T>(bool getAllObjects)
        {
            var selectedObjects = _smartModel.GetSelectedObjects<T>();

            if (selectedObjects.Any())
                return selectedObjects;

            if (getAllObjects)
                return _smartModel.GetAllObjects<T>(true);

            return Enumerable.Empty<T>();
        }
    }
}
