using AxCoDesign.Plugins.Model.Model.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using System.Collections;
using AxCoDesign.Applications.Library;
using AxCoDesign.ML.Library.AutoConnect;

namespace AxCoDesign.Applications.AutoConnect.Model
{
    interface IConnectTrainer
    {
        bool TeklaModelTraining();
        TeklaPluginRules TeklaModelTraining(uint predict, IMLResultsViewer resultsViewer);
    }

    public class ConnectTrainer : IConnectTrainer
    {
        private TeklaModel _teklaModel;
        private ObservableCollection<TeklaPluginRules> _dataRules;
        private IEnumerable<IModelData> _datas;

        public ConnectTrainer(TeklaModel teklaModel, IEnumerable<IModelData> datas, ObservableCollection<TeklaPluginRules> dataRules)
        {
            _teklaModel = teklaModel;
            _dataRules = dataRules;
            _datas = datas;
        }

        public bool TeklaModelTraining()
        {
            var trainData = _datas.GroupBy(data => data.Predict).Select(g => g.First()).OrderBy(g => g.Predict);

            var count = 1;

            foreach (var appData in trainData)
            {
                var result = TeklaModelTraining(appData.Predict);

                if (result != null)
                    _dataRules.Add(result);
                count++;
            }

            _teklaModel.CurrentModel.CommitChanges();

            ModelView.RedrawVisibleViews();

            return true;
        }

        public TeklaPluginRules TeklaModelTraining(uint predict, IMLResultsViewer resultsViewer = null)
        {
            var trainData = _datas.GroupBy(data => data.Predict).Select(g => g.First()).OrderBy(g => g.Predict);

            foreach (var appData in trainData)
            {
                if (appData.Predict != predict)
                    continue;
                
                ModelView.RedrawVisibleViews();

                var beam = _teklaModel.CurrentModel.SelectModelObject(new Identifier(appData.GUID)) as Beam;

                if (beam == null)
                    continue;

                var point = new Point(beam.StartPoint.X + 100, beam.StartPoint.Y, beam.StartPoint.Z);

                if (appData.Type == AppDataTypesEnum.END)
                    point = new Point(beam.EndPoint.X - 100, beam.EndPoint.Y, beam.EndPoint.Z);

                var selectObjects = new ArrayList();
                selectObjects.Add(beam);

                resultsViewer.DrawPredictionType(beam, point, appData.Predict, "Элемент для крепления");

                var ms = new Tekla.Structures.Model.UI.ModelObjectSelector();
                ms.Select(selectObjects);
                Tekla.Structures.ModelInternal.Operation.dotStartAction("ZoomToSelected", "");

                var result = new TrainingWindow();
                result.ShowDialog();

                ModelView.RedrawVisibleViews();

                return result.GetUserInput(appData);
            }

            return null;
        }
    }
}
