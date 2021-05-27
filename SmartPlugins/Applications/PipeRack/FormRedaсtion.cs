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

namespace PipeRack
{
    public partial class FormRedaсtion : Form
    {
        Model M = new Model();                  // текущая модель

        public FormRedaсtion()
        {
            if (!M.GetConnectionStatus())
                MessageBox.Show("Не подключено к моделе");
            else
            {
                InitializeComponent();
                Obsledovanie(); 
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
    }
}
