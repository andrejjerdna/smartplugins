namespace SmartPlugins.Common.Abstractions
{
    public interface IProgressState
    {
        /// <summary>
        /// Current value
        /// </summary>
        int CurrentValue { get; }

        /// <summary>
        /// Total count
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Message
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Is indeterminate
        /// </summary>
        bool IsIndeterminate { get; }
    }
}
