using System;

namespace SmartPlugins.Common.Core.Exceptions
{
    [Serializable]
    public class UserInputException : Exception
    {
        public UserInputException() { }
        public UserInputException(string message) : base(message) { }
        public UserInputException(string message, Exception inner) : base(message, inner) { }
        protected UserInputException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
