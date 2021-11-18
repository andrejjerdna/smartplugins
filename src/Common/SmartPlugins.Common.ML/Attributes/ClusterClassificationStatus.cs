using System;

namespace SmartPlugins.Common.ML.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ClusterClassificationStatus : Attribute
    {
        public bool Status { get; set; }

        public ClusterClassificationStatus(bool status)
        {
            Status = status;
        }
    }
}
