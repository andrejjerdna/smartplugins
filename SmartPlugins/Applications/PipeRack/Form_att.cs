using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PipeRack
{
    public partial class Form_att : Form
    {
        public Form_att()
        {
            InitializeComponent();
        }

        private void Label24_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

            string namen = Namen.Text;
            string profile = Profile.Text;
            string material = Material.Text;
            string class1 = Class.Text;

            var att = new Attributes()

            {
                Name = namen,
                Profile = profile,
                Material = material,
                Class = class1,

            };



            Form_att.ActiveForm.Close();
        }
    }
}
