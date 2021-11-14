using Microsoft.ML.Data;

namespace SmartPlugins.Common.ML.Classification
{
    public class ClusterClassificationPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint Category;

        [ColumnName("Score")]
        public float[] Distances;
    }    {
    }
}
