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
        public List<double> Traversy { get; set;}
        public List<string> Profiles { get; set; }

        public double Razdv_1_2 { get; set; }
        public double Razdv_2_3 { get; set; }

        private int _yarusCount;
        private double _countColumns;

        public Frame(int yarusCount, double countColumns)
        {
            _yarusCount = yarusCount;
            _countColumns = countColumns;
        }

        public void Create_rama()
        {
            Elements elements = new Elements();
            //список колон
            //список балок
            int H = _yarusCount;

            Point C_Start_point1 = new Point(0, 0 + Razdv_1_2, 0);                           //точка низа 1 колонны
            Point C_End_point1 = new Point(0, 0 + Razdv_1_2, Traversy[H - 1] + 100);     //точка верха  1 колонны 

            Point C_Start_point2 = new Point(0, 0 - Razdv_2_3, 0);                           //точка низа 2 колонны
            Point C_End_point2 = new Point(0, 0 - Razdv_2_3, Traversy[H - 1] + 100);     //точка верха  2 колонны 

            Point C_Start_point3 = new Point(0, 0, 0);                                       //точка низа 3 колонны
            Point C_End_point3 = new Point(0, 0, Traversy[H - 1] + 100);                                   //точка верха  3 колонны 

            double L = C_End_point1.Z - C_Start_point1.Z;          // определяем высоту
            string prof_column = elements.Profile_column(L);  // определяем профиль

            if (_countColumns == 2)
            {
                Beam Column1 = elements.Beam_main(C_Start_point1, C_End_point1, prof_column);
                Beam Column2 = elements.Beam_main(C_Start_point2, C_End_point2, prof_column);

                for (int _count = 0; _count < _yarusCount; _count++)
                {
                    Point B_1_start = new Point(C_Start_point1.X, C_Start_point1.Y, Traversy[_count]);
                    Point B_1_end = new Point(C_Start_point2.X, C_Start_point2.Y, Traversy[_count]);
                    Beam B_1 = elements.Beam_traversa(B_1_start, B_1_end, Profiles[_count]);
                }
            }

            else if (_countColumns == 3)
            {
                Beam Column1 = elements.Beam_main(C_Start_point1, C_End_point1, prof_column);
                Beam Column2 = elements.Beam_main(C_Start_point2, C_End_point2, prof_column);
                Beam Column3 = elements.Beam_main(C_Start_point3, C_End_point3, prof_column);
            }
        }
    }
}
