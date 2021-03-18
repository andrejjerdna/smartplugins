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

namespace SmartObjects.Drawings
{
    public class DimensionsForRebarGroup
    {
        private Model _model;

        private ViewBase _viewBase;
        private View _view;
        RebarGroup _rebarGroup;

        private t3d.Point _p1;
        private t3d.Point _p2;
        private t3d.Point _p3;
        private t3d.Point _p4;

        public double L1 { get; set; }
        public double L2 { get; set; }
        public double L3 { get; set; }
        public double L4 { get; set; }
        public string LineColor { get; set; }
        public string LineType { get; set; }
        public string DimensionType { get; set; }

        public DimensionsForRebarGroup(Model model, ViewBase viewBase, RebarGroup rebarGroup)
        {
            _model = model;
            _viewBase = viewBase;
            _rebarGroup = rebarGroup;
        }

        public bool Insert()
        {
            _view = _viewBase as View;

            if (_view == null)
                return false;

            GetGetRebarGeometries(_rebarGroup);

            InsertLines();

            InsertDimensions(_viewBase.GetAllObjects());

            return true;
        }

        /// <summary>
        /// Получаем базовые точки для отрисовки прямоугольника оласти армирования.
        /// </summary>
        /// <param name="rebarGroup"></param>
        private void GetGetRebarGeometries(RebarGroup rebarGroup)
        {
            var geom = rebarGroup.GetRebarGeometries(Reinforcement.RebarGeometryOptionEnum.NONE)
                .OfType<RebarGeometry>()
                .SelectMany(geomr => geomr.Shape.Points.OfType<t3d.Point>());

            var listPoints = new List<t3d.Point>
            {
            geom.ElementAt(0),
            geom.ElementAt(1),
            geom.ElementAt(geom.Count() - 2),
            geom.ElementAt(geom.Count() - 1),
            };

            listPoints = listPoints.OrderBy(p => p.X).ToList();

            _p1 = listPoints[0];
            _p2 = listPoints[1];
            _p3 = listPoints[2];
            _p4 = listPoints[3];
        }

        /// <summary>
        /// Отрисовка области армирования.
        /// </summary>
        private void InsertLines()
        {
            var localP1 = _p1;
            var localP2 = _p2;
            var localP3 = _p3;
            var localP4 = _p4;

            var baseLine = new LineTypeAttributes
            {
                Color = Colors.GetColor(LineColor),
                Type = Lines.GetLineTypes(LineType)
            };

            var lineTypeAttributes = new Line.LineAttributes
            {
                Line = baseLine,
            };

            var diagonal1 = new Line(_viewBase, localP1, localP4, lineTypeAttributes);
            diagonal1.Insert();

            var diagonal2 = new Line(_viewBase, localP2, localP3, lineTypeAttributes);
            diagonal2.Insert();

            var side1 = new Line(_viewBase, localP1, localP2, lineTypeAttributes);
            side1.Insert();

            var side2 = new Line(_viewBase, localP2, localP4, lineTypeAttributes);
            side2.Insert();

            var side3 = new Line(_viewBase, localP3, localP4, lineTypeAttributes);
            side3.Insert();

            var side4 = new Line(_viewBase, localP3, localP1, lineTypeAttributes);
            side4.Insert();
        }

