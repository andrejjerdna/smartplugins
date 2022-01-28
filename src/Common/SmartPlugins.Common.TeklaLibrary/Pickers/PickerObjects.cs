using SmartPlugins.Common.Abstractions.ModelObjects;
using SmartPlugins.Common.Abstractions.Pickers;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.TeklaLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Pickers
{
    public class PickerObjects : IPickerObjects
    {
        private readonly ISmartModel _smartModel;

        public PickerObjects(ISmartModel smartModel)
        {
            _smartModel = smartModel;
        }

        public IEnumerable<IAssembly> GetSelectedObjectsAssembly(bool getAllObjects)
        {
            return GetSelectedObjects<Assembly>(getAllObjects).Select(a => new SmartAssembly(a));
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
