//using SmartPlugins.Applications.SmartHelper.Pages;
//using SmartPlugins.Applications.SmartHelper.Pages.TestApp;
//using SmartPlugins.Common.TeklaStructures;
//using SmartPlugins.Common.SmartWPFElements;
//using SmartPlugins.Common.SmartWPFElements.SmartHelper;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media;

//namespace SmartPlugins.Applications.SmartHelper.ViewModel
//{
//    public class MainWindowViewModel : BaseViewModel
//    {
//        /// <summary>
//        /// Здесь создается объект, который в себе содержит модель теклы, а также различные расширения.
//        /// В данный момент это в основном пути к папкам модели. 
//        /// Постепенно этот класс будет расширяться.
//        /// </summary>
//        public Common.TeklaStructures.SmartModel SmartModel
//        {
//            get => Get(new Common.TeklaStructures.SmartModel());
//            set => Set(value);
//        }

//        public ObservableCollection<SmartButton> SmartButtons
//        {
//            get => GetColl<SmartButton>();
//            set => SetColl(value);
//        }

//        public object InnerContent
//        {
//            get { return Get(new SteelApps()); }
//            set { Set(value); }
//        }

//        public MainWindowViewModel()
//        {
//            var steelApps = GetSteelApps();
//            SmartButtons = new ObservableCollection<SmartButton>(steelApps);
//        }

//        private IEnumerable<SmartButton> GetSteelApps()
//        {
//            var steelApps = new List<SmartButton>
//            {
//                //new SmartButton()
//                //{
//                //    TextButton = "Test",
//                //    Icon = "EmoticonCool",
//                //    SmartHelperApp = new TestAppRunner(),
//                //    UI = new TestApp(),
//                //},

//                //new SmartButton()
//                //{
//                //    TextButton = "Test2",
//                //    Icon = "EmoticonCool",
//                //    SmartHelperApp = new TestAppRunner(),
//                //    UI = new TestApp(),
//                //}
//            };

//            return steelApps;
//        }
//    }
//}
