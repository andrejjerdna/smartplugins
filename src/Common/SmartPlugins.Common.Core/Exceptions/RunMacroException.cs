using System;

namespace SmartPlugins.Common.Core.Exceptions
{
    [Serializable]
    public class RunMacroException : Exception
    {
        public RunMacroException() { }
        public RunMacroException(string message) : base(message) { }
        public RunMacroException(string message, Exception inner) : base(message, inner) { }
        protected RunMacroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
