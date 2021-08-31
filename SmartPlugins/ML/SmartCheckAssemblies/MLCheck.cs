using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using SmartPlugins.Common.SmartTeklaModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.ML.DataOperationsCatalog;

namespace SmartPlugins.Applications.SmartCheckAssembliesML
{
    public class MLCheck
    {
        private SmartModel _smartModel;
        private string _pathData;
        private IEnumerable<CheckData> _data;

        public MLCheck()
        {
            _smartModel = new SmartModel();
            _pathData = _smartModel.SmartPluginsPath + "check.json";
            _data = GetData();
        }

        private IEnumerable<CheckData> GetData()
        {
            var jsonString = File.ReadAllText(_pathData);
            return JsonSerializer.Deserialize<IEnumerable<CheckData>>(jsonString);
        }

        public void MLTraining()
        {
            var mlContext = new MLContext(seed: 0);

            var data = _data.Where(d => d != null);

            var trainingData = mlContext.Data.LoadFromEnumerable(data);

            var splitDataView = mlContext.Data.TrainTestSplit(trainingData, testFraction: 0.1);

            var model = BuildAndTrainModel(mlContext, splitDataView.TrainSet);

            Evaluate(mlContext, model, splitDataView.TestSet);
        }

        public ITransformer BuildAndTrainModel(MLContext mlContext, IDataView splitTrainSet)
        {
            var estimator = mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression();

            var model = estimator.Fit(splitTrainSet);

            return model;
        }

        public void Evaluate(MLContext mlContext, ITransformer model, IDataView splitTestSet)
        {
            var predictions = model.Transform(splitTestSet);

            var metrics = mlContext.BinaryClassification.Evaluate(predictions);

            using (var fileStream = new FileStream(_smartModel.SmartPluginsPath+"ml.zip", FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                mlContext.Model.Save(model, splitTestSet.Schema, fileStream);
            }
        }

        public bool MLPredictor(CheckData data)
        {
            var mlContext = new MLContext(seed: 0);

            DataViewSchema modelSchema;
            ITransformer trainedModel;

            using (var fileStream = new FileStream(_smartModel.SmartPluginsPath + "ml.zip", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                trainedModel = mlContext.Model.Load(fileStream, out modelSchema);
            }

            var predictor = mlContext.Model.CreatePredictionEngine<CheckData, ClusterPrediction>(trainedModel);

            var prediction = predictor.Predict(data);

            return prediction.Prediction;
        }
    }
}
