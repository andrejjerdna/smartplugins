using AxCoDesign.Applications.Library;
using AxCoDesign.Plugins.Model.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using Parallel = System.Threading.Tasks.Parallel;
using System.Collections.ObjectModel;
using Tekla.Structures.Model.UI;
using AxCoDesign.Plugins.Model.Collections;
using System.Collections.Concurrent;
using AxCoDesign.Plugins.Model.TeklaModelObjects;
using AxCoDesign.ML.Library.AutoConnect;

namespace AxCoDesign.Applications.AutoConnect.Model
{
    interface ITeklaModelDatas : IHasProgress<ProgressState>
    {
        Task<bool> GetTeklaModelDatas();
    }

    public class TeklaModelDatas : ITeklaModelDatas
    {
        private TeklaModel _teklaModel { get; }
        private string _filterName { get; }
        private double _delta { get; }
        private ObservableCollection<IModelData> _datas;
        private List<int> _partsConnect;
        private List<int> _partsView;

        public IProgressTracker<ProgressState> Progress { get; }
        public bool GetObjectsWithCurrentView { get; set; }
        public string FilterConnectName { get; set; }

        public TeklaModelDatas(TeklaModel teklaModel, string filterName, double delta, ObservableCollection<IModelData> datas)
        {
            _teklaModel = teklaModel;
            _filterName = filterName;
            _delta = delta;
            _datas = datas;

            Progress = new ProgressTracker<ProgressState>();
            GetObjectsWithCurrentView = true;
            FilterConnectName = string.Empty;
        }

        /// <summary>
        /// Получение набора элементов из модели Tekla.
        /// </summary>
        public async Task<bool> GetTeklaModelDatas()
        {
            _partsConnect = new List<int>();
            _partsView = new List<int>();

            //Детали с которыми ищется соединение.
            if (!string.IsNullOrEmpty(FilterConnectName))
            {
                _partsConnect = _teklaModel.CurrentModel
                    .GetSecondaryesBeams<Beam>(FilterConnectName)
                    .Select(p => p.Identifier.ID)
                    .ToList();
            }

            //Получаем детали с вида.
            if (GetObjectsWithCurrentView)
            {
                try
                {
                    var aabbView = ViewHandler.GetSelectedViews().ToIEnumerable<View>().First().WorkArea;

                    Progress.Report(new ProgressState(0, 1, MLMessages.GetObjectsWithView));

                    _partsView = _teklaModel.CurrentModel
                       .GetDetailJoint(aabbView)
                       .Select(p => p.Identifier.ID)
                       .ToList();
                }
                catch
                {
                    Progress.Report(new ProgressState(0, 1, MLMessages.ViewSelectError));
                }
            }

            //Детали, для которых ищется соединение.
            var beams = _teklaModel.CurrentModel.GetConcurrentBagSecondaryesBeams(_filterName, _partsView);

            var currentCount = 0;
            var maxCount = beams.Count;

            var result = new List<TeklaModelData>();

            Parallel.ForEach(beams, (beam) =>
            {
                var dataBeam = GetFromBeam(beam);

                if (dataBeam.Item1 != null)
                    result.Add(dataBeam.Item1);

                if (dataBeam.Item2 != null)
                    result.Add(dataBeam.Item2);

                currentCount++;

                Progress.Report(new ProgressState(currentCount, maxCount, string.Format("{2}: {0} / {1}", currentCount, maxCount, MLMessages.StatusProgress)));
            });

            foreach (var data in result)
                _datas.Add(data);

            _teklaModel.CurrentModel.CommitChanges();

            Progress.Report(new ProgressState(0, 1, MLMessages.ModelDatasDone));

            return true;
        }

        /// <summary>
        /// Получение данных для балки. Получается набор деталей в кубе.
        /// </summary>
        /// <param name="beam"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        private Tuple<TeklaModelData, TeklaModelData> GetFromBeam(Beam beam)
        {
            var startPoint = beam.StartPoint;
            var endPoint = beam.EndPoint;

            var detailsStart = _teklaModel.CurrentModel.GetDetailJoint(startPoint, _delta, _partsConnect);
            var detailsEnd = _teklaModel.CurrentModel.GetDetailJoint(endPoint, _delta, _partsConnect);

            var centerLine = beam.GetCenterLine(false).OfType<Point>();

            var dataStart = GetData(centerLine.First(), detailsStart, AppDataTypesEnum.START, beam);
            var dataEnd = GetData(centerLine.Last(), detailsEnd, AppDataTypesEnum.END, beam);

            return new Tuple<TeklaModelData, TeklaModelData>(dataStart, dataEnd);
        }

        /// <summary>
        /// Получение данных для конца балки. Получается точка балки и набор элементов, с которыми она взаимодействует вконце.
        /// </summary>
        /// <param name="basePoint"></param>
        /// <param name="objects"></param>
        /// <param name="mainBeam"></param>
        /// <returns></returns>
        private TeklaModelData GetData(Point basePoint, IEnumerable<Part> objects, AppDataTypesEnum type, Beam mainBeam)
        {
            var result = new List<float>();

            var parts = objects.Where(obj => obj.Identifier.GUID != mainBeam.Identifier.GUID)
                .Select(p => new {Part = p, Height = p.GetPropertyDouble("HEIGHT"), Width = p.GetPropertyDouble("WIDTH") })
                .OrderBy(obj => obj.Height)
                .ThenBy(obj => obj.Width)
                .ToList();

            var heightMainPart = mainBeam.GetPropertyDouble("HEIGHT");
            var widthMainPart = mainBeam.GetPropertyDouble("WIDTH"); ;
            var detailsCount = parts.Count;

            result.Add((float)detailsCount);
            result.Add((float)heightMainPart);
            result.Add((float)widthMainPart);

            for (int i = 0; i < 10; i++)
            {
                var heightPart = 0.0f;
                var widthPart = 0.0f;

                try
                {
                    var detail = parts[i];

                    var part = detail.Part;

                    var mBeam = _teklaModel.CurrentModel.SelectModelObject(mainBeam.Identifier) as Beam;

                    if (mBeam == null)
                        continue;

                    var mainBeamPoints = mBeam.GetCenterLine(false).OfType<Point>().ToList();

                    var vector1 = new Vector(mainBeamPoints.First() - mainBeamPoints.Last());

                    var clBeam = _teklaModel.CurrentModel.SelectModelObject(part.Identifier) as Part;

                    if (clBeam == null)
                        continue;

                    var cl = clBeam.GetCenterLine(false).OfType<Point>().ToList();

                    var vector2 = new Vector(cl.First() - cl.Last());

                    var localBasePoint = basePoint;

                    var angle = vector1.GetAngleBetween(vector2);

                    heightPart = (float)detail.Height;
                    widthPart = (float)detail.Width;

                    result.Add((float)angle);
                    result.Add(0.0f);
                    result.Add(0.0f);
                    result.Add((float)heightPart);
                    result.Add((float)widthPart);
                }
                catch
                {
                    result.Add(0.0f);
                    result.Add(0.0f);
                    result.Add(0.0f);
                    result.Add(0.0f);
                    result.Add(0.0f);
                }
            }

            var data = new MLDataConnect();
            data.Label = (uint)0;
            data.Features = result.ToArray();

            return new TeklaModelData
            {
                GUID = mainBeam.Identifier.GUID,
                Type = type,
                DataConnect = data
            };
        }
    }
}

