using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.ML
{
    public enum NormalizeType
    {
        None,
        NormalizeMeanVariance,
        NormalizeBinning,
        NormalizeGlobalContrast,
        NormalizeLogMeanVariance
    }
}