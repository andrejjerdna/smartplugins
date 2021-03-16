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
        DrawingHandler _drawingHandler;

        private ViewBase _view;

        private t3d.Point _p1;
        private t3d.Point _p2;
        private t3d.Point _p3;
        private t3d.Point _p4;

        public DimensionsForReinforcement(DimensionsForReinforcementData data)
        {
            _data = data;
            _model = new tsm.Model();
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
            var drawingObject = (ReinforcementGroup)InputDefinitionFactory.GetDrawingObject(inputs[0]);

            if (drawingObject == null || _view == null)
                return false;

            var rebarGroup = _model.SelectModelObject(drawingObject.ModelIdentifier) as RebarGroup;

            if (rebarGroup == null)
                return false;

            var points = GetGetRebarGeometries(rebarGroup);

            _p1 = points.Item1;
            _p2 = points.Item2;
            _p3 = new t3d.Point(_p2.X, _p1.Y, _p1.Z);
            _p4 = new t3d.Point(_p1.X, _p2.Y, _p2.Z);

            InsertLines(points.Item1, points.Item2);

            InsertDimensions(_view.GetAllObjects());

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

            return new Tuple<t3d.Point, t3d.Point>(new t3d.Point(X.Min(), Y.Min(), Z.Max()), new t3d.Point(X.Max(), Y.Max(), Z.Max()));
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

            var diagonal1 = new Line(_view, _p1, _p2, lineTypeAttributes);
            diagonal1.Insert();

            var diagonal2 = new Line(_view, _p3, _p4, lineTypeAttributes);
            diagonal2.Insert();

            var rectangle = new Rectangle(_view, _p1, _p2, rectangleTypeAttributes);
            rectangle.Insert();
        }

        private void InsertDimensions(DrawingObjectEnumerator drawingObjects)
        {
            var pointX1 = new t3d.Point(_p1.X, (_p1.Y + _p4.Y) / 2, _p1.Z);
            var pointX2 = new t3d.Point(_p3.X, (_p1.Y + _p4.Y) / 2, _p1.Z);

            var pointY1 = new t3d.Point((_p1.X + _p3.X) / 2, _p1.Y, _p1.Z);
            var pointY2 = new t3d.Point((_p1.X + _p3.X) / 2, _p2.Y, _p1.Z);

            var tempLenghtX = double.MaxValue;
            var tempLenghtY = double.MaxValue;

            var minY = double.MaxValue;
            var maxY = double.MaxValue;

            var pointsX = new Tuple<t3d.Point, t3d.Point>(null, null);
            var pointsY = new Tuple<t3d.Point, t3d.Point>(null, null);

            foreach (var obj in drawingObjects)
            {
                if (obj is GridLine gridLine)
                {
                    var currentX = gridLine.StartLabel.GridPoint.X;
                    var currentY = gridLine.StartLabel.GridPoint.Y;

                    if ((gridLine.StartLabel.GridPoint.X - gridLine.EndLabel.GridPoint.X) < 0.01)
                    {
                        var l1 = PointsGeometry.LenghtBetweenPoints(pointX1, new t3d.Point(gridLine.StartLabel.GridPoint.X, pointX1.Y, pointX1.Z));
                        var l2 = PointsGeometry.LenghtBetweenPoints(pointX2, new t3d.Point(gridLine.StartLabel.GridPoint.X, pointX2.Y, pointX2.Z));

                        if (Math.Min(l1, l2) < tempLenghtX)
                        {
                            if (l1 < l2)
                            {
                                pointsX = new Tuple<t3d.Point, t3d.Point>(pointX1, new t3d.Point(gridLine.StartLabel.GridPoint.X, pointX1.Y, pointX1.Z));
                            }
                            else
                            {
                                pointsX = new Tuple<t3d.Point, t3d.Point>(pointX2, new t3d.Point(gridLine.StartLabel.GridPoint.X, pointX2.Y, pointX2.Z));
                            }

                            tempLenghtX = Math.Min(l1, l2);
                        }
                    }

                    if ((gridLine.StartLabel.GridPoint.Y - gridLine.EndLabel.GridPoint.Y) < 0.01)
                    {
                        var l1 = PointsGeometry.LenghtBetweenPoints(pointY1, new t3d.Point(pointY1.X, gridLine.StartLabel.GridPoint.Y, pointY1.Z));
                        var l2 = PointsGeometry.LenghtBetweenPoints(pointY2, new t3d.Point(pointY2.X, gridLine.StartLabel.GridPoint.Y, pointY2.Z));

                        if (Math.Min(l1, l2) < tempLenghtY)
                        {
                            if (l1 < l2)
                            {
                                pointsY = new Tuple<t3d.Point, t3d.Point>(pointY1, new t3d.Point(pointY1.X, gridLine.StartLabel.GridPoint.Y, pointY1.Z));
                            }
                            else
                            {
                                pointsY = new Tuple<t3d.Point, t3d.Point>(pointY2, new t3d.Point(pointY1.X, gridLine.StartLabel.GridPoint.Y, pointY2.Z));
                            }

                            tempLenghtY = Math.Min(l1, l2);
                        }
                    }
                }
            }

            var dimension1 = new StraightDimension(_view, pointsX.Item1, pointsX.Item2, new t3d.Vector(0, 1, 0), 0.0);
            dimension1.Insert();

            var dimension2 = new StraightDimension(_view, pointsY.Item1, pointsY.Item2, new t3d.Vector(1, 0, 0), 0.0);
            dimension2.Insert();
        }
    }
}
