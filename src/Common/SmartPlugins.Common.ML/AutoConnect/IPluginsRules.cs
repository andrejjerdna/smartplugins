using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.ML.AutoConnect
{
    interface IPluginsRules
    {
        public uint Prediction { get; set; }
        public IModelData AppData { get; set; }
        public string NameComponent { get; set; }
        public int NumberComponent { get; set; }
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

    public enum PluginRulesEnum
    {
        MAINPART,
        SECONDARYPART
    }

    public enum TypePluginEnum
    {
        Connection,
        Component
    }

    public enum InputType
    {
        ONE_BEAM,
        COLLECTION
    }
}
