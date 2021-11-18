using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.Abstractions.TeklaStructures
{
    public interface ISmartModel
    {
        bool ConnectionStatus { get; }
        string FilterPath { get; }
        string AttributesPath { get; }
        string SmartPluginsPath { get; }

        IEnumerable<T> GetAllObjects<T>(bool autoFetch);

        ConcurrentBag<T> GetAllObjectsConcurrent<T>(bool autoFetch);

        void ReConnect();

        bool? CommitChanges();
    }
}
