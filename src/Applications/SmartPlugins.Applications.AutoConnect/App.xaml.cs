using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AxCoDesign.Applications.AutoConnect
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex _instanceMutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                _instanceMutex = new Mutex(true, @"Global\ReportsForAGHKMutex", out var createdNew);
                if (!createdNew)
                {
                    _instanceMutex = null;
                    Current.Shutdown();
                    return;
                }
            }
            catch
            {
                //ignore
            }
            base.OnStartup(e);
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private static void ComposeObjects()
        {
            Current.MainWindow = Container.Get<MainWindow>();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                _instanceMutex?.ReleaseMutex();
                _instanceMutex?.Close();
            }
            catch
            {
                //ignore
            }
            finally
            {
                _instanceMutex = null;
            }
            base.OnExit(e);
        }
    }
}
