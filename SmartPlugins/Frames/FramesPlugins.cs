using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using tsm = Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;
using tdt = Tekla.Structures.Datatype;
using SmartTeklaModel;
using CSLib;
using System.Linq;
using SmartObjects.SteelParts;

namespace Frames
{
    public class FramesPluginsData
    {
        [StructuresField(nameof(L1))] public double L1;
        [StructuresField(nameof(Dist1))] public string Dist1;
        [StructuresField(nameof(Dist2))] public string Dist2;

        [StructuresField(nameof(Profile1))] public string Profile1;
    }

    [Plugin("sp_Frames")]
    [PluginUserInterface("Frames.FramesPluginsForm")]
    [SecondaryType(ConnectionBase.SecondaryType.SECONDARYTYPE_ONE)]
    [AutoDirectionType(AutoDirectionTypeEnum.AUTODIR_BASIC)]
    [PositionType(PositionTypeEnum.COLLISION_PLANE)]

    public class sp_ConcretePiles : PluginBase
    {
        #region Fields
        private readonly tsm.Model _model;
        private readonly FramesPluginsData _data;

        public double L1;
        public string Dist1;
        public string Dist2;

        public string Profile1;

        public IEnumerable<double> DistanceListX;
        public IEnumerable<double> DistanceListZ;

        #endregion

        #region Constructor
        public sp_ConcretePiles(FramesPluginsData data)
        {
            _model = new tsm.Model();
            _data = data;
        }
        #endregion

        #region Overriden methods
        public override List<InputDefinition> DefineInput()
        {
            var objectList = new List<InputDefinition>();
            var picker = new Picker();

            var point1 = picker.PickPoint("Pick point 1");
            var point2 = picker.PickPoint("Pick point 2");

            objectList.Add(new InputDefinition(point1));
            objectList.Add(new InputDefinition(point2));

            return objectList;
        }
        public override bool Run(List<InputDefinition> Input)
        {
            var result = false;

            var currentTP = _model.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            try
            {
                GetLocalValuesFromDialog();

                var point1 = (Point)(Input[0]).GetInput();
                var point2 = (Point)(Input[1]).GetInput();

                if (point1 == null || point2 == null)
                    return false;

                var frames = new Frame_type1(_model, point1, point2)
                {
                    DistanceListX = DistanceListX,
                    DistanceListZ = DistanceListZ,
                    DistanceListY = new List<double> { L1 },
                    ColumnProfile = Profile1
                };

                frames.Insert();

                _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
            }

            catch (Exception exc)
            {
                _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
                MessageBox.Show(exc.ToString());
            }

            return result;
        }
        #endregion


        private void GetLocalValuesFromDialog()
        {
            L1 = _data.L1; if (IsDefaultValue(L1)) L1 = 6000.0;

            Dist1 = _data.Dist1; if (IsDefaultValue(Dist1)) Dist1 = "0";
            Dist2 = _data.Dist2; if (IsDefaultValue(Dist2)) Dist2 = "5000";

            Profile1 = _data.Profile1; if (IsDefaultValue(Profile1)) Profile1 = "HEA400";

            if (tdt.Distance.CurrentUnitType == tdt.Distance.UnitType.Millimeter)
            {
                DistanceListX = BoltsMethods.GetDistanceList(Dist1);
                DistanceListZ = BoltsMethods.GetDistanceList(Dist2);
            }
        }
    }
}
