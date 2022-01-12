using static Tekla.Structures.Model.Operations.Operation;

namespace SmartPlugins.Macros.Library
{
    /// <summary>
    /// Progress bar for macros.
    /// This is a wrapper class over Tekla.Structures.Model.Operations.Operation
    /// </summary>
    public class MacroProgressBar
    {
        private static MacroProgressBar _macroProgress;
        private static ProgressBar _progressBar;
        private MacroProgressBar()
        {
            _progressBar = new ProgressBar();
        }

        /// <summary>
        /// Display progress bar
        /// </summary>
        /// <param name="sleepTime"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="cancelButtonLabel"></param>
        /// <param name="progressLabel"></param>
        /// <returns></returns>
        public bool Display(int sleepTime, string title, string message, string cancelButtonLabel, string progressLabel) 
            => _progressBar.Display(sleepTime, title, message, cancelButtonLabel, progressLabel);

        /// <summary>
        /// Close progress bar
        /// </summary>
        public void Close() => _progressBar.Close();

        /// <summary>
        /// Canceled progress
        /// </summary>
        /// <returns></returns>
        public bool Canceled() => _progressBar.Canceled();

        /// <summary>
        /// Set progress
        /// </summary>
        /// <param name="progressLabel"></param>
        /// <param name="progress"></param>
        public void SetProgress(string progressLabel, int progress) => _progressBar.SetProgress(progressLabel, progress);

        /// <summary>
        /// Get progress bar for macros
        /// </summary>
        /// <returns></returns>
        public static MacroProgressBar GetMacroProgressBar()
        {
            if (_macroProgress == null)
                _macroProgress = new MacroProgressBar();

            return _macroProgress;
        }
    }
}
