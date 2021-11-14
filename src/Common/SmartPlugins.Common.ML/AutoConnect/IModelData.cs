using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.ML.AutoConnect
{
    public interface IModelData
    {
        Guid GUID { get; set; }
        AppDataTypesEnum Type { get; set; }
        MLDataConnect DataConnect { get; set; }
        uint Predict { get; set; }
    }

    public enum AppDataTypesEnum
    {
        START,
        END
    }
}
