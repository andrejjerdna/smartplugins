using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace PipeRack
{
    class Connections
    {
        public void BeamsToColumn(Beam Column, List<Beam> _Travers)
        {
                for (int u = 0; u < _Travers.Count; u++)
                    BeamToColumn(Column, _Travers[u]);
        }
        public void BeamsToColumn(Beam Column, List<SuperProdolnayaBalka> _Travers)
        {

            for (int u = 0; u < _Travers.Count; u++)
                BeamToColumn(Column, _Travers[u]._beam);
        }
        public void BeamsToColumn(Beam Column, List<SuperTraversaYarysa> _Travers)
        {

            for (int u = 0; u < _Travers.Count; u++)
                BeamToColumn(Column, _Travers[u]._beam);
        }

        private Connection BeamToColumn(Beam PrimaryBeam, ModelObject SecondaryBeam)
        {
            Connection C2 = new Connection();
           // C2.Name = "x_Column_Web_Stiff_Clasp";
            C2.Name = NapravlenieDvytavra(PrimaryBeam);
            C2.Number = BaseComponent.PLUGIN_OBJECT_NUMBER;
            C2.SetPrimaryObject(PrimaryBeam);
            C2.SetSecondaryObject(SecondaryBeam);
          //  C2.LoadAttributesFromFile(1.ToString());
            C2.Insert();
            return C2;
        }

        private string NapravlenieDvytavra(Beam PrimaryBeam)
        {
            string rotation = PrimaryBeam.Position.Rotation.ToString();
            string profile_type = "";
            PrimaryBeam.GetReportProperty("PROFILE_TYPE", ref profile_type);

            if (profile_type == "I")
            {
                if (rotation == "BELOW" || rotation == "TOP")
                {
                    profile_type = "x_Column_EndPlate";
                }
                else if (rotation == "FRONT" || rotation == "BACK")
                {
                    profile_type = "x_Column_Web_Stiff_Clasp";
                }
            }


            return profile_type;
        }

    }
}
