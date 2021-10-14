using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPlugins.Common.Navisworks
{
    public class Class1
    {
        [Plugin("SmartColor", "SMARTPLUGINS")]
        [AddInPlugin(AddInLocation.CurrentSelectionContextMenu)]
        public class MessageReceiver : AddInPlugin
        {
            private Document _activeDocument;
            private ModelItemCollection _selectedItems;

            public override int Execute(params string[] parameters)
            {
                _activeDocument = Autodesk.Navisworks.Api.Application.ActiveDocument;

                if (_activeDocument == null)
                    return 0;

                _selectedItems = new ModelItemCollection(_activeDocument.CurrentSelection.SelectedItems);

                _activeDocument.Models.OverrideTemporaryColor(_selectedItems, Color.Red);
                _activeDocument.Models.OverrideTemporaryTransparency(_selectedItems, 0.0);

                return 0;
            }
        }
    }
}
