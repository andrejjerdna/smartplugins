using SmartExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartCheckAssemblies
{
    class SmartCheckAssembliesModel
    {
        private Model _model;

        private List<float> _result;

        public SmartCheckAssembliesModel()
        {
            _model = new Model();
            _result = new List<float>();
        }

        public void Start()
        {
            if (!_model.GetConnectionStatus())
                return;

            var assemblies = _model.GetModelObjectSelector()
                .GetAllObjectsWithType(ModelObject.ModelObjectEnum.ASSEMBLY)
                .ToIEnumerable<Assembly>()
                .ToList();

            var info = new List<DataConnect>();

            foreach(var assembly in assemblies)
            {
                var detailsInfo = new DetailsInfo(assembly);

                //info.Add(new DetailsInfo(assembly));
            }
        }
    }
}
