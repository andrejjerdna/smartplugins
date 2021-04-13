using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using T3D = Tekla.Structures.Geometry3d;

namespace PipeRack
{
    class Direction
    {

        Model M = new Model();

        public double X_start { get; set; }
        public double Y_start { get; set; }
        public double Z_start { get; set; }
        public double X_start2 { get; set; }
        public double Y_start2 { get; set; }
        public double Z_start2 { get; set; }

        public TransformationPlane Uzer_CS()
        {
            TransformationPlane currentPlane = M.GetWorkPlaneHandler().GetCurrentTransformationPlane(); //сохранили текущий вид
            return currentPlane;
        }

        
        private T3D.CoordinateSystem Coord()
        {
            T3D.Point CS_point = new T3D.Point(X_start, Y_start, Z_start);
            T3D.Vector vector_X = new T3D.Vector(X_start2 - X_start, 0, Z_start);
            T3D.Vector vector_Y = new T3D.Vector(Y_start2 - Y_start, 0, Z_start);
            T3D.CoordinateSystem CoordinateSystem = new T3D.CoordinateSystem(CS_point, vector_X, vector_Y);
            return CoordinateSystem;
        }

        public void Set_plan()
        {
           Uzer_CS();
           TransformationPlane zero_plane = new TransformationPlane();
           M.GetWorkPlaneHandler().SetCurrentTransformationPlane(zero_plane); //оси модели переставили по началу координад

            TransformationPlane localPlane = new TransformationPlane(Coord()); // переставили в координаты
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(localPlane);  //вид по выбранному направлению исходя из условия
        }

        public void Return_plan()
        {
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(Uzer_CS());  // вернули в пользовательский вид
        }
    }
}
