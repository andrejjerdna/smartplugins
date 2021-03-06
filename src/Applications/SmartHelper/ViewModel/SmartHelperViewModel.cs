using SmartPlugins.Common.SmartWPFElements;
using SmartPlugins.Common.SmartWPFElements.SmartHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Applications.SmartHelper.ViewModel
{
    public class SmartHelperViewModel : BaseViewModel
    {
        public ObservableCollection<SmartButton> SmartButtons
        {
            get => GetColl<SmartButton>();
            set => SetColl(value);
        }

    }
}
