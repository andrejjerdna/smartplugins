using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using SmartGeometry;
using System.Windows.Forms;

namespace PipeRack
{
    public class Frame
    {
        public List<Attributes> AttributeColumn { get; set; }
        public List<Attributes> Attributes { get; set; }
        public List<Attributes> Attributes2 { get; set; }

        public List<double> Traversy { get; set; }
        public List<double> Traversy2 { get; set; }
        public List<string> Profiles { get; set; }
        public List<double> UklonbI { get; set; }

        public double Razdv12 { get; set; }
        public double Razdv23 { get; set; }

        public Model _M;
        public int _yarusCount { get; set; }
        public int _count_column { get; set; }
        public string _nameOfPipeRack { get; set; }
        public TransformationPlane workTP { get; set; }

        public Point _basePoint { get; set; }



        public List<SuperColumn> _Columns = new List<SuperColumn>();
        public List<Beam> _Travers = new List<Beam>();
        public List<SuperTraversaYarysa> _TraversRight = new List<SuperTraversaYarysa>();
        public List<SuperTraversaYarysa> _TraversLeft = new List<SuperTraversaYarysa>();
        public List<double> Z { get; set; }

        public Point ColumnStartPoint1 { get; set; }
        public Point ColumnStartPoint2 { get; set; }
        public Point ColumnStartPoint3 { get; set; }
        public Point ColumnEndPoint1 { get; set; }
        public Point ColumnEndPoint2 { get; set; }
        public Point ColumnEndPoint3 { get; set; }

        public Frame(Model M, Point basePoint, int yarusCount, int count_column, string nameOfpipeRack)
        {
            _basePoint = basePoint;
            _yarusCount = yarusCount;
            _count_column = count_column;
            _M = M;
            _nameOfPipeRack = nameOfpipeRack;
        }
        public Frame()
        {
        }

        public void Insert()
        {
            var currentTP = _M.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            var workCS = new CoordinateSystem(_basePoint, new Vector(1, 0, 0), new Vector(0, 1, 0));
            workTP = new TransformationPlane(workCS);
            _M.GetWorkPlaneHandler().SetCurrentTransformationPlane(workTP);

            Connections Con = new Connections();

            Points();

            var Column1 = new SuperColumn(AttributeColumn[0], ColumnStartPoint1, ColumnEndPoint1);
            if (!Column1.Insert())
                return;    //если не создалать что-то надо сделать - продумать
            

            _Columns.Add(Column1);
           
            var Column2 = new SuperColumn(AttributeColumn[1], ColumnStartPoint2, ColumnEndPoint2);
            Column2.Insert();
            _Columns.Add(Column2);

            _TraversRight = ( CreateTraversy(Column1._beam, Column2._beam, Traversy, Attributes));

            Con.BeamsToColumn(Column1._beam, _TraversRight);
            Con.BeamsToColumn(Column2._beam, _TraversRight);


            if (_count_column == 3)
            {
                var Column3 = new SuperColumn(AttributeColumn[2], ColumnStartPoint3, ColumnEndPoint3);
                Column3.Insert();
                _Columns.Add(Column3);
                
                _TraversLeft =  CreateTraversy(Column2._beam, Column3._beam, Traversy2, Attributes);

                Con.BeamsToColumn(Column2._beam, _TraversLeft);
                Con.BeamsToColumn(Column3._beam, _TraversLeft);
            }
            _M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
            
        }

        public void Modify()
        {
            var workCS = new CoordinateSystem(_basePoint, new Vector(1, 0, 0), new Vector(0, 1, 0));
            workTP = new TransformationPlane(workCS);
            _M.GetWorkPlaneHandler().SetCurrentTransformationPlane(workTP);
            Points();

            foreach (SuperColumn col in _Columns)
            {
                col.Modify();
            }
            foreach (SuperTraversaYarysa travers in _TraversRight)
            {
                travers.Modify();
            }
            foreach (SuperTraversaYarysa travers in _TraversLeft)
            {
                travers.Modify();
            }

            //   ModifyColumn(_Columns[0], ColumnStartPoint1, ColumnEndPoint1);
            // ModifyColumn(_Columns[1], ColumnStartPoint2, ColumnEndPoint2);
            //  if(_count_column ==3)
            //  ModifyColumn(_Columns[2], ColumnStartPoint3, ColumnEndPoint3);
        }

     //   public void ModifyColumn(SuperColumn Col, Point start, Point end)
      //  {
      //      Col.StartPoint = start;
      //      Col.EndPoint = end;
      //      Col.Modify();
      //  }

        private List<SuperTraversaYarysa> CreateTraversy(Beam Column1, Beam Column2, List<double> Traversy, List<Attributes> Attributes)
        {
            List<SuperTraversaYarysa> beams = new List<SuperTraversaYarysa>();
            for (int _count = 0; _count < _yarusCount; _count++)
            {
                Point B_1_start = new Point(Column1.StartPoint.X, Column1.StartPoint.Y, Traversy[_count] + _basePoint.X * UklonbI[_count] * 0.001);
                Point B_1_end = new Point(Column2.StartPoint.X, Column2.StartPoint.Y, Traversy[_count] + _basePoint.X * UklonbI[_count] * 0.001);
                SuperTraversaYarysa traversa = new SuperTraversaYarysa(Attributes[_count], B_1_start, B_1_end);
                traversa.Insert();
                beams.Add(traversa);
            }
            return beams;
        }

        public Beam BeamMain(Attributes attributes, Point startPoint, Point endPoint, string RNumberOfYarus)
        {
            Beam newBeam = new Beam(startPoint, endPoint);
            newBeam.Profile.ProfileString = "I30K1_20_93";
            newBeam.Insert();
          
            newBeam.SetUserProperty("RNumberOfYarus", RNumberOfYarus);
            newBeam.Modify();
            return newBeam;
        } //удалить как пропадут ссылки
        public Beam BeamMain(Point startPoint, Point endPoint)
        {
            Beam newBeam = new Beam(startPoint, endPoint);
            newBeam.Profile.ProfileString = "I30K1_20_93";
            newBeam.Insert();
            return newBeam;
        }   //удалить как пропадут ссылки



        private void Points()
        {
            int H = _yarusCount;

            ColumnStartPoint1 = new Point(0, 0 - Razdv12, _basePoint.X * UklonbI[0] * 0.001);                           
            ColumnEndPoint1 = new Point(0, 0 - Razdv12, Traversy[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001);     

            if (_count_column == 2)
            {
                ColumnStartPoint2 = new Point(0, 0 + Razdv23, _basePoint.X * UklonbI[0] * 0.001);                                      
                ColumnEndPoint2 = new Point(0, 0 + Razdv23, Traversy[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001);         
            }
            if (_count_column == 3)
            {
                 ColumnStartPoint2 = new Point(0, 0, _basePoint.X * UklonbI[0] * 0.001);                         
                 ColumnEndPoint2 = new Point(0, 0, Traversy[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001); 

                if (Traversy2[H - 1] > Traversy[H - 1])
                    ColumnEndPoint2 = new Point(0, 0, Traversy2[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001);

                ColumnStartPoint3 = new Point(0, 0 + Razdv23, _basePoint.X * UklonbI[0] * 0.001);                                     
                ColumnEndPoint3 = new Point(0, 0 + Razdv23, Traversy2[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001);           
            }
        }
    }
}
