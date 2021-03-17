using SmartTeklaModel.Drawings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Datatype;
using Tekla.Structures.Dialog;
using Tekla.Structures.Drawing;
using String = Tekla.Structures.Datatype.String;

namespace sp_DimensionsForReinforcement
{
    public class DimensionsForReinforcementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [StructuresDialog(nameof(L1), typeof(Distance))]
        public Distance L1 { get; set; }

        [StructuresDialog(nameof(L2), typeof(Distance))]
        public Distance L2 { get; set; }

        [StructuresDialog(nameof(L3), typeof(Distance))]
        public Distance L3 { get; set; }

        [StructuresDialog(nameof(L4), typeof(Distance))]
        public Distance L4 { get; set; }

        [StructuresDialog(nameof(LineColor), typeof(String))]
        public String LineColor { get; set; }

        [StructuresDialog(nameof(LineType), typeof(String))]
        public String LineType { get; set; }

        public IEnumerable<string> DrawinColors { get => Colors.GetListColors().Select(c => c.ToString()); }
        public IEnumerable<string> LineTypes { get => Lines.GetListLineTypes().Select(c => c.ToString()); }



        protected void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
