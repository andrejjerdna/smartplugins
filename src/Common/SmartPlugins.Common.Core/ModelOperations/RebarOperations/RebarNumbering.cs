//using SmartPlugins.Common.Abstractions;
//using SmartPlugins.Common.Abstractions.ModelObjects;
//using SmartPlugins.Common.Core.Exceptions;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SmartPlugins.Common.Core.ModelOperations.RebarOperations
//{
//    internal class RebarNumbering : IOperation
//    {
//        private readonly IEnumerable<IReinforcement> _reinforcements;
//        private readonly string _attributeForNumber;
//        public RebarNumbering(IEnumerable<IReinforcement> reinforcements, string attributeForNumber)
//        {
//            _reinforcements = reinforcements;
//            _attributeForNumber = attributeForNumber;
//        }

//        public void Run()
//        {
//            throw new NotImplementedException();
//        }

//        /// <inheritdoc/>
//        public void RefreshAllNumbers(string attributeForNumber)
//        {
//            _progressLogger.Open();

//            var task = Task.Run(() =>
//            {
//                _progressLogger.UpdateState(new ProgressState(0, 0, "Get reinforcement...", true));

//                var reinforcements = _smartModel.GetAllObjects<Reinforcement>(true);

//                var sort = GetReinforcementNumberingItems(reinforcements).ToList();

//                var rebarsGroups = GetNumbers(sort);

//                WriteUDA(rebarsGroups, attributeForNumber);

//            }, _progressLogger.CancellationToken);

//            task.Wait();

//            _progressLogger.Close();
//        }

//        /// <summary>
//        /// Write UDA for reinforcements
//        /// </summary>
//        /// <param name="reinforcements"></param>
//        private void WriteUDA(IEnumerable<ReinforcementNumberingItem> reinforcements, string attributeForNumber)
//        {
//            var rebars = new ConcurrentBag<ReinforcementNumberingItem>(reinforcements);

//            var count = 0;
//            var totalCount = rebars.Count;

//            Parallel.ForEach(rebars, _parallelOptions, (rebar) =>
//            {
//                rebar.OriginObject.SetUserProperty(attributeForNumber, rebar.Number);
//                count++;
//                _progressLogger.UpdateState(new ProgressState(count, totalCount, "WriteUDAs...", false));

//                if (_parallelOptions.CancellationToken.IsCancellationRequested)
//                    throw new RunMacroException(MessagesLibrary.MacroRunCanceled);
//            });
//        }

//        /// <summary>
//        /// Get list of reinforcement numbering items
//        /// </summary>
//        /// <param name="reinforcements"></param>
//        /// <returns></returns>
//        private IEnumerable<IReinforcement> GetReinforcementNumberingItems(IEnumerable<IReinforcement> reinforcements)
//        {
//            var result = new ConcurrentBag<IReinforcement>();

//            var rebars = new ConcurrentBag<IReinforcement>(reinforcements);

//            var count = 0;
//            var totalCount = rebars.Count;

//            Parallel.ForEach(rebars, _parallelOptions, (rebar) =>
//            {
//                var reportProperties = new ReportProperties(rebar);

//                reportProperties.AddStringAttribute(CommonConcreteParts.CastUnitPos);
//                reportProperties.AddStringAttribute(CommonConcreteParts.RebarPos);

//                reportProperties.RefreshReportProperties();

//                result.Add(new IReinforcement()
//                {
//                    OriginObject = rebar,
//                    CastUnitPos = reportProperties.GetAttribute<string>(CommonConcreteParts.CastUnitPos),
//                    RebarPos = reportProperties.GetAttribute<string>(CommonConcreteParts.RebarPos)
//                });

//                count++;
//                _progressLogger.UpdateState(new ProgressState(count, totalCount, "Get reinforcement numbering items...", false));

//                if (_parallelOptions.CancellationToken.IsCancellationRequested)
//                    throw new RunMacroException(MessagesLibrary.MacroRunCanceled);
//            });

//            return result;
//        }

//        /// <summary>
//        /// Get numbers for list of reinforcement numbering items
//        /// </summary>
//        /// <param name="sort"></param>
//        /// <returns></returns>
//        private IEnumerable<ReinforcementNumberingItem> GetNumbers(IEnumerable<ReinforcementNumberingItem> sort)
//        {
//            _progressLogger.UpdateState(new ProgressState(0, 1, "Get numbers...", true));

//            var rebarsGroups = sort.GroupBy(r => r.CastUnitPos)
//                       .Select(group => group)
//                       .Select(group => group.GroupBy(r => r.RebarPos).OrderBy(g => g.Key).Select(g => g));

//            foreach (var rebarGoup in rebarsGroups)
//            {
//                var count = 1;
//                foreach (var gr in rebarGoup)
//                {
//                    foreach (var rebar in gr)
//                        rebar.Number = count;

//                    count++;
//                }

//                if (_parallelOptions.CancellationToken.IsCancellationRequested)
//                    throw new RunMacroException(MessagesLibrary.MacroRunCanceled);
//            }

//            return rebarsGroups.SelectMany(gr => gr).SelectMany(gr => gr).Select(r => r);
//        }
//    }
//}
