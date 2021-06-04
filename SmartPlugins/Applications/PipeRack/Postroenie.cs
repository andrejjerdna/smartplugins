using SmartGeometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using T3D = Tekla.Structures.Geometry3d;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Newtonsoft.Json;
using System.IO;

namespace PipeRack
{
    class Postroenie
    {
        public int yarus_count { get; set; }
        public int count_column { get; set; }
        public string nameOfpipeRack { get; set; }
        public IEnumerable<double> DistanceList { get; set; }
        public List<double> Traversy { get; set; }
        public List<double> Traversy2 { get; set; }
        public List<double> UklonbI { get; set; }
        public double razdv1to2 { get; set; }
        public double razdv2to3 { get; set; }
        public T3D.Point CS_point { get; set; }
        public T3D.Point CS_point_end { get; set; }
        public DataGridView dataGridViewYarusRight { get; set; }
        public DataGridView dataGridViewYarusLeft { get; set; }
        public DataGridView dataGridViewProdolnieRight { get; set; }
        public DataGridView dataGridViewProdolnieLeft { get; set; }
        public DataGridView dataGridColumn { get; set; }
        public DataGridView dataGridViewStoyki { get; set; }
        public DataGridView dataGridViewYarusRightVProlete { get; set; }
        public DataGridView dataGridViewYarusLeftVProlete { get; set; }
        public AttributesFrame AttFrame { get; set; }
        public AttributesFrameProlet AttFrameProlet { get; set; }
        public double startLabelY { get; set; }
        public string shagRam { get; set; }
        public string LabelX { get; set; }
        

        Model M = new Model(); // текущая модель
        public List<Frame> FraMES = new List<Frame>(); // список построенных рам
        public List<BalkiYarysa> BalkiYarysA = new List<BalkiYarysa>();
        

