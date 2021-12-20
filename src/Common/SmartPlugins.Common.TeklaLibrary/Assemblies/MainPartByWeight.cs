using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Task = System.Threading.Tasks.Task;

namespace SmartPlugins.Common.TeklaLibrary.Assemblies
{
    /// <summary>
    /// Main part by weight
    /// </summary>
    public sealed class MainPartByWeight : IMainPartByWeight
    {
        private readonly IProgressLogger _progressLogger;
        private readonly ISmartModel _smartModel;
        private readonly ParallelOptions _parallelOptions;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="smartModel"></param>
        /// <param name="progressLogger"></param>
        public MainPartByWeight(ISmartModel smartModel, IProgressLogger progressLogger)
        {
            _smartModel = smartModel;
            _progressLogger = progressLogger;
            _parallelOptions = new ParallelOptions();
            _parallelOptions.CancellationToken = _progressLogger.CancellationToken;
            _parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
        }

        /// <inheritdoc/>
        public void CheckAllAssemblies()
        {
            _progressLogger.Open();

            var task = Task.Run(() =>
            {
                _progressLogger.UpdateState(new ProgressState(0, 0, "Get assemblies...", true));

                var assemblies = new ConcurrentBag<Assembly>(_smartModel.GetAllObjects<Assembly>(true));

                var count = 0;
                var totalCount = assemblies.Count;

                Parallel.ForEach(assemblies, _parallelOptions, (assembly) =>
                {
                    var check = AssemblyOperations.SetMainPartByMaxWeight(assembly);

                    if (!check)
                        assembly.Modify();

                    count++;
                    _progressLogger.UpdateState(new ProgressState(count, totalCount, "WriteUDAs...", false));

                    if (_parallelOptions.CancellationToken.IsCancellationRequested)
                        throw new RunMacroException(MessagesEN.MacroRunCanceled);
                });

                _smartModel.CommitChanges();
            }, _progressLogger.CancellationToken);

            task.Wait();

            _progressLogger.Close();
        }

        /// <inheritdoc/>
        public void CheckAssembly<T>(T assembly) where T : class
        {
            if (assembly == null)
                return;

            var teklaAssembly = assembly as Assembly;

            if (teklaAssembly == null)
                return;

            var check = AssemblyOperations.SetMainPartByMaxWeight(teklaAssembly);

            if (check)
                teklaAssembly.Modify();
        }
    }
}
