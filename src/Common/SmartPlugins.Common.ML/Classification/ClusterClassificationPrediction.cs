using Microsoft.ML.Data;
using SmartPlugins.Common.Abstractions.ML;

namespace SmartPlugins.Common.ML.Classification
{
    public class ClusterClassificationPrediction : IClusterClassificationPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint Category { get; set; }

        [ColumnName("Score")]
        public float[] Distances { get; set; }
    }
}
