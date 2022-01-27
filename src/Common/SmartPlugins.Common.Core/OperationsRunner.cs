using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Core.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace SmartPlugins.Common.Core
{
    public class OperationsRunner : IOperationsRunner
    {
        private readonly ISubject<Action> _statChange;
        private readonly IProgressLogger _progressLogger;

        private ConcurrentBag<Task> _tasks;

        public OperationsRunner(IProgressLogger progressLogger)
        {
            _progressLogger = progressLogger;
            _statChange = new Subject<Action>();
            _tasks = new ConcurrentBag<Task>();
            
            _progressLogger.Open();

            _statChange.Subscribe(operation => RunOperationAsync(operation));
        }

        public void AddOperation(Action operation)
        {
            _statChange.OnNext(operation);
        }

        public void SetProgressState(IProgressState progressState)
        {
            _progressLogger.UpdateState(progressState);
        }

        public void OperationsRunnerStop()
        {
            Task.WhenAll(_tasks.ToArray());
            
            _statChange.OnCompleted();
            _progressLogger.Close();
        }

        private void RunOperationAsync(Action operation)
        {
            var task = Task.Run(() =>
            {
                operation.Invoke();

                if (_progressLogger.CancellationToken.IsCancellationRequested)
                    _statChange.OnCompleted();
            });

            _tasks.Add(task);
        }

        ///TODO: Add sync operations
        private void RunOperationSync(IOperation operation)
        {
            operation.Run();

            if (_progressLogger.CancellationToken.IsCancellationRequested)
                _statChange.OnError(new RunMacroException(MessagesLibrary.MacroRunCanceled));
        }
    }
}
