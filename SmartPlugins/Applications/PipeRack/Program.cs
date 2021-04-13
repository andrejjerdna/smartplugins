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
    public class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public void Create_rama(double yarus_count,
                                double count_column,
                                double razdv_1_2,
                                double razdv_2_3,
                                List<double> Traversy,
                                List<string> Traversy_prof)
        {
            Elements elements = new Elements();




            //------------------ тут будут колонны
            int H = (int)yarus_count;


            T3D.Point C_Start_point1 = new T3D.Point(0, 0 + razdv_1_2, 0);                           //точка низа 1 колонны
            T3D.Point C_End_point1 = new T3D.Point(0, 0 + razdv_1_2, Traversy[H - 1] + 100);     //точка верха  1 колонны 

            T3D.Point C_Start_point2 = new T3D.Point(0, 0 - razdv_2_3, 0);                           //точка низа 2 колонны
            T3D.Point C_End_point2 = new T3D.Point(0, 0 - razdv_2_3, Traversy[H - 1] + 100);     //точка верха  2 колонны 

            T3D.Point C_Start_point3 = new T3D.Point(0, 0, 0);                                       //точка низа 3 колонны
            T3D.Point C_End_point3 = new T3D.Point(0, 0, Traversy[H - 1] + 100);                                   //точка верха  3 колонны 

            double L = C_End_point1.Z - C_Start_point1.Z;          // определяем высоту
            string prof_column = elements.Profile_column(L);  // определяем профиль

            if (count_column == 2)
            {
                Beam Column1 = elements.Beam_main(C_Start_point1, C_End_point1, prof_column);
                Beam Column2 = elements.Beam_main(C_Start_point2, C_End_point2, prof_column);


                for (int _count = 0; _count < yarus_count; _count++)
                {

                    T3D.Point B_1_start = new T3D.Point(C_Start_point1.X, C_Start_point1.Y, Traversy[_count]);
                    T3D.Point B_1_end = new T3D.Point(C_Start_point2.X, C_Start_point2.Y, Traversy[_count]);
                    Beam B_1 = elements.Beam_traversa(B_1_start, B_1_end, Traversy_prof[_count]);
                }

            }

            else if (count_column == 3)
            {
                Beam Column1 = elements.Beam_main(C_Start_point1, C_End_point1, prof_column);
                Beam Column2 = elements.Beam_main(C_Start_point2, C_End_point2, prof_column);
                Beam Column3 = elements.Beam_main(C_Start_point3, C_End_point3, prof_column);
            }


            //------------------ тут будут балки



        }
    }
}