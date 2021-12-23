using System.Collections.Generic;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Extensions
{
    public static class PartExtension
    {
        /// <summary>
        /// Получаем всю арматуру детали.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="getMainPart">Надо ли получать главную деталь сборки.</param>
        /// <returns></returns>
        public static IEnumerable<Reinforcement> GetAllReinforcements(this Part part)
        {
            return part.GetReinforcements().ToIEnumerable<Reinforcement>();
        }
    }
}
