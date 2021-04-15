using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using SmartGeometry;

namespace PipeRack
{
    public class Attributes
    {
        public string Name { get; set; }
        public string Profile { get; set; }
        public string Material { get; set; }
        public string Class { get; set; }

        private double _yarusCount;
        private List<string> _attributes;

        public Attributes()
        {

        }

        public void Insert()
        {
            _attributes.Add(Name);
            _attributes.Add(Profile);
            _attributes.Add(Material);
            _attributes.Add(Class);
        }

        public List<string> Get_att()
        {
            return _attributes;
        }
    }
}
