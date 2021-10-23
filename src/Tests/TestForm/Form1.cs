using SmartPlugins.Macroses.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model.Operations;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Operation.RunMacro("RebarSeqNumbering.cs");
            stopwatch.Stop();

            label1.Text = label1.Text + " " + stopwatch.Elapsed.TotalSeconds + " sec.";

            stopwatch.Reset();
            stopwatch.Start();

            new RebarSequenceNumberingMacro().Run();

            stopwatch.Stop();

            label2.Text = label2.Text + " " + stopwatch.Elapsed.TotalSeconds + " sec.";

            //var stopwatch = new Stopwatch();
            //stopwatch.Start();

            //var userClass = new UserClass();
            //userClass.Run();

            //stopwatch.Stop();

            //label1.Text = label1.Text + " " + stopwatch.Elapsed.TotalSeconds + " sec.";

            // stopwatch.Reset();
            // stopwatch.Start();

            //var checkMarks = new CheckMarks();
            //checkMarks.Run();

            //stopwatch.Stop();

            //label2.Text = label2.Text + " " + stopwatch.Elapsed.TotalSeconds + " sec.";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
