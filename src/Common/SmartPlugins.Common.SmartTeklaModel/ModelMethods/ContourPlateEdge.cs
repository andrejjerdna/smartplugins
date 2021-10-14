using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.SmartTeklaModel
{
    public class ContourPlateEdge
    {
        private Part MainPart;
        private double Height;
        private double Width;
        private double Z;

        public ContourPlateEdge(Part contourPlate, double height, double width, double z)
        {
            MainPart = contourPlate;
            Height = height;
            Width = width;
            Z = z;
        }
        public void Insert()
        {
            CreateEdge();
        }

        private void CreateEdge()
        {
            if (MainPart is ContourPlate slab)
            {
                var contour = slab.Contour.ContourPoints.OfType<ContourPoint>().ToList();

                for (int i = 0; i < contour.Count(); i++)
                {
                    var p1 = new Point(contour[i].X, contour[i].Y, Z);
                    var p2 = new Point();

                    if (i < contour.Count() - 1)
                    {
                        p2 = new Point(contour[i + 1].X, contour[i + 1].Y, Z);
                    }
                    if (i == contour.Count() - 1)
                    {
                        p2 = new Point(contour[0].X, contour[0].Y, Z);
                    }

                    var edge = new EdgeChamfer(p1, p2);
                    edge.Father = slab;
                    edge.FirstChamferEndType = EdgeChamfer.ChamferEndTypeEnum.FULL;
                    edge.Chamfer.X = Width;
                    edge.Chamfer.Y = Height;
                    edge.Insert();
                }
            }
        }
    }
}
