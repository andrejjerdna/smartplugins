using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using T3D = Tekla.Structures.Geometry3d;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

namespace PipeRack
{
    public class Elements
    {
        public Beam Beam_main(T3D.Point startPoint, T3D.Point endPoint, string Profile)  //горизонтальный одинарный уголок
        {
            Beam newBeam = new Beam(startPoint, endPoint);
            newBeam.Profile.ProfileString = Profile;
            newBeam.Material.MaterialString = "C345";
            newBeam.Position.Plane = Position.PlaneEnum.MIDDLE;
          ///  newBeam.Position.PlaneOffset = 4;
            newBeam.Position.Depth = Position.DepthEnum.MIDDLE;
          //  newBeam.Position.Rotation = Position.RotationEnum.BACK;
             newBeam.Class = "2";
          //  newBeam.AssemblyNumber.Prefix = "СВ";
            newBeam.Insert();
           // newBeam9.StartPointOffset.Dx = +100;
           // newBeam9.EndPointOffset.Dx = -100;
            newBeam.Modify();
            return newBeam;
        }
        public Beam Beam_traversa(T3D.Point startPoint, T3D.Point endPoint, string Profile)  //горизонтальный одинарный уголок
        {
            Beam newBeam = new Beam(startPoint, endPoint);
            newBeam.Profile.ProfileString = Profile;
            newBeam.Material.MaterialString = "C345";
            //  newBeam.Position.Plane = Position.PlaneEnum.RIGHT;
            //  newBeam.Position.PlaneOffset = 4;
            //  newBeam.Position.Depth = Position.DepthEnum.MIDDLE;
            //  newBeam.Position.Rotation = Position.RotationEnum.BACK;
            //  newBeam.Class = "6";
            //  newBeam.AssemblyNumber.Prefix = "СВ";
            newBeam.Insert();
            // newBeam9.StartPointOffset.Dx = +100;
            // newBeam9.EndPointOffset.Dx = -100;
            //newBeam.Modify();
            return newBeam;
        }

        public string Profile_column(double L)
        {
            string Profile_column = "I30K1_20_93";
            double L1 = L * 1;
           /* if (L > 7400) MessageBox.Show("Слишком длинный пролет. Ставь сразу 40Ш1 где горизонтальный профиль");
            else if (L >= 6400 && L <= 7400) prof_gor = "PK160X6.0_30245_2003"; //распорки
            else if (L >= 5235 && L < 6400) prof_gor = "PK140X6.0_30245_2003";

            else if (L >= 4350 && L < 5235) prof_gor = "L90X7_8509_93";   // спаренные
            else if (L >= 2670 && L < 4350) prof_gor = "L75X6_8509_93";

            else if (L >= 2220 && L < 2670) prof_gor = "L90X7_8509_93";   //одинарные
            else if (L >= 1875 && L < 2220) prof_gor = "L75X6_8509_93";
            else if (L >= 0 && L < 1875) prof_gor = "L63X5_8509_93";*/

            return Profile_column;
        }


    }
}
