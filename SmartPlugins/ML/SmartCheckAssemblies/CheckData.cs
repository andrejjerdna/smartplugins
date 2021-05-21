using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCheckAssemblies
{
    public class CheckData
    {
        [ColumnName("Label")]
        public bool Label { get; set; }

        [ColumnName("Features")]
        [VectorType(9)]
        public float[] Features { get; set; }
    }

    public class ClusterPrediction : CheckData
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Probability { get; set; }

        public float Score { get; set; }
    }
}
