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
        public Model Model { get; }
        public bool ConectStatus { get; }
        public string FilterPath { get; }
        public string Attributes { get; }

        public TeklaModel()
        {
            Model = new Model();
            ConectStatus = Model.GetConnectionStatus();

            if (ConectStatus)
            {
                Attributes = Model.GetInfo().ModelPath + "\\attributes\\";
                FilterPath = Attributes;
            }
        }
    }
}
