using System.Collections.Generic;

namespace SmartPlugins.Common.Abstractions.ML
{
    /// <summary>
    /// Clustering by the K-Means method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IClusteringKMeans<T> where T : class
    {
        /// <summary>
        /// Run training for a data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="numberOfClusters"></param>
        /// <returns></returns>
        bool MLTraining(IEnumerable<T> data, int numberOfClusters);

        /// <summary>
        /// Run prediction pipeline on one example
        /// </summary>
        /// <param name="example"></param>
        /// <returns></returns>
        IClusterClassificationPrediction MLPredictor(T example);
    }
}
