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
    public class MainPartByWeight : IMainPartByWeight
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
        public void CheckAssemblies()
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
                    var check = CheckAssembly(assembly);

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

        /// <summary>
        /// Check assembly.
        /// If the main part does not have the maximum weight, then return false 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private bool CheckAssembly(Assembly assembly)
        {
            Part candidatePart = null;

            var mainPart = assembly.GetMainPart();

            var weightMainPart = 0.0;
            mainPart.GetReportProperty("WEIGHT", ref weightMainPart);

            var assemblyDetails = assembly.GetSecondaries();

            foreach (var modelObject in assemblyDetails)
            {
                if (modelObject is Part part)
                {
                    var weightCurrentDetail = 0.0;
                    part.GetReportProperty("WEIGHT", ref weightCurrentDetail);

                    if (weightCurrentDetail > weightMainPart)
                    {
                        weightMainPart = weightCurrentDetail;
                        candidatePart = part;
                    }
                }
            }

            if (candidatePart != null)
            {
                assembly.SetMainPart(candidatePart);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
