using SmartObjects;
using SmartTeklaModel.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tekla.Structures;
using Tekla.Structures.Datatype;
using Tekla.Structures.Dialog;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;
using Point = Tekla.Structures.Geometry3d.Point;
using Vector = Tekla.Structures.Geometry3d.Vector;
using tdt = Tekla.Structures.Datatype;
using SmartTeklaModel;

namespace ConcreteFoundationPlugin
{
    [Plugin("sp_ConcreteFoundation")]
    [PluginUserInterface("ConcreteFoundationPlugin.PluginWindow")]

    public class ConcreteFoundation : ConcretePluginBase
    {
        private readonly Model _model;
        private PluginData _data;

        public double Delta; //Отступ от точки вставки по оси Z.

        public ConcreteFoundation(PluginData data)
        {
            _model = new Model();
            _data = data;

        }

        public override List<InputDefinition> DefineInput()
        {
            var objectList = new List<InputDefinition>();
            var picker = new Picker();

            var modelObject1 = picker.PickPoint("Pick point");

            objectList.Add(new InputDefinition(modelObject1));

            return objectList;
        }

        public override bool Run(List<InputDefinition> Input)
        {
            bool result = false;

            if (Input.Count != 1)
                return false;

            try
            {
                GetBaseValuesFromDialog(_data);
                GetLocalValuesFromDialog();

                var point = (Point)(Input[0]).GetInput();

                if (point == null)
                    return false;

                var currentTP = _model.GetWorkPlaneHandler().GetCurrentTransformationPlane();

                #region Определяем рабочую систему координат.
                var workCS = new CoordinateSystem(point, new Vector(1, 0, 0), new Vector(0, 1, 0));
                _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(workCS));
                workCS = new CoordinateSystem(new Point(0, 0, -Delta), new Vector(1, 0, 0), new Vector(0, 1, 0));
                _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(workCS));

                #endregion

                var concreteFoundation = new SmartObjects.ConcreteFoundation(_model)
                {
                    Class = Class,
                    Material = Material,
                    AssemblyPrefix = AssemblyPrefix,
                    PartPrefix = PartPrefix,
                    AssemblyStartNumber = AssemblyStartNumber,
                    PartStartNumber = PartStartNumber,
                    AssemblyName = AssemblyName,
                    PartName = PartName,
                    Finish = Finish,
                    PourPhase = PourPhase,
                    StepsZ = DistanceList1,
                    StepsX1 = DistanceList2,
                    StepsX2 = DistanceList3,
                    StepsY1 = DistanceList4,
                    StepsY2 = DistanceList5,
                };

                if (AddToCastUnit == 0)
                {
                    concreteFoundation.AddToCastUnit = ConcretePartBase.AssemblyTypeEnum.ADD_TO_CAST_UNIT;
                }
                else
                {
                    concreteFoundation.AddToCastUnit = ConcretePartBase.AssemblyTypeEnum.NOT_ADD_TO_CAST_UNIT;
                }

                    concreteFoundation.Insert();

                //Задаем дополнительные аттрибуты.
                /*
                GeneralMethods.SerUserAttribute(concreteFoundation.GetMainPart(), Attribute1, Attribute1_Value);
                GeneralMethods.SerUserAttribute(concreteFoundation.GetMainPart(), Attribute2, Attribute2_Value);
                GeneralMethods.SerUserAttribute(concreteFoundation.GetMainPart(), Attribute3, Attribute3_Value);

                GeneralMethods.SerUserAttribute(concreteFoundation.GetSecondaryParts(), Attribute1, Attribute1_Value);
                GeneralMethods.SerUserAttribute(concreteFoundation.GetSecondaryParts(), Attribute2, Attribute2_Value);
                GeneralMethods.SerUserAttribute(concreteFoundation.GetSecondaryParts(), Attribute3, Attribute3_Value);
                */
                result = true;

                _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            return result;
        }

        private void GetLocalValuesFromDialog()
        {
            L1 = _data.L1; if (IsDefaultValue(L1)) L1 = 400.0;
            L2 = _data.L2; if (IsDefaultValue(L2)) L2 = 400.0;
            L3 = _data.L3; if (IsDefaultValue(L3)) L3 = 400.0;
            L4 = _data.L4; if (IsDefaultValue(L4)) L4 = 400.0;
            L5 = _data.L5; if (IsDefaultValue(L5)) L5 = 400.0;
            L6 = _data.L6; if (IsDefaultValue(L6)) L6 = 400.0;

            Delta = _data.Delta; if (IsDefaultValue(Delta)) Delta = 0.0;

            Dist1 = _data.Dist1; if (IsDefaultValue(Dist1)) Dist1 = "1000 300 300 300";
            Dist2 = _data.Dist2; if (IsDefaultValue(Dist2)) Dist2 = "300 300 300";
            Dist3 = _data.Dist3; if (IsDefaultValue(Dist3)) Dist3 = "300 300 300";
            Dist4 = _data.Dist4; if (IsDefaultValue(Dist4)) Dist4 = "300 300 300";
            Dist5 = _data.Dist5; if (IsDefaultValue(Dist5)) Dist5 = "300 300 300";

            Dist2 = L1.ToString() + " " + Dist2;
            Dist3 = L2.ToString() + " " + Dist3;
            Dist4 = L3.ToString() + " " + Dist4;
            Dist5 = L4.ToString() + " " + Dist5;

            if (tdt.Distance.CurrentUnitType == tdt.Distance.UnitType.Millimeter)
            {
                DistanceList1 = BoltsMethods.GetDistanceList(Dist1);
                DistanceList2 = BoltsMethods.GetDistanceList(Dist2);
                DistanceList3 = BoltsMethods.GetDistanceList(Dist3);
                DistanceList4 = BoltsMethods.GetDistanceList(Dist4);
                DistanceList5 = BoltsMethods.GetDistanceList(Dist5);
            }


        }
    }
}
