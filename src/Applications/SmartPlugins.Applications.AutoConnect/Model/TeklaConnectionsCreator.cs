using AxCoDesign.Applications.Library;
using AxCoDesign.ML.Library.AutoConnect;
using AxCoDesign.Plugins.Model.Collections;
using AxCoDesign.Plugins.Model.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures;
using Tekla.Structures.Model;

namespace AxCoDesign.Applications.AutoConnect.Model
{
    interface ITeklaConnectionsCreator : IHasProgress<ProgressState>
    {
        Task<bool> InsertConnections();
    }

    public class TeklaConnectionsCreator : ITeklaConnectionsCreator
    {
        private TeklaModel _teklaModel;
        private IEnumerable<IModelData> _datas;
        private IEnumerable<TeklaPluginRules> _rules;
        private double _delta;
        private double _numberOfClusters;

        private List<Guid> ConnectsGuids;

        public IProgressTracker<ProgressState> Progress { get; }

        public TeklaConnectionsCreator(TeklaModel teklaModel, IEnumerable<IModelData> datas, IEnumerable<TeklaPluginRules> rules, double delta, int numberOfClusters)
        {
            _teklaModel = teklaModel;
            _datas = datas;
            _rules = rules;
            _delta = delta;
            _numberOfClusters = numberOfClusters;

            Progress = new ProgressTracker<ProgressState>();
            ConnectsGuids = new List<Guid>();
        }

        public async Task<bool> InsertConnections()
        {
            if (_datas == null || _rules == null)
                return false;

            var maxCount = _datas.Count();
            var currentCount = 0;

            foreach (var appData in _datas)
            {
                var beam = _teklaModel.CurrentModel.SelectModelObject(new Identifier(appData.GUID)) as Beam;

                if (beam == null)
                    continue;

                var dataConnect = appData.DataConnect;

                var rules = _rules.Where(r => r.Prediction == appData.Predict).ToList();

                if (rules.Count() > 0)
                {
                    var rule = rules.First();

                    var centerPoint = beam.StartPoint;

                    if (appData.Type == AppDataTypesEnum.END)
                        centerPoint = beam.EndPoint;

                    var partsConnect = _teklaModel.CurrentModel.GetDetailJoint(centerPoint, _delta).ToList();

                    var components = partsConnect.SelectMany(p => p.GetComponents().ToIEnumerable<BaseComponent>()).ToList();

                    var connect = new TeklaConnectCreator(_teklaModel, appData, partsConnect, rule);

                    var result = false;

                    if (rule.DublicatePlugin)
                    {
                        result = connect.Insert();
                    }
                    else
                    {
                        var statusSearch = Check(partsConnect, components);

                        if (!statusSearch)
                            result = connect.Insert();
                    }

                    if (result)
                        ConnectsGuids.Add(connect.ConnectGuid);

                    _teklaModel.CurrentModel.CommitChanges();
                }

                currentCount++;

                Progress.Report(new ProgressState(currentCount, maxCount, string.Format("{2}: {0} / {1}", currentCount, maxCount, MLMessages.StatusProgress)));
            }

            Progress.Report(new ProgressState(0, 1, ""));

            return true;
        }

        /// Поиск наличия уже вставленного в узел компонента.
        /// </summary>
        /// <param name="parts"></param>
        /// <param name="baseComponents"></param>
        /// <returns></returns>
        private bool Check(IEnumerable<Part> parts, List<BaseComponent> baseComponents)
        {
            var details = new List<Part>();

            foreach (var baseComponent in baseComponents)
            {
                details.AddRange(baseComponent.GetChildren().ToIEnumerable<Part>().Where(d => d != null));
            }

            var guids1 = parts.Select(p => p.Identifier.GUID);
            var guids2 = details.Select(p => p.Identifier.GUID);

            var result = guids1.Intersect(guids2);

            if (result.Count() > 0)
                return true;
            else
                return false;
        }

        public void DeleteConnections()
        {
            var maxCount = ConnectsGuids.Count();
            var currentCount = 0;

            foreach (var connectGuid in ConnectsGuids)
            {
                var connect = _teklaModel.CurrentModel.SelectModelObject(new Identifier(connectGuid)) as BaseComponent;

                if(connect != null)
                    connect.Delete();

                currentCount++;

                Progress.Report(new ProgressState(currentCount, maxCount, string.Format("{2}: {0} / {1}", currentCount, maxCount, MLMessages.StatusProgress)));
            }

            _teklaModel.CurrentModel.CommitChanges();

            Progress.Report(new ProgressState(0, 1, MLMessages.ConnectDeleteDone));
        }
    }
}
