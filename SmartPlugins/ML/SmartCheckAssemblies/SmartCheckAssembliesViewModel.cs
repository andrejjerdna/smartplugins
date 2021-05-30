using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartCheckAssemblies
{
    public class SmartCheckAssembliesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }

        public async void RunApp()
        {
            var check = new SmartCheckAssembliesModel();

            await Task.Run(() => check.Start());
        }

        public async void RunBug()
        {
            var check = new BugGenerator();

            await Task.Run(() => check.Start());
        }

        public async void RunTrain()
        {
            var check = new MLCheck();

            await Task.Run(() => check.MLTraining());
        }

        public void RunPredict()
        {
            var check = new SmartCheckAssembliesModel();

            var datas = check.GetCheckDataSelectAssembly();

            var MLcheck = new MLCheck();

            var result = false;

            foreach (var chekData in datas)
            {
                result = MLcheck.MLPredictor(chekData);

                if (result)
                {
                    MessageBox.Show("Есть ошибка!");
                    break;
                }
            }

            if (!result)
                MessageBox.Show("Нет ошибки.");
        }
    }
}
