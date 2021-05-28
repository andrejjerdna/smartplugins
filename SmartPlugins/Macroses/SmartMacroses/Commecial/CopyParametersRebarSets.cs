using SmartExtensions;
using SmartMacroses.Commecial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;


namespace SmartMacroses
{
    public class CopyParametersRebarSets
    {
        public static void Run()
        {
            var selectWindow = new CopyParametersRebarSetsWindow();
            selectWindow.ShowDialog();
        }
    }
}
