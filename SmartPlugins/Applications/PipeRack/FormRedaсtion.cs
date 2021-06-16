using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using SmartExtensions;
using System.IO;
using SmartGeometry;
using SmartTeklaModel;
using T3D = Tekla.Structures.Geometry3d;
using Newtonsoft.Json;

namespace PipeRack
{
    public partial class FormRedaсtion : Form
    {
        Postroenie oldPostroenie { get; set; }
        Model M = new Model();                  // текущая модель

        public FormRedaсtion()
        {
            if (!M.GetConnectionStatus())
                MessageBox.Show("Не подключено к моделе");
            else
            {
                InitializeComponent();
                Obsledovanie();
                GetPostroenie();
            }
        }
        private void Obsledovanie()
        {
            var namesOfRack = M.GetModelObjectSelector()
                .GetObjectsByFilterName("GetAllPipeRack")
                .ToIEnumerable<Beam>()
                .Select(beam => beam.SmartGetPropertyString("RNazvanie"))
                .Distinct()
                .ToArray();

            NameOfRack.Items.AddRange(namesOfRack);
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (NameOfRack.SelectedItem == null)
            {
                MessageBox.Show("Не выбран участок эстакады");
                return;
            }
                
            var nameOfPipeRack = NameOfRack.SelectedItem.ToString();

            
            var typeOfElement = TypeOfElements.SelectedItem.ToString();
            var nomerProleta = int.Parse(NomerProleta.Text);
            var right = checkBox1.Checked;
            var nomerYarusa = int.Parse(NomerYarusa.Text);
            var typeAtt = TypeAtt.SelectedItem.ToString();
            var newZnachenie = New.Text;

            List<Beam> beams = new List<Beam>();

            var selectedBeams = M.GetModelObjectSelector()
                .GetObjectsByFilterName("GetAllPipeRack")
                .ToIEnumerable<Beam>().ToArray();

            foreach (var beam in selectedBeams)
            {
                var getTypeOfElement = beam.SmartGetPropertyString("RType");
                var getnomerProleta = int.Parse(beam.SmartGetPropertyString("RNumberOfSpan"));
                var getNumberOfYarus = int.Parse(beam.SmartGetPropertyString("RNumberOfYarus"));
                var DirectionOfYarus = beam.SmartGetPropertyString("DirectionOfYarus");

                if (getTypeOfElement == typeOfElement)
                {
                    if (getnomerProleta == nomerProleta)
                    {
                        if (getNumberOfYarus == nomerYarusa)
                        {
                            if (DirectionOfYarus == "Right" && right == true)
                                beams.Add(beam);
                            else if (DirectionOfYarus == "Left" && right == false)
                                beams.Add(beam);
                            else if (DirectionOfYarus == "Center")
                                beams.Add(beam);
                        }
                    }
                        
                }
            }

            foreach (var m in beams)
            {
                ATT(m, typeAtt, newZnachenie);
                m.Modify();
            }
            M.CommitChanges();
        }
        private void ATT(Beam beam, string typeAtt, string newZnachenie)
        {
             if (typeAtt == "Класс")
                beam.Class = newZnachenie;
            if (typeAtt == "Имя")
                beam.Name = newZnachenie;
            if (typeAtt == "Профиль")
                beam.Profile.ProfileString = newZnachenie;
            if (typeAtt == "Материал")
                beam.Material.MaterialString = newZnachenie;
            if (typeAtt == "Префикс сборки")
                beam.PartNumber.Prefix = newZnachenie;
            if (typeAtt == "Номер сборки")
                beam.PartNumber.StartNumber = int.Parse(newZnachenie);

            if (typeAtt == "Положение вертикально")
            {
                var PolojenieVertikalno = int.Parse(newZnachenie);
                if (PolojenieVertikalno == 1) beam.Position.Depth = Position.DepthEnum.MIDDLE;
                if (PolojenieVertikalno == 0) beam.Position.Depth = Position.DepthEnum.BEHIND;
                if (PolojenieVertikalno == 2) beam.Position.Depth = Position.DepthEnum.FRONT;
            }
                
            if (typeAtt == "Положение горизонтально")
            {
                var PolojenieGorizontalno = int.Parse(newZnachenie);
                if (PolojenieGorizontalno == 1) beam.Position.Plane = Position.PlaneEnum.LEFT;
                if (PolojenieGorizontalno == 0) beam.Position.Plane = Position.PlaneEnum.MIDDLE;
                if (PolojenieGorizontalno == 2) beam.Position.Plane = Position.PlaneEnum.RIGHT;

            }
                
            if (typeAtt == "Положение поворот")
            {
                var PolojeniePovorot = int.Parse(newZnachenie);
                if (PolojeniePovorot == 1) beam.Position.Rotation = Position.RotationEnum.FRONT;
                if (PolojeniePovorot == 0) beam.Position.Rotation = Position.RotationEnum.TOP;
            }
        }

