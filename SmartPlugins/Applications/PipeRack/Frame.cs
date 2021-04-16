using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using SmartGeometry;

namespace PipeRack
{
    public class Frame
    {
        public List<double> Traversy { get; set; }
        public List<string> Profiles { get; set; }

        public double Razdv_1_2 { get; set; }
        public double Razdv_2_3 { get; set; }
        public List<Attributes> Attributes { get; set; }

        private int _yarusCount;

        public List<Beam> _Columns = new List<Beam>();
        public List<Beam> _Travers = new List<Beam>();

        public Frame(int yarusCount)
        {
            _yarusCount = yarusCount;
        }

        private Beam Beam_main(Point startPoint, Point endPoint)
        {
            Beam newBeam = new Beam(startPoint, endPoint);
            newBeam.Profile.ProfileString = "I30K1_20_93";
            newBeam.Insert();
            return newBeam;
        }

        public void CreateRamaDveKolony(double G)
        {
            int H = _yarusCount;

            Point C_Start_point1 = new Point(G, 0 + Razdv_1_2, 0);                           //точка низа 1 колонны
            Point C_End_point1 = new Point(G, 0 + Razdv_1_2, Traversy[H - 1] + 100);     //точка верха  1 колонны 

            Point C_Start_point2 = new Point(G, 0 - Razdv_2_3, 0);                           //точка низа 2 колонны
            Point C_End_point2 = new Point(G, 0 - Razdv_2_3, Traversy[H - 1] + 100);     //точка верха  2 колонны 

            _Columns.Add(Beam_main(C_Start_point1, C_End_point1));
            _Columns.Add(Beam_main(C_Start_point2, C_End_point2));
            
            


            for (int _count = 0; _count < _yarusCount; _count++)
            {
                Point B_1_start = new Point(C_Start_point1.X, C_Start_point1.Y, Traversy[_count]);
                Point B_1_end = new Point(C_Start_point2.X, C_Start_point2.Y, Traversy[_count]);
                _Travers.Add(Beam_main(B_1_start, B_1_end));
                if(Attributes[0] != null)
                    SetAtt(_Travers[0], Attributes[0]);
            }
        }

        /*   public void CreateRamaTriKolony()
           {
               int H = _yarusCount;

               Point C_Start_point1 = new Point(0, 0 + Razdv_1_2, 0);                           //точка низа 1 колонны
               Point C_End_point1 = new Point(0, 0 + Razdv_1_2, Traversy[H - 1] + 100);     //точка верха  1 колонны 

               Point C_Start_point2 = new Point(0, 0 - Razdv_2_3, 0);                           //точка низа 2 колонны
               Point C_End_point2 = new Point(0, 0 - Razdv_2_3, Traversy[H - 1] + 100);     //точка верха  2 колонны 

               Point C_Start_point3 = new Point(0, 0, 0);                                       //точка низа 3 колонны
               Point C_End_point3 = new Point(0, 0, Traversy[H - 1] + 100);                                   //точка верха  3 колонны 

               Beam Column1 = new Beam(C_Start_point1, C_End_point1);
               _Columns.Add(Column1);

               Beam Column2 = new Beam(C_Start_point2, C_End_point2);
               _Columns.Add(Column2);

               Beam Column3 = new Beam(C_Start_point3, C_End_point3);
               _Columns.Add(Column2);

               for (int _count = 0; _count < _yarusCount; _count++)
               {
                   Point B_1_start = new Point(C_Start_point1.X, C_Start_point1.Y, Traversy[_count]);
                   Point B_1_end = new Point(C_Start_point2.X, C_Start_point2.Y, Traversy[_count]);
                   Beam B_1 = new Beam(B_1_start, B_1_end);
                   _Travers.Add(B_1);

               }
           }*/
        public void SetAtt(Beam beam, Attributes _attributes)
        {
            beam.Name = _attributes.Name;
            beam.Profile.ProfileString = _attributes.Profile;
            beam.Material.MaterialString = _attributes.Material;
            beam.Class = _attributes.Class;
             beam.PartNumber.Prefix = _attributes.PrefixSborki;
            int nomerSborki;
            var H = Int32.TryParse(_attributes.NomerSborki.ToString(), out nomerSborki);
            beam.PartNumber.StartNumber = nomerSborki;

            //LEFT MIDDLE RIGHT
            if (_attributes.PolojenieGorizontalno == "Слева") beam.Position.Plane = Position.PlaneEnum.LEFT;
            if (_attributes.PolojenieGorizontalno == "Центр") beam.Position.Plane = Position.PlaneEnum.MIDDLE;
            if (_attributes.PolojenieGorizontalno == "Справа") beam.Position.Plane = Position.PlaneEnum.RIGHT;
            // beam.Position.Rotation = Position.RotationEnum(_attributes.PolojeniePovorot.ToString()); // BACK BELOW FRONT TOP

            //  var BEHIND1 = _attributes.PolojenieVertikalno.ToString();
            // beam.Position.Depth = _attributes.PolojenieVertikalno; //BEHIND=2 FRONT MIDLE
            beam.Modify(); 
        }
    }
}
