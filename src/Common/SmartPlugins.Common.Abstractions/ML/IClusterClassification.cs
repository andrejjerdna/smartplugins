using System.Collections.Generic;

namespace SmartPlugins.Common.Abstractions.ML
{
    public interface IClusterClassification<T> 
        where T : class
    {
        bool MLTraining(IEnumerable<T> datas, double stopFactor = 0.985, double skipPercent = 0.8);
        uint MLPredictor(T teklaPartData);
    }
}
