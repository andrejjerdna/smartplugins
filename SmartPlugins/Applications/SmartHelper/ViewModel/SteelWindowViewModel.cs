using SmartHelper.Pages;
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
    public class SteelWindowViewModel : BaseViewModel
    {
        public ObservableCollection<SmartButton> SmartButtons
        {
            get => GetColl<SmartButton>();
            set => SetColl(value);
        }

        public SteelWindowViewModel()
        {
            var steelApps = GetSteelApps();
            SmartButtons = new ObservableCollection<SmartButton>(steelApps);
        }

        private IEnumerable<SmartButton> GetSteelApps()
        {
            var steelApps = new List<SmartButton>
            {

            };

            return steelApps;
        }
    }
}
