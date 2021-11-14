using AxCoDesign.Applications.Library;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartPlugins.Common.ML.AutoConnect
{
    public class MLTrainer
    {
        private MLContext _mlContext;
        private ObservableCollection<IModelData> _datas;
        private int _numberOfClusters;
        private string _modelPath;

        private string _mlModelName = "ML.zip";

        public IProgressTracker<ProgressState> Progress { get; }

        public MLTrainer(ObservableCollection<IModelData> datas, int numberOfClusters, string modelPath)
        {
            _mlContext = new MLContext(seed: 0);
            _datas = datas;
            _numberOfClusters = numberOfClusters;
            _modelPath = modelPath + _mlModelName;
        }

        public bool MLTraining()
        {
            var datas = _datas.Select(d => d?.DataConnect).ToList();

            var trainingData = _mlContext.Data.LoadFromEnumerable(datas);

            var options = new KMeansTrainer.Options
            {
                NumberOfClusters = _numberOfClusters,
                OptimizationTolerance = 1e-45f,
                NumberOfThreads = 1,
            };

            var pipeline = _mlContext.Clustering.Trainers.KMeans(options);

            try
            {
                var model = pipeline.Fit(trainingData);

                using (var fileStream = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    _mlContext.Model.Save(model, trainingData.Schema, fileStream);
                }

                foreach(var data in _datas)
                    data.Predict = MLPredictor(model, data.DataConnect);

                return true;
            }
            catch(Exception exp)
            {
                //MessageBox.Show(exp.Message);
                return false;
            }
        }

        private uint MLPredictor(ITransformer trainedModel, MLDataConnect data)
        {
            var predictor = _mlContext.Model.CreatePredictionEngine<MLDataConnect, MLPrediction>(trainedModel);

            var prediction = predictor.Predict(data);

            return prediction.PredictedLabel;
        }
    }
}
