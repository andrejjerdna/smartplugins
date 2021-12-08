using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.Core.Exceptions
{

    [Serializable]
    public class ContainerBuildExeption : Exception
    {
        public ContainerBuildExeption() { }
        public ContainerBuildExeption(string message) : base(message) { }
        public ContainerBuildExeption(string message, Exception inner) : base(message, inner) { }
        protected ContainerBuildExeption(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
