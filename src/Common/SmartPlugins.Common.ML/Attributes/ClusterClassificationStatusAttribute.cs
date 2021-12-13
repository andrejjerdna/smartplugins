using System;

namespace SmartPlugins.Common.ML.Attributes
{
    /// <summary>
    /// Attribute for a property when it is used in a cluster classification algorithm
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ClusterClassificationStatusAttribute : Attribute
    {
        /// <summary>
        /// This property added in cluster classification
        /// </summary>
        public bool IsAdded { get; set; }

        /// <summary>
        /// Property type
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Property is featurized (only for string type)
        /// </summary>
        public bool IsFeaturized { get; set; }

        /// <summary>
        /// Normalize type
        /// </summary>
        public NormalizeType NormalizeType { get; set; }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="isAdded"></param>
        public ClusterClassificationStatusAttribute(bool isAdded, Type type, bool isFeaturized, NormalizeType normalizeType)
        {
            IsAdded = isAdded;
            Type = type;
            IsFeaturized = isFeaturized;
            NormalizeType = normalizeType;
        }
    }
}
