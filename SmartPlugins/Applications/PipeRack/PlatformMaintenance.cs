using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace PipeRack
{
    class PlatformMaintenance
    {
        Frame _FraMES;
        List<Frame> _FraMESS;
        public Beam Console { get; set; }
        Beam startColumn;
        List<Beam> Consoless;
        public double consoleH { get; set; }
        public double yklonMP { get; set; }
        public double consoleL { get; set; }
        public bool checkRight { get; set; }

        public Model _M { get; set; }

        public PlatformMaintenance(Frame FraMES, Model M)
        {
            _FraMES = FraMES;
            _M = M;
        }
        public PlatformMaintenance(List<Frame> _FraMESS, List<Beam> Consoless, Model M)
        {
            this.Consoless = Consoless;
            _M = M;
            this._FraMESS = _FraMESS;
        }

        public void InsertConsole()
        {
            _M.GetWorkPlaneHandler().SetCurrentTransformationPlane(_FraMES.workTP);
            Frame frame = new Frame();
            int napravlenie = 1;
            if (!checkRight)  startColumn = _FraMES._Columns.First(); //первая колонна
             else
            {
                startColumn = _FraMES._Columns.Last(); //последняя колонна
                napravlenie = -1;
            }
            
            var startPoint = new Point(startColumn.StartPoint);
            startPoint.Z = consoleH+ yklonMP *0.001* _FraMES._basePoint.X;

            var endPoint = new Point(startPoint);
            startPoint.Y -= consoleL* napravlenie;

            Console = frame.Beam_main(startPoint, endPoint);

            Podcos(startColumn, Console);
        }

        public void InsertBalkiPloshadki()
        {
            _M.GetWorkPlaneHandler().SetCurrentTransformationPlane(_FraMESS[0].workTP);
            Frame frame = new Frame();

            // var _shagi = (FraMESS[1]._basePoint.X - FraMESS[0]._basePoint.X);

            var startPoint = new Point(Consoless[0].StartPoint.X, Consoless[0].StartPoint.Y, Consoless[0].StartPoint.Z);
            var endPoint = new Point(Consoless[0].StartPoint.X, Consoless[0].EndPoint.Y, Consoless[0].EndPoint.Z);

            var startPoint2 = new Point(_FraMESS[1]._basePoint.X- _FraMESS[0]._basePoint.X, Consoless[1].StartPoint.Y, Consoless[1].StartPoint.Z);
            var endPoint2 = new Point(_FraMESS[1]._basePoint.X- _FraMESS[0]._basePoint.X, Consoless[1].EndPoint.Y, Consoless[1].EndPoint.Z);

                frame.Beam_main(startPoint, startPoint2); // левая балка
                frame.Beam_main(endPoint, endPoint2);     // правая балка  
            BalkiYarysa Balki = new BalkiYarysa();

            List<double> _shagi = new List<double>();

            _shagi = Balki.Shagtravers(_FraMESS[1]._basePoint.X - _FraMESS[0]._basePoint.X, 1000);
            for (int _i = 0; _i < _shagi.Count(); _i++)
            {
                Point startPoint3 = new Point(startPoint.X + _shagi[_i], startPoint.Y, startPoint.Z  );
                Point endPoint3 = new Point(endPoint.X + _shagi[_i], endPoint.Y, endPoint.Z  );
                frame.Beam_main( startPoint3, endPoint3); 
            }
        }

        public Connection Podcos (ModelObject PrimaryBeam, ModelObject SecondaryBeam)
        {
            Connection C2 = new Connection();
            C2.Name = "Подкос";
            C2.Number = BaseComponent.CUSTOM_OBJECT_NUMBER;
            C2.SetPrimaryObject(PrimaryBeam);
            C2.SetSecondaryObject(SecondaryBeam);
            C2.Insert();
            return C2;
        }

    }
}
