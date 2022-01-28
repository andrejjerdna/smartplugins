using System;
using System.Threading;

namespace SmartPlugins.Common.Abstractions
{
    public interface IOperationsRunner
    {
        void AddOperation(Action operation);

        void SetProgressState(IProgressState progressState);

        void OperationsRunnerStop();

        CancellationToken CancellationToken { get; }
    }
}
