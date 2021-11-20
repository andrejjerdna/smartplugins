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
        /// Open logger
        /// </summary>
        void Open();

        /// <summary>
        /// Close logger
        /// </summary>
        void Close();
    }
}
