using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.TeklaLibrary.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary
{
    public class SmartModel : ISmartModel
    {
        public Model _teklaModel;

        public SmartModel()
        {
            _teklaModel = new Model();
            ConnectionStatus = TeklaModel.GetConnectionStatus();

            if (ConnectionStatus)
            {
                AttributesPath = Path.Combine(TeklaModel.GetInfo().ModelPath, "attributes");
                FilterPath = Path.Combine(AttributesPath);
                SmartPluginsPath = Path.Combine(AttributesPath, "SmartPlugins");
            }
        }

        public Model TeklaModel { get => _teklaModel; }
        public bool ConnectionStatus { get; }
        public string FilterPath { get; }
        public string AttributesPath { get; }
        public string SmartPluginsPath { get; }

        public T GetParentObject<T>() where T : class
        {
            return _teklaModel as T;
        }

        public IEnumerable<T> GetAllObjects<T>(bool autoFetch)
        {
            return _teklaModel.GetAllObjects<T>(autoFetch);
        }

        public ConcurrentBag<T> GetAllObjectsConcurrent<T>(bool autoFetch)
        {
            return new ConcurrentBag<T>(_teklaModel.GetAllObjects<T>(autoFetch));
        }

        public void ReConnect()
        {
            _teklaModel = new Model();
        }

        public bool? CommitChanges() => _teklaModel?.CommitChanges();
    }

}
