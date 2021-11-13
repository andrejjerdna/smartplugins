using System.Threading;
using System.Windows.Threading;

namespace SmartPlugins.Common.Abstractions
{
    /// <summary>
    /// ProgressBar view model
    /// </summary>
    public interface IProgressBarViewModel
    {
        /// <summary>
        /// Total count
        /// </summary>
        int TotalCount { get; set; }

        /// <summary>
        /// Current value
        /// </summary>
        int CurrentValue { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Title 
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Cancellation token
        /// </summary>
        CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// Dispatcher
        /// </summary>
        Dispatcher Dispatcher { get; }

        /// <summary>
        /// Update a progress state
        /// </summary>
        /// <param name="progressState"></param>
        void UpdateProgressState(IProgressState progressState);
    }
}
