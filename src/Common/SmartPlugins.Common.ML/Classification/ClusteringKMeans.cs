using Microsoft.ML;
using Microsoft.ML.Trainers;
using SmartPlugins.Common.Abstractions.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.ML.DataOperationsCatalog;

namespace SmartPlugins.Common.ML.Classification
{
    public sealed class ClusteringKMeans<T> : IClusteringKMeans<T>
        where T : class
    {
        private readonly MLContext _mlContext;
        private readonly EstimatorBuilder _estimatorBuilder;

        private ITransformer _trainedModel;
        private PredictionEngine<T, ClusterClassificationPrediction> _predictionEngine;

        public ClusteringKMeans()
        {
            _mlContext = new MLContext(seed: 0);
            _estimatorBuilder = new EstimatorBuilder(_mlContext);
        }

        /// <inheritdoc/>
        public bool MLTraining(IEnumerable<T> datas, int numberOfClusters)
        {
            var options = new KMeansTrainer.Options
            {
                NumberOfClusters = numberOfClusters,
                OptimizationTolerance = 1e-45f,
                NumberOfThreads = 1,
            };

            try
            {
                var data = GetTrainTestData(datas);
                var pipeline = _estimatorBuilder.GetEstimator(typeof(T));
                var trainingPipeline = pipeline.Append(_mlContext.Clustering.Trainers.KMeans(options));
                _trainedModel = trainingPipeline.Fit(data.TrainSet);
                var testMetrics = _mlContext.Clustering.Evaluate(data.TestSet);
                _predictionEngine = _mlContext.Model.CreatePredictionEngine<T, ClusterClassificationPrediction>(_trainedModel);

                var predictions = _mlContext.Data.CreateEnumerable<ClusterClassificationPrediction>(data.TestSet, reuseRowObject: false).ToList();

                return true;
            }
            catch(Exception exp)
            {
                throw exp;
            }
        }

        /// <inheritdoc/>
        public IClusterClassificationPrediction MLPredictor(T teklaPartData)
        {
            return _predictionEngine.Predict(teklaPartData);
        }

        /// <summary>
        /// Split the dataset into the train set and test set
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        private TrainTestData GetTrainTestData(IEnumerable<T> datas)
        {
            var trainingData = _mlContext.Data.LoadFromEnumerable(datas);
            return _mlContext.Data.TrainTestSplit(trainingData, testFraction: 0.2);
        }
    }
}
