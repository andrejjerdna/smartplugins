using Microsoft.ML;
using SmartPlugins.Common.ML.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartPlugins.Common.ML.Classification
{
    public class EstimatorBuilder
    {
        private readonly MLContext _mlContext;

        public EstimatorBuilder(MLContext mlContext)
        {
            _mlContext = mlContext;
        }

        /// <summary>
        /// Get the estimator for a type
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public IEstimator<ITransformer> GetEstimator(Type type) => GenerateEstimatorForType(type);

        /// <summary>
        /// Generate the estimator for a type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private IEstimator<ITransformer> GenerateEstimatorForType(Type type)
        {
            var properties = type.GetProperties();

            IEstimator<ITransformer> mainEstimator = null;
            var columnNames = new List<string>();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false).OfType<ClusterClassificationStatusAttribute>();

                foreach (var attribute in attributes)
                {
                    if (!attribute.IsAdded)
                        continue;

                    IEstimator<ITransformer> estimator = null;

                    if (attribute.Type == typeof(string))
                        estimator = GetTextEstimator(ref columnNames, property.Name, "Featurized");

                    if (property.PropertyType == typeof(float))
                        estimator = GetDigitEstimator(ref columnNames, attribute.NormalizeType, property.Name, "Normalize");

                    if (estimator == null)
                        continue;

                    if (mainEstimator == null)
                        mainEstimator = estimator;
                    else
                        mainEstimator.Append(estimator, Microsoft.ML.Data.TransformerScope.Training);
                }
            }

            return mainEstimator.Append(_mlContext.Transforms.Concatenate("Features", columnNames.ToArray()));
        }

        private IEstimator<ITransformer> GetTextEstimator(ref List<string> columnNames, string name, string postfix)
        {
            columnNames.Add(name + postfix);

            return _mlContext.Transforms.Text.FeaturizeText(inputColumnName: name, outputColumnName: name + postfix);
        }

        private IEstimator<ITransformer> GetDigitEstimator(ref List<string> columnNames, NormalizeType normalizeType, string name, string postfix)
        {
            columnNames.Add(name + postfix);

            //TODO: не обработан тип None
            switch (normalizeType)
            {
                case NormalizeType.NormalizeLogMeanVariance:
                    return _mlContext.Transforms.NormalizeLogMeanVariance(inputColumnName: name, outputColumnName: name + postfix);
                case NormalizeType.NormalizeMeanVariance:
                    return _mlContext.Transforms.NormalizeMeanVariance(inputColumnName: name, outputColumnName: name + postfix);
                case NormalizeType.NormalizeBinning:
                    return _mlContext.Transforms.NormalizeBinning(inputColumnName: name, outputColumnName: name + postfix);
                case NormalizeType.NormalizeGlobalContrast:
                    return _mlContext.Transforms.NormalizeGlobalContrast(inputColumnName: name, outputColumnName: name + postfix);
                default:
                    return _mlContext.Transforms.NormalizeLogMeanVariance(inputColumnName: name, outputColumnName: name + postfix);
            }
        }
    }
}
