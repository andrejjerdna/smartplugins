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

        /// <summary>
        /// Numbering secondaries parts
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="propertyName"></param>
        /// <param name="userAttr"></param>
        public static void NumberingSecondariesParts(this IAssembly assembly, string propertyName, string userAttr)
        {
            //TODO: Убрать жесткую привязку к PART_POS
            var secondariesPartGroups = assembly.GetSecondaries()
                                          .GroupBy(part => part.GetProperty<string>("PART_POS"))
                                          .OrderByDescending(part => part.First().GetProperty<double>(propertyName))
                                          .ToList();

            assembly.GetMainPart().SetProperty(userAttr, 1);

            var count = 2;

            foreach (var secondariesPartGroup in secondariesPartGroups)
            {
                foreach(var part in secondariesPartGroup)
                    part.SetProperty(userAttr, count);

                count++;
            }

            assembly.Modify();
        }
    }
}
