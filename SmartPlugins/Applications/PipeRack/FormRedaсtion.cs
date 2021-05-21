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
            ModelObjectEnumerator Enuml = M.GetModelObjectSelector().GetAllObjectsWithType(ModelObject.ModelObjectEnum.BEAM);

            foreach (Beam allBeam in Enuml)
            {
                string Nazvanie = null ;
                allBeam.GetUserProperty("Nazvanie", ref Nazvanie);
                if (Nazvanie == null)
                    continue;
                if (!NameOfRack.Items.Contains(Nazvanie))
                     NameOfRack.Items.Add(Nazvanie);
            }

            var namesOfRack = M.GetModelObjectSelector()
                .GetAllObjectsWithType(ModelObject.ModelObjectEnum.BEAM)
                .ToIEnumerable<Beam>()
                .Select(beam => beam.)

        }


        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
