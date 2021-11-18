using SmartPlugins.Common.Abstractions.Exceptions;
using SmartPlugins.Common.Abstractions.Messages;
using SmartPlugins.Common.Core.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SmartPlugins.Common.Core.Exceptions
{
    public class ExceptionsLogger : IExceptionsLogger
    {
        public void AddException<T>(T e) where T : Exception 
        {
           // MessagesViewer.Show(e.Message, MessageType.Error);
        }

        public void AddEx<T>(IReadOnlyCollection<T> exps) where T : Exception
        {
            if (exps == null)
                return;
            
            foreach (var ex in exps)
                throw ex;
        }
    }
}
