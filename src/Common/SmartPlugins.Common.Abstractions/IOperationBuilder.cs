using System.Collections.Generic;

namespace SmartPlugins.Common.Abstractions
{
    public interface IOperationBuilder
    {
        void AddObjects<T>(IEnumerable<T> objects);

        void AddOperation(IOperation operation);
    }
}
