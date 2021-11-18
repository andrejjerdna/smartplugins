using SmartPlugins.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartPlugins.Common.TeklaLibrary
{
    /// <summary>
    /// TODO: refactoring
    /// </summary>
    public class ApplicationsProgressLogger : IProgressLogger
    {
        private readonly IProgressBarViewModel _progressBarViewModel;
        private readonly IProgressWindow _progressWindow;
        private CancellationTokenSource _cancellationTokenSource;
        private Action<IProgressState> _progressEvent;

        /// <summary>
        /// .ctor
        /// </summary>
        public ApplicationsProgressLogger(IProgressBarViewModel progressBarViewModel, IProgressWindow progressWindow)
        {
            _progressBarViewModel = progressBarViewModel;
            _progressWindow = progressWindow;
            _cancellationTokenSource = new CancellationTokenSource();

            _progressEvent += _progressBarViewModel.UpdateProgressState;
        }

        /// <inheritdoc/>
        public CancellationToken CancellationToken { get => _cancellationTokenSource.Token; }

        /// <inheritdoc/>
        public void UpdateState(IProgressState progressState)
        {
            _progressBarViewModel.Dispatcher?.Invoke(new Action(() =>
            {
                _progressEvent?.Invoke(progressState);

                if (_progressBarViewModel.CancellationToken.IsCancellationRequested)
                {
                    _cancellationTokenSource.Cancel();
                    _progressWindow.Close();
                }
            }), System.Windows.Threading.DispatcherPriority.Send);
        }

        /// <inheritdoc/>
        public void Open()
        {
            _progressBarViewModel.Dispatcher?.Invoke(new Action(() =>
            {
                _progressWindow.Show();
            }), System.Windows.Threading.DispatcherPriority.Send);
        }

        /// <inheritdoc/>
        public void Close()
        {
            _progressBarViewModel.Dispatcher?.Invoke(new Action(() =>
            {
                _progressWindow.Close();
            }), System.Windows.Threading.DispatcherPriority.Send);
        }
    }

}