        /// <summary>
        /// Простановка размеров до ближайших осей.
        /// </summary>
        /// <param name="drawingObjects"></param>
        private void InsertDimensions(DrawingObjectEnumerator drawingObjects)
        {
            var vectorX = new t3d.Vector(_p3 - _p1);
            var vectorY = new t3d.Vector(_p2 - _p1);

            var workCS = new CoordinateSystem(_p1, vectorX, vectorY);
            var workTP = new TransformationPlane(workCS);

            var viewTP = new TransformationPlane(_view.DisplayCoordinateSystem);

            _p1 = workTP.TransformationMatrixToLocal.Transform(viewTP.TransformationMatrixToGlobal.Transform(_p1));
            _p2 = workTP.TransformationMatrixToLocal.Transform(viewTP.TransformationMatrixToGlobal.Transform(_p2));

            _p3 = workTP.TransformationMatrixToLocal.Transform(viewTP.TransformationMatrixToGlobal.Transform(_p3));
            _p4 = workTP.TransformationMatrixToLocal.Transform(viewTP.TransformationMatrixToGlobal.Transform(_p4));



            var pointX1 = new t3d.Point(_p1.X, (_p1.Y + _p2.Y) / 2, _p1.Z);
            var pointX2 = new t3d.Point(_p3.X, (_p1.Y + _p2.Y) / 2, _p1.Z);

            var pointY1 = new t3d.Point((_p1.X + _p3.X) / 2, _p1.Y, _p1.Z);
            var pointY2 = new t3d.Point((_p1.X + _p3.X) / 2, _p2.Y, _p1.Z);

            var tempLenghtX = double.MaxValue;
            var tempLenghtY = double.MaxValue;

            var pointsX = new Tuple<t3d.Point, t3d.Point>(null, null);
            var pointsY = new Tuple<t3d.Point, t3d.Point>(null, null);

            foreach (var obj in drawingObjects)
            {
                if (obj is GridLine gridLine)
                {
                    var startGridPoint = gridLine.StartLabel.GridPoint;
                    var endGridPoint = gridLine.EndLabel.GridPoint;

                    startGridPoint = workTP.TransformationMatrixToLocal.Transform(viewTP.TransformationMatrixToGlobal.Transform(startGridPoint));
                    endGridPoint = workTP.TransformationMatrixToLocal.Transform(viewTP.TransformationMatrixToGlobal.Transform(endGridPoint));

                    var delta1 = startGridPoint.X - pointX1.X;
                    var delta2 = endGridPoint.X - pointX1.X;

                    if ((Math.Abs(delta1 - delta2)) < 0.01)
                    {
                        var l1 = PointsGeometry.LenghtBetweenPoints(pointX1, new t3d.Point(startGridPoint.X, pointX1.Y, pointX1.Z));
                        var l2 = PointsGeometry.LenghtBetweenPoints(pointX2, new t3d.Point(startGridPoint.X, pointX2.Y, pointX2.Z));

                        if (Math.Min(l1, l2) < tempLenghtX)
                        {
                            if (l1 < l2)
                            {
                                pointsX = new Tuple<t3d.Point, t3d.Point>(pointX1, new t3d.Point(startGridPoint.X, pointX1.Y, pointX1.Z));

                                if (l1 < L1)
                                {
                                    pointsX = new Tuple<t3d.Point, t3d.Point>(pointX2, new t3d.Point(startGridPoint.X, pointX1.Y, pointX1.Z));
                                }
                            }
                            else
                            {
                                pointsX = new Tuple<t3d.Point, t3d.Point>(pointX2, new t3d.Point(startGridPoint.X, pointX2.Y, pointX2.Z));

                                if (l2 < L1)
                                {
                                    pointsX = new Tuple<t3d.Point, t3d.Point>(pointX1, new t3d.Point(startGridPoint.X, pointX2.Y, pointX2.Z));
                                }
                            }

                            tempLenghtX = Math.Min(l1, l2);
                        }
                    }

                    var delta3 = startGridPoint.Y - pointY1.Y;
                    var delta4 = endGridPoint.Y - pointY1.Y;

                    if ((Math.Abs(delta3 - delta4)) < 0.01)
                    {
                        var l1 = PointsGeometry.LenghtBetweenPoints(pointY1, new t3d.Point(pointY1.X, startGridPoint.Y, pointY1.Z));
                        var l2 = PointsGeometry.LenghtBetweenPoints(pointY2, new t3d.Point(pointY2.X, startGridPoint.Y, pointY2.Z));

                        if (Math.Min(l1, l2) < tempLenghtY)
                        {
                            if (l1 < l2)
                            {
                                pointsY = new Tuple<t3d.Point, t3d.Point>(pointY1, new t3d.Point(pointY1.X, startGridPoint.Y, pointY1.Z));

                                if (l1 < L2)
                                {
                                    pointsY = new Tuple<t3d.Point, t3d.Point>(pointY2, new t3d.Point(pointY1.X, startGridPoint.Y, pointY1.Z));
                                }
                            }
                            else
                            {
                                pointsY = new Tuple<t3d.Point, t3d.Point>(pointY2, new t3d.Point(pointY2.X, startGridPoint.Y, pointY2.Z));

                                if (l2 < L2)
                                {
                                    pointsY = new Tuple<t3d.Point, t3d.Point>(pointY1, new t3d.Point(pointY2.X, startGridPoint.Y, pointY2.Z));
                                }
                            }

                            tempLenghtY = Math.Min(l1, l2);
                        }
                    }
                }
            }

            var dimensionsAttrbutes = new StraightDimensionSet.StraightDimensionSetAttributes(DimensionType);

            if (pointsX.Item1 != null && pointsX.Item2 != null)
            {
                var p1 = viewTP.TransformationMatrixToLocal.Transform(workTP.TransformationMatrixToGlobal.Transform(pointsX.Item1));
                var p2 = viewTP.TransformationMatrixToLocal.Transform(workTP.TransformationMatrixToGlobal.Transform(pointsX.Item2));

                var vector = new t3d.Vector(p2 - p1);

                var dimension1 = new StraightDimension(_viewBase, p1, p2, vector.Cross(new t3d.Vector(0, 0, 1)), L4 * _view.Attributes.Scale, dimensionsAttrbutes);
                dimension1.Insert();
            }
            if (pointsY.Item1 != null && pointsY.Item2 != null)
            {
                var p1 = viewTP.TransformationMatrixToLocal.Transform(workTP.TransformationMatrixToGlobal.Transform(pointsY.Item1));
                var p2 = viewTP.TransformationMatrixToLocal.Transform(workTP.TransformationMatrixToGlobal.Transform(pointsY.Item2));

                var vector = new t3d.Vector(p2 - p1);

                var dimension2 = new StraightDimension(_viewBase, p1, p2, vector.Cross(new t3d.Vector(0, 0, 1)), L3 * _view.Attributes.Scale, dimensionsAttrbutes);
                dimension2.Insert();
            }

        }
    }

}
