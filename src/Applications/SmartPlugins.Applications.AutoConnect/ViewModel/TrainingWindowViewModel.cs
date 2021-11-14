using AxCoDesign.Applications.AutoConnect.Model;
using AxCoDesign.Applications.Library.WindowsHelpers;
using AxCoDesign.ML.Library.AutoConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace AxCoDesign.Applications.AutoConnect.ViewModel
{
    public class TrainingWindowViewModel : BaseViewModel
    {
        public string NameComponent
        {
            get => Get(string.Empty);
            set => Set(value);
        }
        public int NumberComponent
        {
            get => Get(0);
            set => Set(value);
        }
        public string UserSettings
        {
            get => Get(string.Empty);
            set => Set(value);
        }

        public string DublicatePluginString
        {
            get => Get("Нет");
            set => Set(value);
        }

        public IEnumerable<Part> MainParts
        {
            get => Get(new List<Part>());
            set => Set(value);
        }
        public IEnumerable<Part> SecondaryParts
        {
            get => Get(new List<Part>());
            set => Set(value);
        }

        public PluginRulesEnum PluginRulesType
        {
            get => Get(PluginRulesEnum.MAINPART);
            set => Set(value);
        }
        public bool DublicatePlugin
        {
            get => Get(false);
            set => Set(value);
        }
        public TypePluginEnum TypePlugin
        {
            get => Get(TypePluginEnum.Component);
            set => Set(value);
        }

        private void FillAttributeComboBox(string ExportFormat)
        {
            List<string> folders = Tekla.Structures.Dialog.UIControls.EnvironmentFiles.GetStandardPropertyFileDirectories();
            List<string> viewPropertyFiles = Tekla.Structures.Dialog.UIControls.EnvironmentFiles.GetMultiDirectoryFileList(folders, ExportFormat);
            //foreach (string propertyFile in viewPropertyFiles)
            //{
            //    this.comboBoxFile.Items.Add(propertyFile);
            //}
        }
    }
}
