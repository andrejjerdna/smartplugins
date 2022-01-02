using SmartPlugins.Common.Abstractions.ModelObjects;
using System.Linq;

namespace SmartPlugins.Common.Core.ModelAlgorithms
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Set a main part by max weight.
        /// If the main part already has the maximum weight, the value false will be returned
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static bool SetMainPartByMaxWeight(this IAssembly assembly, string propertyName)
        {
            if (assembly == null)
                return false;

            IPart candidatePart = null;

            var mainPart = assembly.GetMainPart();

            if (mainPart == null)
                return false;

            var weightMainPart = mainPart.GetProperty<double>(propertyName);

            var assemblyDetails = assembly.GetSecondaries();

            foreach (var modelObject in assemblyDetails)
            {
                if (modelObject is IPart part)
                {
                    var weightCurrentDetail = part.GetProperty<double>(propertyName);

                    if (weightCurrentDetail > weightMainPart)
                    {
                        weightMainPart = weightCurrentDetail;
                        candidatePart = part;
                    }
                }
            }

            if (candidatePart != null)
            {
                assembly.SetMainPart(candidatePart);
                assembly.Modify();
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void NumberingSecondariesParts(this IAssembly assembly, string propertyName)
        {
            var eer = assembly.GetSecondaries().Select(part => new { ppp = part.GetProperty<double>(propertyName), we = part })
                .OrderByDescending(obj => obj.ppp).Select(obj => obj.we).ToList();

            assembly.GetMainPart().SetProperty("USER_FIELD_1", 1);

            var count = 2;

            foreach (var ee in eer)
            {
                ee.SetProperty("USER_FIELD_1", count);
                count++;
            }
        }
    }
}
