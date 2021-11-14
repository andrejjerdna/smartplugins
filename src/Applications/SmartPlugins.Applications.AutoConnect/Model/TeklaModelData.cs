using AxCoDesign.ML.Library.AutoConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxCoDesign.Applications.AutoConnect.Model
{
    public class TeklaModelData : IModelData
    {
        public Guid GUID { get; set; }
        public AppDataTypesEnum Type { get; set; }
        public MLDataConnect DataConnect { get; set; }
        public uint Predict { get; set; }
    }
}
