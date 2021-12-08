//using AxCoDesign.Applications.AutoConnect.Model;
//using AxCoDesign.Applications.Library.Exceptions;
//using AxCoDesign.Applications.Library.Properties;
//using AxCoDesign.ML.Library.AutoConnect;
//using Ninject;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;

//namespace AxCoDesign.Applications.AutoConnect
//{
//    class Container
//    {
//        private static IKernel _container;

//        static Container()
//        {
//            _container = new StandardKernel();
//           // _container.Bind<IMLResultsViewer>().To<MLResultsViewer>().InTransientScope();
//           // _container.Bind<IModelData>().To<TeklaModelData>().InTransientScope();
            
//        }

//        public static T Get<T>()
//        {
//            try
//            {
//                return _container.Get<T>();
//            }
//            catch (Exception e)
//            {
//               // MessageBox.Show(e.Message.Contains("Инициализатор типа") ? ErrorMessages.NotConnectedTekla : e.Message, Resources.MessageCaption);
//                return _container.Get<T>();
//            }
//        }
//    }
//}
