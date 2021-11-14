using AxCoDesign.ML.Library.AutoConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace AxCoDesign.Applications.AutoConnect.Model
{
    public class TeklaPluginRules
    {
        public uint Prediction { get; set; }
        public IModelData AppData { get; set; }
        public string NameComponent { get; set; }
        public int NumberComponent{get; set; }
        public IEnumerable<string> UserSettings { get; set; }
        public string UserSetting { get; set; }
        public IEnumerable<Part> MainParts { get; set; }
        public IEnumerable<Part> SecondaryParts { get; set; }
        public PluginRulesEnum PluginRulesType { get; set; }
        public bool DublicatePlugin { get; set; }
        public TypePluginEnum TypePlugin { get; set; }
        public InputType InputTypeMain { get; set; }
        public InputType InputTypeSecondary { get; set; }
    }
}
