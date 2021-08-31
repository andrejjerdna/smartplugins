using SmartPlugins.Common.SmartExtensions;
using SmartPlugins.Common.SmartTeklaModel;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Tekla.Structures.Model;
using System.Linq;
using System.Diagnostics;

namespace SmartPlugins.Applications.SmartCheckAssembliesML
{
    class SmartCheckAssembliesModel
    {
        private SmartModel _smartModel;

        private List<CheckData> _result;

        public SmartCheckAssembliesModel()
        {
            _smartModel = new SmartModel();
            _result = new List<CheckData>();
        }

        public void Start()
        {
            if (!_smartModel.ConnectionStatus)
                return;

            var assemblies = _smartModel.TeklaModel.GetModelObjectSelector()
                .GetAllObjectsWithType(ModelObject.ModelObjectEnum.ASSEMBLY)
                .ToConcurrentBag<Assembly>();

            Parallel.ForEach(assemblies, (assembly) =>
            {
                var detailsInfo = new DetailsInfo(assembly);

                var data = detailsInfo.GetChangeDatas();

                if (data != null)
                    _result.AddRange(data);
            });

            //var w2 = new Stopwatch();
            //w2.Start();

            //var assemblies2 = _smartModel.TeklaModel.GetModelObjectSelector()
            //    .GetAllObjectsWithType(ModelObject.ModelObjectEnum.ASSEMBLY)
            //    .ToIEnumerable<Assembly>();

            //foreach(var assembly in assemblies2)
            //{
            //    var detailsInfo = new DetailsInfo(assembly);

            //    var data = detailsInfo.GetChangeDatas();

            //    if (data != null)
            //        _result.AddRange(data);
            //}

            //w2.Stop();

            //MessageBox.Show(string.Format("Всего получено сборок: {2}. \nМного потоков: {0} сек. \nОдин поток: {1} сек.", w1.Elapsed, w2.Elapsed, assemblies.Count));

            WriteDataFile();

            MessageBox.Show("OK!");
        }

        public List<CheckData> GetCheckDataSelectAssembly()
        {
            if (!_smartModel.ConnectionStatus)
                return null;

            var assemblies = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects()
                .ToIEnumerable<Assembly>()
                .ToList();

            if (assemblies.Count > 0)
            {
                var detailsInfo = new DetailsInfo(assemblies.First());
                return detailsInfo.GetChangeDatas();
            }
            else
                return null;
        }

        /// <summary>
        /// Запись базы данных в файл.
        /// </summary>
        public void WriteDataFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(_result, options);

            var path = _smartModel.SmartPluginsPath + "check.json";

            var dirInfo = new DirectoryInfo(_smartModel.SmartPluginsPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            using (var file = new StreamWriter(path, false))
            {
                file.WriteLine(json);
            }
        }
    }
}
