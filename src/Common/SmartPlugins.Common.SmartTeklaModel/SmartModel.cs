using SmartPlugins.Common.SmartExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.SmartTeklaModel
{
    public class SmartModel
    {
        private Model _teklaModel;

        public SmartModel()
        {
            _teklaModel = new Model();

            if (ConnectionStatus)
            {
                AttributesPath = Path.Combine(_teklaModel.GetInfo().ModelPath, "attributes");
                FiltersPath = Path.Combine(AttributesPath);
                SmartPluginsPath = Path.Combine(AttributesPath,"SmartPlugins");
            }
        }

        /// <summary>
        /// Connection status changed event
        /// </summary>
        public event EventHandler ConnectionStatusChanged;

        /// <summary>
        /// Current tekla model
        /// </summary>
        public Model TeklaModel { get => _teklaModel; }

        /// <summary>
        /// Connection status for current tekla model
        /// </summary>
        public bool ConnectionStatus { get => _teklaModel.GetConnectionStatus(); }

        /// <summary>
        /// Filters path
        /// </summary>
        public string FiltersPath { get; }

        /// <summary>
        /// Attributes path
        /// </summary>
        public string AttributesPath { get; }

        /// <summary>
        /// TODO: rename
        /// </summary>
        public string SmartPluginsPath { get; }

        public IEnumerable<T> GetAllObjects<T>()
        {
            return _teklaModel.GetModelObjectSelector().GetAllObjects().ToIEnumerable<T>();
        }
    }
}
