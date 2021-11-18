using SmartPlugins.Common.Abstractions;
using System;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;

namespace SmartPlugins.Common.Core
{
    public abstract class ProgressBarViewModelBase : ViewModelBase, IProgressBarViewModel
    {
        private static CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();

        /// <summary>
        /// .ctor
        /// </summary>
        public ProgressBarViewModelBase()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        private readonly Dispatcher _dispatcher;

        /// <inheritdoc/>
        public Dispatcher Dispatcher => _dispatcher;

        private int _totalCount;

        /// <inheritdoc/>
        public int TotalCount
        {
            get => _totalCount;
            set
            {
                _totalCount = value;
                OnPropertyChanged(nameof(TotalCount));
            }
        }

        private int _currentValue;

        /// <inheritdoc/>
        public int CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                OnPropertyChanged(nameof(CurrentValue));
            }
        }

        private string _message;

        /// <inheritdoc/>
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private string _title;

        /// <inheritdoc/>
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private CancellationToken _cancellationToken = _cancelTokenSource.Token;

        /// <inheritdoc/>
        public CancellationToken CancellationToken
        {
            get => _cancellationToken;
            set
            {
                _cancellationToken = value;
                OnPropertyChanged(nameof(CancellationToken));
            }
        }

        private bool _isIndeterminate;

        /// <inheritdoc/>
        public bool IsIndeterminate
        {
            get => _isIndeterminate;
            set
            {
                _isIndeterminate = value;
                OnPropertyChanged(nameof(IsIndeterminate));
            }
        }

        /// <inheritdoc/>
        public void UpdateProgressState(IProgressState progressState)
        {
            _totalCount = progressState.TotalCount;
            _currentValue = progressState.CurrentValue;
            _message = progressState.Message;
            _isIndeterminate = progressState.IsIndeterminate;
            OnPropertyChanged(nameof(CurrentValue));
            OnPropertyChanged(nameof(TotalCount));
            OnPropertyChanged(nameof(Message));
            OnPropertyChanged(nameof(IsIndeterminate));
        }

        public ICommand Cancel
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _cancelTokenSource.Cancel();
                });
            }
        }
    }
}
