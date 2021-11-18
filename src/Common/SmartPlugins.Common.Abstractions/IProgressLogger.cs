using System.Threading;

namespace SmartPlugins.Common.Abstractions
{
    public interface IProgressLogger
    {
        /// <summary>
        /// Cancellation token
        /// </summary>
        CancellationToken CancellationToken { get; }

        /// <summary>
        /// Update state
        /// </summary>
        /// <param name="progressState"></param>
        void UpdateState(IProgressState progressState);

        /// <summary>
        /// Open
        /// </summary>
        void Open();

        /// <summary>
        /// Close
        /// </summary>
        void Close();
    }
}
