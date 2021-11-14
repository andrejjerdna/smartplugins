using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.ML.AutoConnect
{
    public class MLDataConnect
    {
        [KeyType(2)]
        public uint Label { get; set; }

        [VectorType(53)]
        public float[] Features { get; set; }
    }

    public class MLPrediction
    {
        public uint Label { get; set; }

        public uint PredictedLabel { get; set; }
    }
}