        public Postroenie()
        {

        }
        public void Insert()
        {
            CheckConnectionToModel(); //проверили подключение к модели
            FraMES.Clear();
            BalkiYarysA.Clear();

            // проверка наличия атрибутов
            if (!Nali4ieAtt(AttFrame.AttributesColumn, count_column, "колонны")) return;
            if (!Nali4ieAtt(AttFrame.AttributesYarusRight, yarus_count, "яруса")) return;

            if (count_column == 3)
            {
                if (!Nali4ieAtt(AttFrame.AttributesYarusLeft, yarus_count, "левого ряда яруса")) return;
            }

            TransformationPlane currentPlane = M.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            TransformationPlane zero_plane = new TransformationPlane();
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(zero_plane);
            var TP = SmartTransfomationPlane.GetTransformationPlaneTwoPoints(M, CS_point, CS_point_end);
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(TP);

            //----------------------------------------------построение рам
            var tempX = 0.0;
            foreach (var x in DistanceList)
            {
                tempX += x;
                var point = new T3D.Point(tempX, 0, 0);
                var frame = new Frame(M, point, yarus_count, count_column, nameOfpipeRack)
                {
                    Razdv12 = razdv1to2,
                    Razdv23 = razdv2to3,
                    Traversy = Traversy,
                    Traversy2 = Traversy2,
                    Attributes = AttFrame.AttributesYarusRight,
                    Attributes2 = AttFrame.AttributesYarusLeft,
                    AttributeColumn = AttFrame.AttributesColumn,
                    UklonbI = UklonbI,
                    _M = M,
                };
                frame.Insert();             // построились рамы (колонны и траверсы)
                FraMES.Add(frame);
            }
            UserAttsFrame(FraMES, nameOfpipeRack);
            //---------------------------------------------построение рам


            //-------------------построение продольных балок
            for (int count = 0; count < FraMES.Count() - 1; count++)  //выбрал пару рам между которыми буду строить продольные балки
            {
                var Frame1 = FraMES[count];
                var Frame2 = FraMES[count + 1];

                var balkiYarusa = new BalkiYarysa(Frame1, Frame2, AttFrameProlet);
                balkiYarusa.Insert();
                BalkiYarysA.Add(balkiYarusa);
            }
            UserAttsBalkiYarysa(BalkiYarysA, nameOfpipeRack);
            //-------------------построение продольных балок

            //-------------------построение сетки
            string Y = null, FFF = null, _startLabelY = null;
            

            if (count_column == 2)
                Y = (-1 * razdv1to2).ToString() + " " + (razdv1to2 + razdv2to3).ToString();
            else
                Y = (-1 * razdv1to2).ToString() + " " + razdv1to2.ToString() + " " + (razdv2to3).ToString();

            for (int _count1 = 0; _count1 < yarus_count; _count1++)
                FFF = FFF + " " + Traversy[_count1].ToString();

            for (int _count1 = 0; _count1 < FraMES.Count(); _count1++)
                _startLabelY += (startLabelY + _count1).ToString() + " ";

            Grid Grid = new Grid()
            {
                CoordinateX = shagRam,
                CoordinateY = Y,
                CoordinateZ = "0 " + FFF,
                LabelX = _startLabelY,
                LabelY = LabelX,
                LabelZ = "0 " + FFF,
                ExtensionLeftX = 2000.0,
                ExtensionLeftY = 2000.0,
                ExtensionLeftZ = 2000.0,
                ExtensionRightX = 2000.0,
                ExtensionRightY = 2000.0,
                ExtensionRightZ = 2000.0,
                IsMagnetic = false,
            };
            Grid.Insert();
            Grid.Origin.Z = CS_point.Z;
            Grid.Modify();
            //-------------------построение сетки

            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
            M.CommitChanges();


            var jsonString = JsonConvert.SerializeObject(FraMES);
            var path = M.GetInfo().ModelPath + "\\frames.json";
            var dirInfo = new DirectoryInfo(M.GetInfo().ModelPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            using (var file = new StreamWriter(path, false))
            {
                file.WriteLine(jsonString);
            }
        }

        private void CheckConnectionToModel()
        {
            if (!M.GetConnectionStatus())
            {
                MessageBox.Show("Не подключено к моделе");
                return;
            }
        }
        private bool Nali4ieAtt(List<Attributes> Att, int yarus_count, string Mesaga)
        {
            for (int _count = 1; _count <= yarus_count; _count++)
            {
                if (Att[_count - 1] == null)
                {
                    MessageBox.Show("Введите атрибуты для " + Mesaga.ToString() + " " + (_count).ToString());
                    return false;
                }
            }
            return true;
        }
        private void UserAttsFrame(List<Frame> FraMES, string RNazvanie)
        {
            for (int i = 0; i < FraMES.Count(); i++)
            {
                foreach (SuperColumn beam in FraMES[i]._Columns)
                {
                    beam._beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam._beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                }
                foreach (Beam beam in FraMES[i]._TraversRight)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Траверсы яруса");
                    beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                    beam.SetUserProperty("DirectionOfYarus", "Right");
                }
                foreach (Beam beam in FraMES[i]._TraversLeft)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Траверсы яруса");
                    beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                    beam.SetUserProperty("DirectionOfYarus", "Left");
                }
            }
        }
        private void UserAttsBalkiYarysa(List<BalkiYarysa> BalkiYarysA, string RNazvanie)
        {
            for (int i = 0; i < BalkiYarysA.Count(); i++)
            {
                foreach (Beam beam in BalkiYarysA[i]._balki)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Продольные балки");
                    beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                    beam.SetUserProperty("DirectionOfYarus", "Right");
                }
                foreach (Beam beam in BalkiYarysA[i]._balkiLeft)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Продольные балки");
                    beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                    beam.SetUserProperty("DirectionOfYarus", "Left");
                }
                foreach (Beam beam in BalkiYarysA[i]._stoiki)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Стойки");
                    beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                    beam.SetUserProperty("DirectionOfYarus", "Center");
                }
                foreach (Beam beam in BalkiYarysA[i]._traversyvprovete)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Траверсы в пролете");
                    beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                    beam.SetUserProperty("DirectionOfYarus", "Right");
                }
                foreach (Beam beam in BalkiYarysA[i]._traversyvproveteLeft)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Траверсы в пролете");
                    beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                    beam.SetUserProperty("DirectionOfYarus", "Left");
                }
            }
        }
    }
}
