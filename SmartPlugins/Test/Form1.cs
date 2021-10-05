using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model.Operations;

namespace Test
{
    public partial class Form1 : Form
    {
        private Dictionary<string, List<string>> objctsInDrws = new Dictionary<string, List<string>>();

        public Form1()
        {
            InitializeComponent();
            TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var currentDate = DateTime.Today;
            var endDate = new DateTime(2021, 12, 31, 0, 0, 0);

            var span1 = currentDate - endDate;


            if (span1.Days > 0)
            {
                MessageBox.Show("License status: " + "Not activated.");

            }
            else
            {
                MessageBox.Show("License status: " + "Trial. Expiration date 31.12.2021");
            }
        }

        private struct DateNewType1
        {
            DateTime DateTime;
        }

        private struct DateNewType2
        {
            DateTime DateTime;
        }
    }
}

