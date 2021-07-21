using SmartWPFElements.SmartHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHelper.Apps.Drawings
{
    public interface IDimensionsForRebarGroup : ISmartHelperApp
    {

    }

    public class DimensionsForRebarGroup : IDimensionsForRebarGroup
    {
        public string Name { get; }

        //public TestAppRunner()
        //{
        //    Name = "Test apps";
        //}

        public async Task<bool> Run()
        {
            //MessageBox.Show("I work!");

            return true;
        }
    }
}
