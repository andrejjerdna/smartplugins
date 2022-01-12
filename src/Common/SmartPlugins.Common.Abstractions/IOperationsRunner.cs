namespace SmartPlugins.Common.Abstractions
{
    public interface IOperationsRunner
    {
        void AddOperation(IOperation operation);

        void SetProgressState(IProgressState progressState);

        void OperationsRunnerStop();
    }
}
