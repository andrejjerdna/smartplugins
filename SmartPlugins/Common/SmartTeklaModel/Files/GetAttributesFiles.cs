using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeklaModel.Files
{
    public static class GetAttributesFiles
    {
        /// <summary>
        /// Получение списка файлов по списку типов и пути папки.
        /// </summary>
        /// <param name="save"></param>
        /// <param name="path"></param>
        /// <param name="fileType"></param>
        public static IEnumerable<string> NamesFilesOnTypes(string path, List<string> fileTypeList)
        {
            var result = new List<string>();
            
            try
            {
                fileTypeList.ForEach(fileType =>
                {
                    var resultLocal = Directory.EnumerateFiles(path, "*" + fileType).ToList();
                    resultLocal.ForEach(name =>
                    {
                        var fileName = name.Replace(path, "").Replace(fileType, "");
                        result.Add(fileName);
                    });
                });

                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }

       /* public static IEnumerable<string> Gegeg()
        {
            return Tekla.Structures.Dialog.UIControls.EnvironmentFiles.GetStandardPropertyFileDirectories();
        }*/

        public static IEnumerable<string> GetDimensionsTypes()
        {
            return Tekla.Structures.Dialog.UIControls.EnvironmentFiles.GetMultiDirectoryFileList("dim");
        }

        public static IEnumerable<string> GetMarksTypes()
        {
            return Tekla.Structures.Dialog.UIControls.EnvironmentFiles.GetMultiDirectoryFileList("rm");
        }
    }
}
