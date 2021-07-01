using SmartWPFElements;
using SmartWPFElements.SmartHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHelper.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public SteelWindowViewModel SteelWindowViewModel
        {
            get => Get(new SteelWindowViewModel());
            set => Set(value);
        }
    }
}
