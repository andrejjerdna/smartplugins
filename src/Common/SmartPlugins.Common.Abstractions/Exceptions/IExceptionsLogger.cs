using System;
using System.Collections.Generic;

namespace SmartPlugins.Common.Abstractions.Exceptions
{
    public interface IExceptionsLogger
    {
        void AddException<T>(T e) where T : Exception;

        void AddEx<T>(IReadOnlyCollection<T> exps) where T : Exception;
    }
}
