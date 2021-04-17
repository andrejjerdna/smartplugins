using SmartTeklaModel;
using SmartTeklaModel.Drawings;
using SmartTeklaModel.Files;
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

        [StructuresDialog(nameof(HatchColor), typeof(String))]
        public String HatchColor { get; set; }

        [StructuresDialog(nameof(LineType), typeof(String))]
        public String LineType { get; set; }

        [StructuresDialog(nameof(HatchName), typeof(String))]
        public String HatchName { get; set; }

        [StructuresDialog(nameof(DimensionType), typeof(String))]
        public String DimensionType { get; set; }

        [StructuresDialog(nameof(ScaleX), typeof(Distance))]
        public Distance ScaleX { get; set; }

        [StructuresDialog(nameof(ScaleY), typeof(Distance))]
        public Distance ScaleY { get; set; }

        [StructuresDialog(nameof(AngleHatch), typeof(Distance))]
        public Distance AngleHatch { get; set; }

        [StructuresDialog(nameof(DimensionInsert), typeof(Integer))]
        public Integer DimensionInsert { get; set; }

        [StructuresDialog(nameof(DiagonalInsert), typeof(Integer))]
        public Integer DiagonalInsert { get; set; }

        [StructuresDialog(nameof(HatchInsert), typeof(Integer))]
        public Integer HatchInsert { get; set; }

        public IEnumerable<string> DrawinColors { get => Colors.GetListColors().Select(c => c.ToString()); }
        public IEnumerable<string> DrawinHatchColors { get => Colors.GetListHatchColors().Select(c => c.ToString()); }
        
        public IEnumerable<string> LineTypes { get => Lines.GetListLineTypes().Select(c => c.ToString()); }
        public IEnumerable<string> DimensionsTypes { get => GetAttributesFiles.GetDimensionsTypes(); }

        public IEnumerable<string> HatchNames => new List<string>()
        {
            "ANSI31",
            "ANSI32",
            "SOLID"
        };

        public IEnumerable<string> DimensionsInserts => new List<string>()
        {
            "Нет",
            "Да"
        };

        public IEnumerable<string> DiagonalInserts => new List<string>()
        {
            "Нет",
            "Да"
        };

        public IEnumerable<string> HatchInserts => new List<string>()
        {
            "Нет",
            "Да"
        };

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
