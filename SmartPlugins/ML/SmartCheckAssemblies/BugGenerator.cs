using SmartPlugins.Common.SmartExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using Parallel = System.Threading.Tasks.Parallel;

namespace SmartPlugins.Applications.SmartCheckAssembliesML
{
    public class BugGenerator
    {
        private Model _model;

        private List<float> _result;

        public BugGenerator()
        {
            _model = new Model();
            _result = new List<float>();
        }

        public void Start()
        {
            if (!_model.GetConnectionStatus())
                return;

            var assemblies = new Tekla.Structures.Model.UI.ModelObjectSelector()
                .GetSelectedObjects()
                .ToConcurrentBag<Assembly>();

            Parallel.ForEach(assemblies, (assembly) =>
            {
                var details = assembly.GetSecondaries().OfType<Part>().ToList();

                if (details.Count < 1)
                    return;

                var random = new Random();

                var index = random.Next(0, details.Count() - 1);

                var x1 = random.Next(-1000, -100);
                var y1 = random.Next(-1000, -100);
                var z1 = random.Next(-1000, -100);

                var x2 = random.Next(100, 1000);
                var y2 = random.Next(100, 1000);
                var z2 = random.Next(100, 1000);

                var detail = details[index];

                var p1 = new Point(x1, y1, z1);
                var p2 = new Point(x2, y2, z2);

                detail.Class = "1234";
                detail.Modify();

                Operation.MoveObject(detail, new Vector(p2 - p1));

                _model.CommitChanges();
            });
        }
        }
}
