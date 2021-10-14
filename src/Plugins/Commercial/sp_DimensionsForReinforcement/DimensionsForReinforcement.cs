using SmartTeklaModel.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Drawing.Tools;
using Tekla.Structures.Drawing.UI;
using Tekla.Structures.Plugins;
using static Tekla.Structures.Plugins.DrawingPluginBase;
using tsm = Tekla.Structures.Model;

namespace sp_DimensionsForReinforcement
{
    [Plugin("sp_DimensionsForReinforcement")]
    [PluginUserInterface("sp_DimensionsForReinforcement.sp_DimensionsForReinforcementWindow")]

    public class DimensionsForReinforcement : ConcretePluginBase
    {
        private DimensionsForReinforcementData _data;
        private tsm.Model _model;

        public DimensionsForReinforcement(DimensionsForReinforcementData data)
        {
            _data = data;
            _model = new tsm.Model();
        }

        public override List<InputDefinition> DefineInput()
        {
            List<InputDefinition> inputs = new List<InputDefinition>();
            DrawingHandler drawingHandler = new DrawingHandler();

            if (drawingHandler.GetConnectionStatus())
            {
                Picker picker = drawingHandler.GetPicker();

                ViewBase view = null;
                DrawingObject pickedPart = null;

                picker.PickObject("Pick part", out pickedPart, out view);

                var pickedPoints = picker.PickPoints(_pickerPrompts);

                inputs.Add(InputDefinitionFactory.CreateInputDefinition(view, pickedPart));
                inputs.Add(InputDefinitionFactory.CreateInputDefinition(pickedPoints));
            }

            return inputs;
        }

        public override bool Run(List<InputDefinition> Input)
        {
            GetValuesFromDialog();

            return true;
        }

        private void GetValuesFromDialog()
        {
        }
    }
}