        private void GetShagRam_Click(object sender, EventArgs e)
        {
            ShagRamTB.Text = oldPostroenie.shagRam;
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            ModifyPostroenie();
        }

        private void GetPostroenie()
        {
            var jsonString = File.ReadAllText(M.GetInfo().ModelPath + "\\frames.json");
            oldPostroenie = Newtonsoft.Json.JsonConvert.DeserializeObject<Postroenie>(jsonString);
        }


        private void ModifyPostroenie()
        {
            oldPostroenie.AttFrame.AttributesYarusRight.RemoveRange(0, oldPostroenie.AttFrame.AttributesYarusRight.Count() / 2);
            oldPostroenie.AttFrame.AttributesYarusLeft.RemoveRange(0, oldPostroenie.AttFrame.AttributesYarusLeft.Count() / 2);
            oldPostroenie.AttFrame.AttributesColumn.RemoveRange(0, oldPostroenie.AttFrame.AttributesColumn.Count() / 2);

            oldPostroenie.AttFrameProlet.AttProletBeamLeft.RemoveRange(0, oldPostroenie.AttFrameProlet.AttProletBeamLeft.Count() / 2);
            oldPostroenie.AttFrameProlet.AttProletBeamRight.RemoveRange(0, oldPostroenie.AttFrameProlet.AttProletBeamRight.Count() / 2);
            oldPostroenie.AttFrameProlet.AttProletTraversaLeft.RemoveRange(0, oldPostroenie.AttFrameProlet.AttProletTraversaLeft.Count() / 2);
            oldPostroenie.AttFrameProlet.AttProletTraversaRight.RemoveRange(0, oldPostroenie.AttFrameProlet.AttProletTraversaRight.Count() / 2);
            oldPostroenie.AttFrameProlet.AttProletStoyki.RemoveRange(0, oldPostroenie.AttFrameProlet.AttProletStoyki.Count() / 2);


            TransformationPlane currentPlane = M.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            TransformationPlane zero_plane = new TransformationPlane();
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(zero_plane);
            var TP = SmartTransfomationPlane.GetTransformationPlaneTwoPoints(M, oldPostroenie.CS_point, oldPostroenie.CS_point_end);
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(TP);

            var DistanceList = BoltsMethods.GetDistanceList(ShagRamTB.Text);
            List<double> Trav = DistanceList.ToList();
            var oldCountFrames = oldPostroenie.FraMES.Count();



            if (DistanceList.Count() == oldPostroenie.DistanceList.Count() || DistanceList.Count() > oldPostroenie.DistanceList.Count()) // если количество рам такое же, то изменение координат и тут новое построение
            {


                oldPostroenie.Grid.CoordinateX = ShagRamTB.Text;
                oldPostroenie.Grid.Modify();

                for (int _count = 0; _count < oldPostroenie.FraMES.Count(); _count++)
                {
                    oldPostroenie.FraMES[_count]._basePoint.X = Trav[_count];
                    oldPostroenie.FraMES[_count].Modify();
                }

                List<double> Travs = Trav;
                for (int _count = 1; _count < Travs.Count(); _count++)
                {
                    Travs[0] = Travs[0];
                    Travs[_count] = Travs[_count - 1] + Travs[_count];
                }
                for (int _count = oldCountFrames; _count < DistanceList.Count(); _count++)
                {
                    M.GetWorkPlaneHandler().SetCurrentTransformationPlane(TP);
                    var point = new T3D.Point(Travs[_count], 0, 0);
                    var frame = new Frame(M, point, oldPostroenie.yarus_count, oldPostroenie.count_column, oldPostroenie.nameOfpipeRack)
                    {
                        Razdv12 = oldPostroenie.razdv1to2,
                        Razdv23 = oldPostroenie.razdv2to3,
                        Traversy = oldPostroenie.Traversy,
                        Traversy2 = oldPostroenie.Traversy2,
                        Attributes = oldPostroenie.AttFrame.AttributesYarusRight,
                        Attributes2 = oldPostroenie.AttFrame.AttributesYarusLeft,
                        AttributeColumn = oldPostroenie.AttFrame.AttributesColumn,
                        UklonbI = oldPostroenie.UklonbI,
                        _M = M,
                    };
                    frame.Insert();             // построились рамы (колонны и траверсы)
                    oldPostroenie.FraMES.Add(frame);
                    
                }

                M.GetWorkPlaneHandler().SetCurrentTransformationPlane(TP);
                for (int count = 0; count < oldCountFrames - 1; count++)  
                {
                    oldPostroenie.BalkiYarysA[count]._Frame1 = oldPostroenie.FraMES[count];
                    oldPostroenie.BalkiYarysA[count]._Frame1._basePoint.X = Travs[count];
                    oldPostroenie.BalkiYarysA[count]._Frame2 = oldPostroenie.FraMES[count + 1];
                    oldPostroenie.BalkiYarysA[count]._Frame2._basePoint.X = Travs[count + 1];
                    oldPostroenie.BalkiYarysA[count].Modify();
                }

                for (int count = oldCountFrames - 1; count < DistanceList.Count()-1; count++)
                {
                    var balkiYarusa = new BalkiYarysa(oldPostroenie.FraMES[count], oldPostroenie.FraMES[count +1], oldPostroenie.AttFrameProlet);
                    balkiYarusa.Insert();
                    oldPostroenie.BalkiYarysA.Add(balkiYarusa);
                }




                }
            else if(DistanceList.Count() < oldPostroenie.DistanceList.Count()) // если шагов стало меньше, то удалить лишние
            {
                List<double> Travs = Trav;
                for (int _count = 1; _count < Travs.Count(); _count++)
                {
                    Travs[0] = Travs[0];
                    Travs[_count] = Travs[_count - 1] + Travs[_count];
                }

                oldPostroenie.Grid.CoordinateX = ShagRamTB.Text;
                oldPostroenie.Grid.Modify();

                for (int _count = 0; _count < DistanceList.Count(); _count++)
                {
                    oldPostroenie.FraMES[_count]._basePoint.X = Trav[_count];
                    oldPostroenie.FraMES[_count].Modify();
                }

                for (int _count = DistanceList.Count(); _count < oldPostroenie.FraMES.Count(); _count++)
                {
                    oldPostroenie.FraMES[_count].Delete();
                }

                M.GetWorkPlaneHandler().SetCurrentTransformationPlane(TP);


                for (int count = 0; count < DistanceList.Count() - 1; count++)  //выбрал пару рам между которыми буду строить продольные балки
                {
                    oldPostroenie.BalkiYarysA[count]._Frame1 = oldPostroenie.FraMES[count];
                    oldPostroenie.BalkiYarysA[count]._Frame1._basePoint.X = Travs[count];
                    oldPostroenie.BalkiYarysA[count]._Frame2 = oldPostroenie.FraMES[count + 1];
                    oldPostroenie.BalkiYarysA[count]._Frame2._basePoint.X = Travs[count + 1];
                    oldPostroenie.BalkiYarysA[count].Modify();
                }
                for (int count = DistanceList.Count() - 1; count < oldPostroenie.FraMES.Count() - 1; count++)  //выбрал пару рам между которыми буду строить продольные балки
                {

                    oldPostroenie.BalkiYarysA[count].Delete();
                }
            }




            var jsonString = JsonConvert.SerializeObject(oldPostroenie);
            //var jsonString = JsonSerializer.Serialize(FraMES);
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


            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
            M.CommitChanges();


        }
    }
}
