using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.Exceptions;
using SmartPlugins.Common.TeklaLibrary.CommonParameters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Task = System.Threading.Tasks.Task;

namespace SmartPlugins.Common.TeklaLibrary
{
    /// <summary>
    /// Rebar numerator
    /// </summary>
    public class RebarNumerator : IRebarNumerator
    {
        private readonly IProgressLogger _progressLogger;
        private readonly ISmartModel _smartModel;
        private readonly ParallelOptions _parallelOptions;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="attributeForNumber"></param>
        public RebarNumerator(ISmartModel smartModel, IProgressLogger progressLogger)
        {
            _smartModel = smartModel;
            _progressLogger = progressLogger;
            _parallelOptions = new ParallelOptions();
            _parallelOptions.CancellationToken = _progressLogger.CancellationToken;
            _parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
        }

        /// <inheritdoc/>
        public void RefreshAllNumbers(string attributeForNumber)
        {
            _progressLogger.Open();

            var task = Task.Run(() =>
            {
                _progressLogger.UpdateState(new ProgressState(0, 0, "Get reinforcement...", true));

                var reinforcements = _smartModel.GetAllObjects<Reinforcement>(true);

                var sort = GetReinforcementNumberingItems(reinforcements).ToList();

                var rebarsGroups = GetNumbers(sort);

                WriteUDA(rebarsGroups, attributeForNumber);

            }, _progressLogger.CancellationToken);

            task.Wait();
 
            _progressLogger.Close();
        }

        /// <summary>
        /// Write UDA for reinforcements
        /// </summary>
        /// <param name="reinforcements"></param>
        private void WriteUDA(IEnumerable<ReinforcementNumberingItem> reinforcements, string attributeForNumber)
        {
            var rebars = new ConcurrentBag<ReinforcementNumberingItem>(reinforcements);

            var count = 0;
            var totalCount = rebars.Count;

            Parallel.ForEach(rebars, _parallelOptions, (rebar) =>
            {
                rebar.OriginObject.SetUserProperty(attributeForNumber, rebar.Number);
                count++;
                _progressLogger.UpdateState(new ProgressState(count, totalCount, "WriteUDAs...", false));

                if (_parallelOptions.CancellationToken.IsCancellationRequested)
                    throw new RunMacroException(MessagesEN.MacroRunCanceled);
            });
        }

        /// <summary>
        /// Get list of reinforcement numbering items
        /// </summary>
        /// <param name="reinforcements"></param>
        /// <returns></returns>
        private IEnumerable<ReinforcementNumberingItem> GetReinforcementNumberingItems(IEnumerable<Reinforcement> reinforcements)
        {
            var result = new ConcurrentBag<ReinforcementNumberingItem>();

            var rebars = new ConcurrentBag<Reinforcement>(reinforcements);

            var count = 0;
            var totalCount = rebars.Count;

            Parallel.ForEach(rebars, _parallelOptions, (rebar) =>
            {
                var reportProperties = new ReportProperties(rebar);

                reportProperties.AddStringAttribute(CommonConcreteParts.CastUnitPos);
                reportProperties.AddStringAttribute(CommonConcreteParts.RebarPos);

                reportProperties.RefreshReportProperties();

                result.Add(new ReinforcementNumberingItem()
                {
                    OriginObject = rebar,
                    CastUnitPos = reportProperties.GetAttribute<string>(CommonConcreteParts.CastUnitPos),
                    RebarPos = reportProperties.GetAttribute<string>(CommonConcreteParts.RebarPos)
                });

                count++;
                _progressLogger.UpdateState(new ProgressState(count, totalCount, "Get reinforcement numbering items...", false));

                if (_parallelOptions.CancellationToken.IsCancellationRequested)
                    throw new RunMacroException(MessagesEN.MacroRunCanceled);
            });

            return result;
        }

        /// <summary>
        /// Get numbers for list of reinforcement numbering items
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        private IEnumerable<ReinforcementNumberingItem> GetNumbers(IEnumerable<ReinforcementNumberingItem> sort)
        {
            _progressLogger.UpdateState(new ProgressState(0, 1, "Get numbers...", true));

            var rebarsGroups = sort.GroupBy(r => r.CastUnitPos)
                       .Select(group => group)
                       .Select(group => group.GroupBy(r => r.RebarPos).OrderBy(g => g.Key).Select(g => g));

            foreach (var rebarGoup in rebarsGroups)
            {
                var count = 1;
                foreach (var gr in rebarGoup)
                {
                    foreach (var rebar in gr)
                        rebar.Number = count;

                    count++;
                }

                if (_parallelOptions.CancellationToken.IsCancellationRequested)
                    throw new RunMacroException(MessagesEN.MacroRunCanceled);
            }

            return rebarsGroups.SelectMany(gr => gr).SelectMany(gr => gr).Select(r => r);
        }
    }
}
