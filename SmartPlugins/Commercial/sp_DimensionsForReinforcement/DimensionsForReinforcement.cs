using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tekla.Structures.Drawing;
using Tekla.Structures.Drawing.Tools;
using Tekla.Structures.Drawing.UI;
using Tekla.Structures.Model;
using Tekla.Structures.Plugins;
using tsm = Tekla.Structures.Model;
using t3d = Tekla.Structures.Geometry3d;
using SmartExtensions;
using static Tekla.Structures.Drawing.Line;
using Tekla.Structures.Geometry3d;
using Line = Tekla.Structures.Drawing.Line;
using SmartGeometry;
using SmartTeklaModel.Drawings;
using SmartObjects.Drawings;
using Tekla.Structures;
using System.Collections;
using ModelObjectSelector = Tekla.Structures.Model.UI.ModelObjectSelector;

namespace sp_DimensionsForReinforcement
{
    public class DimensionsForReinforcementData
    {
        [StructuresField(nameof(L1))] public double L1;
        [StructuresField(nameof(L2))] public double L2;
        [StructuresField(nameof(L3))] public double L3;
        [StructuresField(nameof(L4))] public double L4;

        [StructuresField(nameof(LineColor))] public string LineColor;
        [StructuresField(nameof(LineType))] public string LineType;
        [StructuresField(nameof(DimensionType))] public string DimensionType;

        [StructuresField(nameof(HatchColor))] public string HatchColor;
        [StructuresField(nameof(HatchName))] public string HatchName;
        [StructuresField(nameof(ScaleX))] public double ScaleX;
        [StructuresField(nameof(ScaleY))] public double ScaleY;
        [StructuresField(nameof(AngleHatch))] public double AngleHatch;
        [StructuresField(nameof(DimInsert))] public int DimInsert;
        [StructuresField(nameof(DiagInsert))] public int DiagInsert;
        [StructuresField(nameof(HatchInsert))] public int HatchInsert;

        [StructuresField(nameof(MarkInsert))] public int MarkInsert;
        [StructuresField(nameof(MarkType))] public string MarkType;
    }

    [Plugin("sp_DimensionsForReinforcement")]
    [PluginUserInterface("sp_DimensionsForReinforcement.DimensionsForReinforcementWindow")]

    public class DimensionsForReinforcement : DrawingPluginBase
    {
        private DimensionsForReinforcementData _data;
        private Model _model;
        private DrawingHandler _drawingHandler;

        private ViewBase _viewBase;

        public double L1 { get; set; }
        public double L2 { get; set; }
        public double L3 { get; set; }
        public double L4 { get; set; }
        public string LineColor { get; set; }
        public string LineType { get; set; }
        public string DimensionType { get; set; }


        public string HatchColor { get; set; }
        public string HatchName { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double AngleHatch { get; set; }
        public int DimensionInsert { get; set; }
        public int DiagonalInsert { get; set; }
        public int HatchInsert { get; set; }

        public int MarkInsert { get; set; }
        public string MarkType { get; set; }

        public DimensionsForReinforcement(DimensionsForReinforcementData data)
        {
            _data = data;
            _model = new Model();
        }

        public override List<InputDefinition> DefineInput()
        {
            List<InputDefinition> inputs = new List<InputDefinition>();
            _drawingHandler = new DrawingHandler();

            if (_drawingHandler.GetConnectionStatus())
            {
                var picker = _drawingHandler.GetPicker();

                ViewBase view = null;
                DrawingObject pickedPart = null;

                picker.PickObject("Pick rebarGroup", out pickedPart, out view);

                inputs.Add(InputDefinitionFactory.CreateInputDefinition(view, pickedPart));
            }

            return inputs;
        }

        public override bool Run(List<InputDefinition> inputs)
        {
            if (inputs.Count == 0)
                return false;

            GetValuesFromDialog();

            _viewBase = InputDefinitionFactory.GetView(inputs[0]);
            var drawingObject = (ReinforcementGroup)InputDefinitionFactory.GetDrawingObject(inputs[0]);

            if (drawingObject == null || _viewBase == null)
                return false;

            var rebarGroup = _model.SelectModelObject(drawingObject.ModelIdentifier) as RebarGroup;

            if (rebarGroup == null)
                return false;

            var dimension = new DimensionsForRebarGroup(_model, _viewBase, rebarGroup)
            {
                L1 = L1,
                L2 = L2,
                L3 = L3,
                L4 = L4,
                LineColor = LineColor,
                DimensionType = DimensionType,
                LineType = LineType,

                HatchColor = HatchColor,
                HatchName = HatchName,
                ScaleX = ScaleX,
                ScaleY = ScaleY,
                AngleHatch = AngleHatch,
                DimensionInsert = DimensionInsert,
                DiagonalInsert = DiagonalInsert,
                HatchInsert = HatchInsert,

                MarkInsert = MarkInsert,
                MarkType = MarkType,
            };

            dimension.Insert();

            return true;
        }

        private void GetValuesFromDialog()
        {
            L1 = _data.L1; if (IsDefaultValue(L1)) { L1 = 100.0; }
            L2 = _data.L2; if (IsDefaultValue(L2)) { L2 = 100.0; }

            L3 = _data.L3; if (IsDefaultValue(L3)) { L3 = 0.0; }
            L4 = _data.L4; if (IsDefaultValue(L4)) { L4 = 0.0; }

            LineColor = _data.LineColor; if (IsDefaultValue(LineColor)) { LineColor = "Black"; }
            LineType = _data.LineType; if (IsDefaultValue(LineType)) { LineType = "SlashedLine"; }
            DimensionType = _data.DimensionType; if (IsDefaultValue(DimensionType)) { DimensionType = "standard"; }
            MarkType = _data.MarkType; if (IsDefaultValue(MarkType)) { MarkType = "standard"; }

            HatchColor = _data.HatchColor; if (IsDefaultValue(HatchColor)) { HatchColor = "Black"; }
            HatchName = _data.HatchName; if (IsDefaultValue(HatchName)) { HatchName = "ANSI31"; }
            ScaleX = _data.ScaleX; if (IsDefaultValue(ScaleX)) { ScaleX = 1.0; }
            ScaleY = _data.ScaleY; if (IsDefaultValue(ScaleY)) { ScaleY = 1.0; }
            AngleHatch = _data.AngleHatch; if (IsDefaultValue(AngleHatch)) { AngleHatch = 0.0; }
            DimensionInsert = _data.DimInsert; if (IsDefaultValue(DimensionInsert)) { DimensionInsert = 0; }
            DiagonalInsert = _data.DiagInsert; if (IsDefaultValue(DiagonalInsert)) { DiagonalInsert = 0; }
            HatchInsert = _data.HatchInsert; if (IsDefaultValue(HatchInsert)) { HatchInsert = 0; }
            MarkInsert = _data.MarkInsert; if (IsDefaultValue(MarkInsert)) { MarkInsert = 0; }
        }
    }
}
