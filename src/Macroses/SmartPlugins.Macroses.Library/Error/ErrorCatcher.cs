using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.Exceptions;
using SmartPlugins.Common.TeklaLibrary.Messages;
using System;
using System.Threading;

namespace SmartPlugins.Macros.Library
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
                if (Mutex.TryOpenExisting(MessagesLibrary.MacroMutex, out _mutex))
                    throw new RunMacroException(MessagesLibrary.MacroRunExeption);

                _progressBar = MacroProgressBar.GetMacroProgressBar();

                action();
            }
            catch (RunMacroException e)
            {
                StatusBarMessage.Show(e.Message);
            }
            catch (AggregateException e)
            {
                StatusBarMessage.Show(e.InnerException.InnerException.Message);
            }
            catch (ApplicationException e)
            {
                StatusBarMessage.Show(e.Message);
            }
            catch (Exception e)
            {
                StatusBarMessage.Show(e.Message);
            }
            finally
            {
                _progressBar.Close();
                _mutex?.Dispose();
            }
        }
    }
}
