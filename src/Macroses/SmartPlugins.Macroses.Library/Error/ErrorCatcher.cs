using SmartPlugins.Common.Abstractions.Messages;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.Exceptions;
using SmartPlugins.Common.Core.Messages;
using System;
using System.Threading;

namespace SmartPlugins.Macroses.Library
{
    /// <summary>
    /// Error catcher
    /// </summary>
    public class ErrorCatcher
    {
        private static Mutex _mutex;
        private static MacroProgressBar _progressBar;

        /// <summary>
        /// Try action
        /// </summary>
        /// <param name="action"></param>
        public static void Try(Action action)
        {
            try
            {
                if (Mutex.TryOpenExisting(MessagesEN.MacroMutex, out _mutex))
                    throw new RunMacroException(MessagesEN.MacroRunExeption);

                _progressBar = MacroProgressBar.GetMacroProgressBar();

                action();
            }
            catch (RunMacroException e)
            {
                MessagesViewer.Show(e.Message, MessageType.Error);
            }
            catch (AggregateException e)
            {
                MessagesViewer.Show(e.InnerException.InnerException.Message, MessageType.Error);
            }
            catch (Exception e)
            {
                MessagesViewer.Show(e.Message, MessageType.Error);
            }
            finally
            {
                _progressBar.Close();
                _mutex?.Dispose();
            }
        }
    }
}
