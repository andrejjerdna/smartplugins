using System.Collections.Generic;

namespace SmartPlugins.Common.Abstractions.TeklaStructures
{
    public interface IRebarNumerator
    {
        /// <summary>
        /// Refresh numbers for list of reinforcements
        /// </summary>
        /// <param name="reinforcements"></param>
        /// <param name="attributeForNumber"></param>
        void RefreshAllNumbers(string attributeForNumber);
    }
}
