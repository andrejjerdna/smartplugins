using SmartPlugins.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.Core
{
    public class OperationBuilder : IOperationBuilder
    {
        public void AddObjects<T>(IEnumerable<T> objects)
        {
            throw new NotImplementedException();
        }

        public void AddOperation(IOperation operation)
        {
            throw new NotImplementedException();
        }
    }
}
