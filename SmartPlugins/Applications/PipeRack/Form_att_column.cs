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
    public partial class Form_att_column : Form_att
    {
        public Form_att_column(List<Attributes> attributesFrame, int Count) : base(attributesFrame, Count)
        {
            base.selectYarus.Text = "Выбор колонны";
            base.SelectYarusCB.Text = "Колонна 1";
            base.SelectYarusCB.Items.Clear();
            base.SelectYarusCB.Items.AddRange(new object[] {
            "Колонна 1",
            "Колонна 2",
            "Колонна 3"});
            base.SelectYarusCB.SelectedIndex = 0;
        }
    }
}
