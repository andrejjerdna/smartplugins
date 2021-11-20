namespace SmartPlugins.Common.Abstractions.ML
{
    public interface IClusterClassificationPrediction
    {
        /// <summary>
        /// Predicted category
        /// </summary>
        uint Category { get; set; }

        /// <summary>
        /// Distances
        /// </summary>
        float[] Distances { get; set; }
    }
}
