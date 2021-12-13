using Microsoft.ML;
using Microsoft.ML.Trainers;
using SmartPlugins.Common.Abstractions.ML;
using SmartPlugins.Common.ML.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartPlugins.Common.ML.Classification
{
    public class ClusterClassification<T> : IClusterClassification<T> 
        where T : class
    {
        private readonly MLContext _mlContext;
        private readonly EstimatorBuilder _estimatorBuilder;
        private double _stopFactor;
        private double _skipPercent;
        private ITransformer _trainedModel;
        private List<Tuple<string, IEstimator<ITransformer>>> _inputColumNames;
        private List<double> _localDelta;
        private List<double> _averageDistances;

        public ClusterClassification()
        {
            _mlContext = new MLContext(seed: 0);
            _estimatorBuilder = new EstimatorBuilder(_mlContext);
        }

        public bool MLTraining(IEnumerable<T> datas, double stopFactor = 0.985, double skipPercent = 0.8)
        {
            _stopFactor = stopFactor;
            _skipPercent = skipPercent;

            _localDelta = new List<double>();
            _averageDistances = new List<double>();

            var resultTrain = false;
            var count = 2;
            var statusTrain = true;

            while (statusTrain)
            {
                try
                {
                    statusTrain = TrainStep(count, datas);
                    count++;
                }
                catch
                {
                    TrainStep(count - 1, datas);
                    statusTrain = false;
                }
            }

            resultTrain = true;

            return resultTrain;
        }

        public uint MLPredictor(T teklaPartData)
        {
            var predEngine = _mlContext.Model.CreatePredictionEngine<T, ClusterClassificationPrediction>(_trainedModel);

            return predEngine.Predict(teklaPartData).Category;
        }

        private bool TrainStep(int count, IEnumerable<T> datas)
        {
            //_inputColumNaёmes = new List<Tuple<string, IEstimator<ITransformer>>>();

            var options = new KMeansTrainer.Options
            {
                NumberOfClusters = count,
                OptimizationTolerance = 1e-45f,
                NumberOfThreads = 1,
            };

            var trainingData = _mlContext.Data.LoadFromEnumerable(datas);
            var data = _mlContext.Data.TrainTestSplit(trainingData, testFraction: 0.3);
            var pipeline = _estimatorBuilder.GetEstimator(typeof(T));
            var trainingPipeline = pipeline.Append(_mlContext.Clustering.Trainers.KMeans(options));
            _trainedModel = trainingPipeline.Fit(data.TrainSet);
            Evaluate(datas);

            return GetEndStatus();
        }

        private void Evaluate(IEnumerable<T> datas)
        {
            var testDataView = _mlContext.Data.LoadFromEnumerable(datas);
            var testMetrics = _mlContext.Clustering.Evaluate(_trainedModel.Transform(testDataView));
            _averageDistances.Add(testMetrics.AverageDistance);
        }

        private bool GetEndStatus()
        {
            var selectingElements = (int)Math.Floor(_averageDistances.Count() * _skipPercent);
            var vals = _averageDistances.Skip(Math.Max(0, selectingElements)).ToList();

            if (vals.Count < 1)
                return true;

            var dY = Math.Abs(vals.Max() - vals.Min());
            var dX = vals.Count;

            var tgA = dY / dX;
            var angel = Math.Atan(tgA) * 180 / Math.PI;

            if (angel < _stopFactor && dY > 0.00001)
                _localDelta.Add(angel);
            else
                _localDelta.Clear();

            if (_localDelta.Count == 3)
                return false;

            return true;
        }
    }  
}
