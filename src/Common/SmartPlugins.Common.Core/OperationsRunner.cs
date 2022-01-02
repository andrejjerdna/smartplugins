using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Core.Exceptions;
using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace SmartPlugins.Common.Core
{
    public class OperationsRunner : IOperationsRunner
    {
        private readonly ISubject<IOperation> _statChange;
        private readonly IProgressLogger _progressLogger;

        public OperationsRunner(IProgressLogger progressLogger)
        {
            _progressLogger = progressLogger;
            _statChange = new Subject<IOperation>();
            _progressLogger.Open();

            _statChange.Subscribe(operation => RunOperationAsync(operation));
        }

        public void AddOperation(IOperation operation)
        {
            _statChange.OnNext(operation);
        }

        public void SetProgressState(IProgressState progressState)
        {
            _progressLogger.UpdateState(progressState);
        }

        public void OperationsRunnerStop()
        {
            _statChange.OnCompleted();
            _progressLogger.Close();
        }

        private async void RunOperationAsync(IOperation operation)
        {
            await Task.Run(() =>
            {
                operation.Run();

                if (_progressLogger.CancellationToken.IsCancellationRequested)
                    _statChange.OnCompleted();
            });
        }

        //TODO: Добавить возможность переключения на синхронный режим выполнения операций.
        private void RunOperationSync(IOperation operation)
        {
            operation.Run();

            if (_progressLogger.CancellationToken.IsCancellationRequested)
                _statChange.OnError(new RunMacroException(MessagesEN.MacroRunCanceled));
        }
    }
}
