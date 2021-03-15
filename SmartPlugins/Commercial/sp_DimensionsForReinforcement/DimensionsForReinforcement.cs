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

namespace sp_DimensionsForReinforcement
{
    public class DimensionsForReinforcementData
    {
        [StructuresField(nameof(L1))]
        public double L1;

        [StructuresField(nameof(L2))]
        public double L2;
    }

    [Plugin("sp_DimensionsForReinforcement")]
    [PluginUserInterface("sp_DimensionsForReinforcement.DimensionsForReinforcementWindow")]

    public class DimensionsForReinforcement : DrawingPluginBase
    {
        private DimensionsForReinforcementData _data;
        private tsm.Model _model;

        private ViewBase _view;

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

                inputs.Add(InputDefinitionFactory.CreateInputDefinition(view, pickedPart));
            }

            return inputs;
        }

        public override bool Run(List<InputDefinition> inputs)
        {
            if (inputs.Count == 0)
                return false;

            _view = InputDefinitionFactory.GetView(inputs[0]);
            var drawingObject =(ReinforcementGroup)InputDefinitionFactory.GetDrawingObject(inputs[0]);

            if(drawingObject == null || _view == null)
                return false;

            var rebarGroup = _model.SelectModelObject(drawingObject.ModelIdentifier) as RebarGroup;

            if (rebarGroup == null)
                return false;

            var points = GetGetRebarGeometries(rebarGroup);

            InsertLines(points.Item1, points.Item2);
/*
            var types = new[] { typeof(GridLine) };

            var gridLines = _view.GetAllObjects(types).ToIEnumerable<GridLine>().ToList();

            */
            return true;

        }

        private Tuple<t3d.Point, t3d.Point> GetGetRebarGeometries(RebarGroup rebarGroup)
        {
            var geom = rebarGroup.GetRebarGeometries(Reinforcement.RebarGeometryOptionEnum.NONE)
                .OfType<RebarGeometry>()
                .SelectMany(geomr => geomr.Shape.Points.OfType<t3d.Point>());

            var X = geom.Select(p => p.X);
            var Y = geom.Select(p => p.Y);
            var Z = geom.Select(p => p.Z);

            return new Tuple<t3d.Point, t3d.Point>(new t3d.Point(X.Min(),Y.Min(), Z.Max()), new t3d.Point(X.Max(), Y.Max(), Z.Max()));
        }

        private void InsertLines(t3d.Point point1, t3d.Point point2)
        {
            var baseLine = new LineTypeAttributes
            {
                Color = DrawingColors.Black,
                Type = LineTypes.DottedLine
            };
            
            var lineTypeAttributes = new Line.LineAttributes
            {
                Line = baseLine,
            };

            var rectangleTypeAttributes = new Rectangle.RectangleAttributes
            {
                Line = baseLine,
            };

            var diagonal1 = new Line(_view, point1, point2, lineTypeAttributes);
            diagonal1.Insert();

            var diagonal2 = new Line(_view, new t3d.Point(point2.X, point1.Y, point1.Z), new t3d.Point(point1.X, point2.Y, point2.Z), lineTypeAttributes);
            diagonal2.Insert();


            var rectangle = new Rectangle(_view, point1, point2, rectangleTypeAttributes);
            rectangle.Insert();
        }
    }
}
