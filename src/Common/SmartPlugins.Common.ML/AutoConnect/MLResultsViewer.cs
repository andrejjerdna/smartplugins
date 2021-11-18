using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using tsm = Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;
using AxCoDesign.Plugins.Model.Model.View;
using Tekla.Structures;
using AxCoDesign.Applications.Library;
using System.Collections.Concurrent;
using Parallel = System.Threading.Tasks.Parallel;
using AxCoDesign.Plugins.Model.TeklaModelObjects;

namespace SmartPlugins.Common.ML.AutoConnect
{
    public interface IMLResultsViewer : IHasProgress<ProgressState>
    {
        Task<bool> DrawConnectTypesInTeklaModel(int numberOfClusters, IEnumerable<IModelData> datas);
        void DrawPredictionType(Beam beam, Point point, uint type, string message);
    }

    public class MLResultsViewer : IMLResultsViewer
    {
        private TeklaModel _teklaModel;

        public IProgressTracker<ProgressState> Progress { get; }

        public MLResultsViewer(TeklaModel teklaModel)
        {
            _teklaModel = teklaModel;

            Progress = new ProgressTracker<ProgressState>();
        }

        /// <summary>
        /// Обозначить полученные типы соединений в модели.
        /// </summary>
        /// <param name="prefixMainPart"></param>
        /// <param name="numberOfClusters"></param>
        public async Task<bool> DrawConnectTypesInTeklaModel(int numberOfClusters, IEnumerable<IModelData> datas)
        {
            var currentTP = _teklaModel.CurrentModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            try
            {
                ModelView.RedrawVisibleViews();

                var maxCount = datas.Count();
                var currentCount = 0;

                foreach (var data in datas)
                {
                    _teklaModel.CurrentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);

                    var beam = _teklaModel.CurrentModel.SelectModelObject(new Identifier(data.GUID)) as Beam;

                    if (beam == null)
                        continue;

                    MarkTypeInTeklaModel(beam, data.Predict, data.Type);

                    _teklaModel.CurrentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);

                    _teklaModel.CurrentModel.CommitChanges();

                    currentCount++;

                    Progress.Report(new ProgressState(currentCount, maxCount, string.Format("{2}: {0} / {1}", currentCount, maxCount, MLMessages.StatusProgress)));
                }

                Progress.Report(new ProgressState(0, 1, MLMessages.ResultsVisibleDone));

                _teklaModel.CurrentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
                _teklaModel.CurrentModel.CommitChanges();

                return true;
            }
            catch (Exception exp)
            {
                Progress.Report(new ProgressState(0, 1, MLMessages.ResultsVisibleError));
                return false;
            }
            finally
            {
                _teklaModel.CurrentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
                _teklaModel.CurrentModel.CommitChanges();
            }

        }

        private void MarkTypeInTeklaModel(Beam beam, uint type, AppDataTypesEnum positionMark)
        {
            var currentTP = _teklaModel.CurrentModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            var p = beam.StartPoint;

            if (positionMark == AppDataTypesEnum.END)
                p = beam.EndPoint;

            var beamTP = new TransformationPlane(beam.GetCoordinateSystem());
            _teklaModel.CurrentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(beamTP);
            p = beamTP.TransformationMatrixToLocal.Transform(currentTP.TransformationMatrixToGlobal.Transform(p));

            var point = new Point(p.X + 250, p.Y, p.Z);

            if (positionMark == AppDataTypesEnum.END)
                point = new Point(p.X - 250, p.Y, p.Z);

            DrawPredictionType(point, type);

            _teklaModel.CurrentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
        }

        private void DrawPredictionType(Point point, uint type, string message = "ТИП: {0}")
        {
            var colors = new List<Color>
                {
                        new Color(0.0, 0.0, 0.0),
                        new Color(0.0, 0.0, 0.0),
                        new Color(1.0, 0.3, 0.3),
                        new Color(0.0, 1.0, 0.0),
                        new Color(0.0, 0.0, 1.0),
                        new Color(0.5, 0.5, 0.0),
                        new Color(0.0, 0.5, 0.5),
                        new Color(0.5, 0.5, 0.5),
                        new Color(1.0, 0.5, 0.0),
                        new Color(0.5, 0.1, 0.0),
                        new Color(0.0, 1.0, 0.5),
                };

            var currentColor = new Color(0.0, 0.0, 0.0);

            if (type < colors.Count)
                currentColor = colors[(int)type];

            new GraphicsDrawer().DrawText(point, string.Format(message, type), currentColor);
        }

        public void DrawPredictionType(Beam beam, Point point, uint type, string message = "ТИП: {0}")
        {
            DrawPredictionType(point, type);

            DrawPredictionType(beam.GetCenterPoint(), type, message);
        }

    }
}
