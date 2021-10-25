using SmartPlugins.Common.Abstractions;

namespace SmartPlugins.Common.Core
{
    /// <summary>
    /// Progress state
    /// </summary>
    public struct ProgressState : IProgressState
    {
        public readonly int _currentValue;
        public readonly int _totalCount;
        public readonly string _message;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="totalCount"></param>
        /// <param name="message"></param>
        public ProgressState(int currentValue, int totalCount, string message)
        {
            _currentValue = currentValue;
            _totalCount = totalCount;
            _message = message;
        }

        /// <inheritdoc/>
        public int CurrentValue { get => _currentValue; }

        /// <inheritdoc/>
        public int TotalCount { get => _totalCount; }

        /// <inheritdoc/>
        public string Message { get => _message; }
    }
}
