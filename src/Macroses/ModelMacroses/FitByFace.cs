using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Solid;

namespace SmartMacros
{
    public class FitByFace
    {
        List<Point> Points = new List<Point>();
        public void Run()
        {

        }

        public void InsertFitting()
        {
            try
            {
                var model = new Model();
                if (model.GetConnectionStatus() == false) return;

                var currentTP = model.GetWorkPlaneHandler().GetCurrentTransformationPlane();

                var pickFace = new Picker().PickFace("Pick face.");
                var parts = new Picker().PickObjects(Picker.PickObjectsEnum.PICK_N_PARTS);

                var myEnum = pickFace.GetEnumerator();
                while (myEnum.MoveNext())
                {
                    var item = myEnum.Current as InputItem;
                    if (item.GetInputType() == InputItem.InputTypeEnum.INPUT_POLYGON)
                    {
                        Points = (item.GetData() as ArrayList).OfType<Point>().ToList();
                    }
                }

                if (Points.Count > 2)
                {
                    var origin = Points[0];
                    var vectorX = new Vector(Points[0] - Points[1]);
                    var vectorY = new Vector(Points[0] - Points[2]);

                    var workCS = new CoordinateSystem(origin, vectorX, vectorY);
                    var workPlane = new TransformationPlane(workCS);

                    model.GetWorkPlaneHandler().SetCurrentTransformationPlane(workPlane);

                    while (parts.MoveNext())
                    {
                        if (parts.Current is Beam beam)
                        {
                            var fitting = new Fitting();
                            fitting.Father = beam;
                            fitting.Plane.AxisX = vectorX;
                            fitting.Plane.AxisY = vectorY;
                            fitting.Insert();
                        }
                    }
                }

                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
            }
            catch
            {

            }
        }
    }
}
