using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartTeklaModel
{
    public class TeklaModel
    {
        public Model Model { get => GetModel(); }
        public string FilterPath { get => GetFilterPath(); }
        public string DataPath { get => GetDataPath(); }
        public string IFCPath { get => GetIFCPath(); }
        public string attributes { get => Model.GetInfo().ModelPath + "\\attributes\\"; }

        private string GetFilterPath()
        {
            return Model.GetInfo().ModelPath + "\\attributes\\" + GlobalParameters.FilterName;
        }

        private string GetDataPath()
        {
            return Model.GetInfo().ModelPath + "\\attributes\\" + GlobalParameters.WizardData;
        }

        private string GetIFCPath()
        {
            return Model.GetInfo().ModelPath + "\\attributes\\" + GlobalParameters.WizardIFC;
        }

        private Model GetModel()
        {
            return new Model();
        }
    }
}
