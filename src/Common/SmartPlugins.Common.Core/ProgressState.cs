using SmartPlugins.Common.Abstractions;

namespace SmartPlugins.Common.Core
{
    /// <summary>
    /// Progress state
    /// </summary>
    public struct ProgressState : IProgressState
    {
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="totalCount"></param>
        /// <param name="message"></param>
        /// <param name="isIndeterminate"></param>
        public ProgressState(int currentValue, int totalCount, string message, bool isIndeterminate)
        {
            CurrentValue = currentValue;
            TotalCount = totalCount;
            Message = message;
            IsIndeterminate = isIndeterminate;
        }

        /// <inheritdoc/>
        public int CurrentValue { get; }

        /// <inheritdoc/>
        public int TotalCount { get; }

        /// <inheritdoc/>
        public string Message { get; }

        /// <inheritdoc/>
        public bool IsIndeterminate { get; }
    }
}
