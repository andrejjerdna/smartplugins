using SmartWPFElements.SmartHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartHelper.Pages.TestApp
{
    public class TestAppRunner : ITestAppRunner
    {
      public string Name { get; }

        public TestAppRunner()
        {
            Name = "Test apps";
        }

        public async Task<bool> Run()
        {
            MessageBox.Show("I work!");
            
            return true;
        }
    }
}
