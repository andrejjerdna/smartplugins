using System;

namespace SmartPlugins.Common.Core.Exceptions
{
    [Serializable]
    public class TeklaConnectException : Exception
    {
        public TeklaConnectException() { }
        public TeklaConnectException(string message) : base(message) { }
        public TeklaConnectException(string message, Exception inner) : base(message, inner) { }
        protected TeklaConnectException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
