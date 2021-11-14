using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace AxCoDesign.Applications.AutoConnect.Model
{
    public class TeklaModelDatasWriter
    {
        private IEnumerable<TeklaModelData> _datas;
        private string _pathTeklaML;
        private string _pathDB;
        private readonly string _dataFileName = "MLData.json";

        public TeklaModelDatasWriter(string pathTeklaML)
        {
            _pathTeklaML = pathTeklaML;
            _pathDB = Path.Combine(_pathTeklaML, _dataFileName);
        }

        /// <summary>
        /// Запись базы данных в файл.
        /// </summary>
        public async void WriteDatasToFile(IEnumerable<TeklaModelData> datas)
        {
            if (datas == null)
                return;

            using (FileStream fs = new FileStream(_pathDB, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<IEnumerable<TeklaModelData>>(fs, datas);
            }



            //var json = JsonConvert.SerializeObject(_datas);

            //var dirInfo = new DirectoryInfo(_pathTeklaML);
            //if (!dirInfo.Exists)
            //{
            //    dirInfo.Create();
            //}

            //using (var file = new StreamWriter(_pathDB, false))
            //{
            //    file.WriteLine(json);
            //}
        }
    }
}
