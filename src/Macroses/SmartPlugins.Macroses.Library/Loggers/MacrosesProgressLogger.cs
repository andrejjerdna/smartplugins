using SmartPlugins.Common.Abstractions;
using System;
using System.Threading;

namespace SmartPlugins.Macroses.Library
{
    /// <summary>
    /// Macroses progress logger
    /// </summary>
    public class MacrosesProgressLogger : IProgressLogger, IDisposable
    {
        private readonly MacroProgressBar _progressBar;
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// .ctor
        /// </summary>
        public MacrosesProgressLogger()
        {
            _progressBar = MacroProgressBar.GetMacroProgressBar();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        /// <inheritdoc/>
        public CancellationToken CancellationToken { get => _cancellationTokenSource.Token; }

        /// <inheritdoc/>
        public void UpdateState(IProgressState progressState)
        {
            var status = progressState.CurrentValue + "/" + progressState.TotalCount;
            var progress = 0;

            if (progressState.TotalCount == 0)
                status = progressState.Message;
            else
                progress = 100 * progressState.CurrentValue / progressState.TotalCount;

            _progressBar.SetProgress(status, progress);

            if (_progressBar.Canceled())
            {
                _cancellationTokenSource.Cancel();
                _progressBar.Close();
            }
        }

        /// <inheritdoc/>
        public void Open()
        {
            try
            {
                _progressBar.Display(1000, "Smart macros", string.Empty, "Cancel", " ");
            }
            catch
            {
                _progressBar.Close();
                _progressBar.Display(1000, "Smart macros", string.Empty, "Cancel", " ");
            }
        }

        /// <inheritdoc/>
        public void Close() => _progressBar.Close();

        /// <inheritdoc/>
        public void Dispose() => _progressBar.Close();
    }
}
