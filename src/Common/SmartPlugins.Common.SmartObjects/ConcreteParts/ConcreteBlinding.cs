using SmartPlugins.Common.SmartGeometry;
using SmartPlugins.Common.TeklaStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using tsm = Tekla.Structures.Model;

namespace SmartPlugins.Common.SmartObjects
{
    public class ConcreteBlinding : ConcretePartBase, IConcreteBase
    {
        public enum EdgeTypeEnum
        {
            YES,
            NO
        }
        public enum CopyBooleanTypeEnum
        {
            YES,
            NO
        }
        /// <summary>
        /// Класс свай.
        /// </summary>
        public string ClassPile;
        public EdgeTypeEnum EdgeType { get; set; }

        public CopyBooleanTypeEnum CopyBooleanType;
        public ConcreteBlinding(tsm.Model currentModel, Part foundation, int edgeTypeUser, double equidistant, double thickness, string classPile)
        {
            _currentModel = currentModel;
            _foundation = foundation;
            _edgeTypeUser = edgeTypeUser;
            _offcet = equidistant;
            _thickness = thickness;
            ClassPile = classPile;
            AddToCastUnit = AssemblyTypeEnum.NOT_ADD_TO_CAST_UNIT;
            EdgeType = EdgeTypeEnum.NO;
            CopyBooleanType = CopyBooleanTypeEnum.NO;
        }
        /// <summary>
        /// Фундамент.
        /// </summary>
        private Part _foundation { get; set; }
        /// <summary>
        /// Выступ за грань.
        /// </summary>
        private double _offcet;
        /// <summary>
        /// Толщина подготовки.
        /// </summary>
        private double _thickness;
        private int _edgeTypeUser;
        public void Insert()
        {
            _mainPart = InsertPlate();

            if (EdgeType == EdgeTypeEnum.YES)
            {
                CreateEdge();
            }

            if (AddToCastUnit == AssemblyTypeEnum.ADD_TO_CAST_UNIT)
            {
                AddToAssembly(_foundation);
            }
            else
            {
                DeleteToAssembly(_foundation);
            }
            DeleteBoolean();

            CopyBoolean();
            CreateBooleanPile();
        }
        private Part InsertPlate()
        {
            var polygon = IntersectWithSolid.PolygonEquidistant(_foundation, _offcet);

            var groutUnderBasePlate = new ContourPlate();
            groutUnderBasePlate.Class = Class;
            groutUnderBasePlate.Material.MaterialString = Material;
            groutUnderBasePlate.Profile.ProfileString = GlobalParameters.BaseProfilePlate + _thickness.ToString();
            groutUnderBasePlate.AssemblyNumber.Prefix = AssemblyPrefix;
            groutUnderBasePlate.AssemblyNumber.StartNumber = AssemblyStartNumber;
            groutUnderBasePlate.PartNumber.Prefix = PartPrefix;
            groutUnderBasePlate.PartNumber.StartNumber = PartStartNumber;
            groutUnderBasePlate.Name = PartName;
            groutUnderBasePlate.Finish = Finish;
            groutUnderBasePlate.PourPhase = PourPhase;
            groutUnderBasePlate.Position.Plane = Position.PlaneEnum.RIGHT;
            groutUnderBasePlate.Position.Depth = Position.DepthEnum.MIDDLE;
            groutUnderBasePlate.CastUnitType = Part.CastUnitTypeEnum.CAST_IN_PLACE;

            foreach (Point p in polygon.Points)
            {
                p.Z = _foundation.GetSolid().MinimumPoint.Z - _thickness / 2;
                groutUnderBasePlate.AddContourPoint(new ContourPoint(p, null));
            }

            groutUnderBasePlate.Insert();

            return groutUnderBasePlate;
        }
        private void CreateEdge()
        {
            var mainPartGeometry = new ModelObjectGeometry(_mainPart);

            if (_edgeTypeUser == 1)
            {
                var edge = new ContourPlateEdge(_mainPart, _offcet, _thickness, mainPartGeometry.MinZ);
                edge.Insert();
            }
            if (_edgeTypeUser == 2)
            {
                var edge = new ContourPlateEdge(_mainPart, _thickness, _offcet, mainPartGeometry.MaxZ);
                edge.Insert();
            }

        }
        private string CreateFilterByClass()
        {
            var sysPath = _currentModel.GetInfo().ModelPath + "\\attributes" + "\\smplugins_tempFilter.SObjGrp";

            var createFilter = new StreamWriter(sysPath, false, Encoding.Default);

            createFilter.WriteLine("TITLE_OBJECT_GROUP");
            createFilter.WriteLine("{");
            createFilter.WriteLine("    Version= 1.05");
            createFilter.WriteLine("    Count= 2 ");
            createFilter.WriteLine("    SECTION_OBJECT_GROUP ");
            createFilter.WriteLine("    {");
            createFilter.WriteLine("        0");
            createFilter.WriteLine("        1");
            createFilter.WriteLine("        co_part");
            createFilter.WriteLine("        proCLASS");
            createFilter.WriteLine("        albl_Class");
            createFilter.WriteLine("        == ");
            createFilter.WriteLine("        albl_Equals ");
            createFilter.WriteLine("        " + ClassPile);
            createFilter.WriteLine("        0 ");
            createFilter.WriteLine("        && ");
            createFilter.WriteLine("        }");
            createFilter.WriteLine("    SECTION_OBJECT_GROUP ");
            createFilter.WriteLine("    {");
            createFilter.WriteLine("        0 ");
            createFilter.WriteLine("        1 ");
            createFilter.WriteLine("        co_object ");
            createFilter.WriteLine("        proOBJECT_TYPE ");
            createFilter.WriteLine("        albl_ObjectType ");
            createFilter.WriteLine("        == ");
            createFilter.WriteLine("        albl_Equals ");
            createFilter.WriteLine("        albl_Part ");
            createFilter.WriteLine("        0 ");
            createFilter.WriteLine("        && ");
            createFilter.WriteLine("        }");
            createFilter.WriteLine("    }");
            createFilter.Close();

            return "smplugins_tempFilter.SObjGrp";
        }
        /// <summary>
        /// Метод получает выборку элементов из модели и при пересечении элемента с бетонной подготовкой создает тело выреза в бетонной подготовке.
        /// Элемент модели должен быть Beam.
        /// </summary>
        /// <param name="parts">Набор элементов.</param>
        public void CreateBooleanPile()
        {
            var parts = _currentModel.GetModelObjectSelector().GetObjectsByFilterName(CreateFilterByClass());

            if (ClassPile != "")
            {
                var solidBlinding = _mainPart.GetSolid();

                while (parts.MoveNext())
                {
                    if (parts.Current is Beam pile)
                    {
                        CreatePartBoolean(solidBlinding, pile);
                    }
                }
            }
        }
        /// <summary>
        /// Создаем тело выреза.
        /// </summary>
        /// <param name="blindingOBB"></param>
        /// <param name="pile"></param>
        private void CreatePartBoolean(Solid solidBlinding, Beam pile)
        {
            var line = new LineSegment(pile.StartPoint, pile.EndPoint);

            if (solidBlinding.Intersect(line).Count == 0) return;

            //Вырезаем тело сваи из бетонной подготовки.
            var partBoolean = new Beam(pile.StartPoint, pile.EndPoint);
            partBoolean.Profile.ProfileString = pile.Profile.ProfileString;
            partBoolean.Material.MaterialString = pile.Material.MaterialString;
            partBoolean.Position = pile.Position;
            partBoolean.PartNumber.Prefix = "B1";
            partBoolean.Class = BooleanPart.BooleanOperativeClassName;
            partBoolean.Insert();

            var booleanPart = new BooleanPart();
            booleanPart.Father = _mainPart;
            booleanPart.SetOperativePart(partBoolean);
            booleanPart.Insert();

            partBoolean.Delete();
        }
        /// <summary>
        /// Копирование антител между деталями.
        /// </summary>
        private void CopyBoolean()
        {
            if (CopyBooleanType == CopyBooleanTypeEnum.NO) return;
            
            var booleans = _foundation.GetBooleans();

            while (booleans.MoveNext())
            {
                var current = booleans.Current;

                if (current is BooleanPart booleanPart)
                {
                    CreateBoolean(booleanPart.OperativePart);
                }
            }
        }
        private void CreateBoolean(Part operativePart)
        {
            if (operativePart is ContourPlate contourPlate)
            {
                var boolean = new ContourPlate();
                boolean.Contour = contourPlate.Contour;
                boolean.PartNumber.Prefix = "B1";
                boolean.Profile = contourPlate.Profile;
                boolean.Position = contourPlate.Position;
                boolean.Class = BooleanPart.BooleanOperativeClassName;
                boolean.Insert();

                BooleanPart booleanPart = new BooleanPart();
                booleanPart.Father = _mainPart;
                booleanPart.SetOperativePart(boolean);
                booleanPart.Insert();

                boolean.Delete();
            }

            if (operativePart is Beam beam)
            {
                var boolean = new Beam();
                boolean.PartNumber.Prefix = "B1";
                boolean.StartPoint = beam.StartPoint;
                boolean.EndPoint = beam.EndPoint;
                boolean.Profile = beam.Profile;
                boolean.Position = beam.Position;
                boolean.Class = BooleanPart.BooleanOperativeClassName;
                boolean.Insert();

                BooleanPart booleanPart = new BooleanPart();
                booleanPart.Father = _mainPart;
                booleanPart.SetOperativePart(boolean);
                booleanPart.Insert();

                boolean.Delete();
            }

            if (operativePart is PolyBeam polyBeam)
            {
                var boolean = new PolyBeam();
                boolean.PartNumber.Prefix = "B1";
                boolean.Contour = polyBeam.Contour;
                boolean.Profile = polyBeam.Profile;
                boolean.Position = polyBeam.Position;
                boolean.Class = BooleanPart.BooleanOperativeClassName;
                boolean.Insert();

                BooleanPart booleanPart = new BooleanPart();
                booleanPart.Father = _mainPart;
                booleanPart.SetOperativePart(boolean);
                booleanPart.Insert();

                boolean.Delete();
            }
        }
        private void DeleteBoolean()
        {
            var booleans = _mainPart.GetBooleans();

            while (booleans.MoveNext())
            {
                var current = booleans.Current;

                if (current is BooleanPart booleanPart)
                {
                    if (booleanPart.OperativePart is Part part)
                    {
                        if(part.PartNumber.Prefix == "B1")
                        {
                            current.Delete();
                        }
                    }
                }
            }
        }
    }
}
