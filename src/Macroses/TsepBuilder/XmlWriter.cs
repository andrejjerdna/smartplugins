namespace TsepBuilder
{
    public class XmlWriter
    {
        private readonly string _tsepDefinition = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                              "TepInstaller", 
                                                              "TsepDefinition.xml");

        public void WriteObjects(IEnumerable<string> objects, string source, string target, string componentType)
        {
            var libraries= new List<string>();
            foreach (var file in objects)
            {
                var guid = Path.GetFileNameWithoutExtension(file);
                var r = $"<File Id=\"{guid}\" Source=\"%{source}%\\{file}\" Target=\"%{target}%\"/>";
                libraries.Add(r);
            }

            if(libraries.Any())
                AddLibraryInTsepDefinition(libraries, componentType);
        }

        private void AddLibraryInTsepDefinition(IEnumerable<string> libraries, string componentType)
        {
            var result = new List<string>();   
            
            var tsepDefinitionXml = File.ReadAllLines(_tsepDefinition)?.ToList();

            if (tsepDefinitionXml == null)
                return;

            var clear = false;

            for (int i = 0; i < tsepDefinitionXml.Count; i++)
            {
                if (tsepDefinitionXml[i].Contains(componentType))
                {
                    result.Add(tsepDefinitionXml[i]);
                    clear = !clear;
                    continue;
                }

                if (!clear)
                    result.Add(tsepDefinitionXml[i]);
            }

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].Contains(componentType))
                {
                    var localCount = i + 1;
                    foreach (var library in libraries)
                        result.Insert(localCount++, $"\t\t{library}");

                    break;
                }
            }


            File.WriteAllLines(_tsepDefinition, result);
        }
    }
}
