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

        public void Create_rama(double yarus_count, double h, double count_column, double razdv_1_2, double razdv_2_3)
        {
            Elements elements = new Elements();

            
            
            T3D.Point Start_point1 = new T3D.Point(0, 0, 0);                                       //точка низа 1 колонны
            T3D.Point End_point1 = new T3D.Point(0, 0, 0 +1000);                                   //точка верха  1 колонны 
            
            T3D.Point Start_point2 = new T3D.Point(0, 0 + razdv_1_2, 0);                           //точка низа 2 колонны
            T3D.Point End_point2 = new T3D.Point(0, 0 + razdv_1_2, 0 + yarus_count * h + 100);     //точка верха  2 колонны 

            T3D.Point Start_point3 = new T3D.Point(0, 0 + razdv_1_2+ razdv_2_3, 0);                           //точка низа 2 колонны
            T3D.Point End_point3 = new T3D.Point(0, 0 + razdv_1_2+ razdv_2_3, 0 + yarus_count * h + 100);     //точка верха  2 колонны 

            double L = End_point1.Z - Start_point1.Z;          // определяем высоту
            string prof_column = elements.Profile_column(L);  // определяем профиль

            if (count_column == 2)
            {

                Beam Column1 = elements.Beam_main(Start_point1, End_point1, prof_column);
                Beam Column2 = elements.Beam_main(Start_point2, End_point2, prof_column);
            }

            else if(count_column == 3)
            {
                Beam Column1 = elements.Beam_main(Start_point1, End_point1, prof_column);
                Beam Column2 = elements.Beam_main(Start_point2, End_point2, prof_column);
                Beam Column3 = elements.Beam_main(Start_point3, End_point3, prof_column);
            }
        }
    }
}