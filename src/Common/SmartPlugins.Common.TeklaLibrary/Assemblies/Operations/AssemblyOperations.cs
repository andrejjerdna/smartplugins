using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Assemblies
{
    /// <summary>
    /// Operations for tekla assembly
    /// </summary>
    internal sealed class AssemblyOperations
    {
        /// <summary>
        /// Set a main part by max weight.
        /// If the main part already has the maximum weight, the value false will be returned
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static bool SetMainPartByMaxWeight(Assembly assembly)
        {
            Part candidatePart = null;

            var mainPart = assembly.GetMainPart();

            var weightMainPart = 0.0;
            mainPart.GetReportProperty(TeklaProperties.Weight, ref weightMainPart);

            var assemblyDetails = assembly.GetSecondaries();

            foreach (var modelObject in assemblyDetails)
            {
                if (modelObject is Part part)
                {
                    var weightCurrentDetail = 0.0;
                    part.GetReportProperty(TeklaProperties.Weight, ref weightCurrentDetail);

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
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
