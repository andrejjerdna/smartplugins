using SmartPlugins.Common.Abstractions;
using System;
using System.Text;
using System.Threading;
using Tekla.Structures.Model.Operations;

namespace SmartPlugins.Macros.Library.Loggers
{
    public class DisplayPromptProgressLogger : IProgressLogger
    {
        private readonly char _blockDone = '■';
        private readonly char _blockEmpty = '☐';
        private readonly int _lenghtProgressBar = 10;
        private CancellationTokenSource _cancellationTokenSource;

        public DisplayPromptProgressLogger()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public CancellationToken CancellationToken { get => _cancellationTokenSource.Token; }

        public void UpdateState(IProgressState progressState)
        {
            var n = GeneratProgressBar(progressState.Message, progressState.CurrentValue, progressState.TotalCount);
            Operation.DisplayPrompt(n);
        }

        public void Open()
        {
            Operation.DisplayPrompt("Smart macro start!");
        }

        public void Close()
        {
            Operation.DisplayPrompt("Smart macro stop!");
        }

        /// <summary>
        /// Generate a progress bar 
        /// </summary>
        /// <param name="currentCount"></param>
        /// <param name="maxNumber"></param>
        /// <returns></returns>
        private string GeneratProgressBar(string message, int currentCount, int maxNumber)
        {
            var percent = ((double)currentCount / maxNumber);

            if (double.IsNaN(percent))
                return message;

            if (double.IsInfinity(percent))
                return message;

            var doneBlocks = (int)Math.Ceiling(_lenghtProgressBar * percent);
            var emptyBlocks = _lenghtProgressBar - doneBlocks;

            return new StringBuilder().Append(message)
                                      .Append(": ")
                                      .Append(_blockDone, doneBlocks)
                                      .Append(_blockEmpty, emptyBlocks)
                                      .ToString();
        }
    }
}
