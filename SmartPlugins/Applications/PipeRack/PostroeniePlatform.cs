using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace PipeRack
{
    class PostroeniePlatform
    {
        Model M = new Model(); // текущая модель
        List<Beam> Consoles = new List<Beam>();
        public double ConsoleH { get; set; }
        public double ConsoleL { get; set; }
        public double YklonMP { get; set; }
        public bool CheckRight { get; set; }
        public Postroenie StroikaVeka { get; set; }

        public PostroeniePlatform()
        {

        }
        public void Insert()
        {
            TransformationPlane currentPlane = M.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            Consoles.Clear();
            
            // ------------- построение консолей с подкосами
            for (int count = 0; count < StroikaVeka.FraMES.Count(); count++)
            {
                var PlatformMaintenance = new PlatformMaintenance(StroikaVeka.FraMES[count], M)
                {
                    consoleH = ConsoleH,
                    consoleL = ConsoleL,
                    checkRight = CheckRight,
                    yklonMP = YklonMP,
                };
                PlatformMaintenance.InsertConsole();
                Consoles.Add(PlatformMaintenance.Console);
            }
            // ------------- построение консолей с подкосами


            // ------------- построение продольных балок
            for (int count = 0; count < StroikaVeka.FraMES.Count() - 1; count++)
            {
                var FF = StroikaVeka.FraMES.Skip(count);
                List<Frame> FraMESS = new List<Frame>();
                foreach (Frame F in FF)
                    FraMESS.Add(F);
               
                var FFF = Consoles.Skip(count);
                List<Beam> Consoless = new List<Beam>();
                foreach (Beam F in FFF)
                    Consoless.Add(F);
                
                var PlatformMaintenance2 = new PlatformMaintenance(FraMESS, Consoless, M)
                {
                    consoleH = ConsoleH,
                    consoleL = ConsoleL,
                    checkRight = CheckRight,
                };
                PlatformMaintenance2.InsertBalkiPloshadki();
            }
            // ------------- построение продольных балок

            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
            M.CommitChanges();
        }
    }
}
