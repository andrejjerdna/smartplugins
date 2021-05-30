using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartExtensions
{
    public static class PartExtension
    {
        public static double SmartGetPropertyDouble(this Part part, string attributeName)
        {
            var result = double.NaN;
            part.GetReportProperty(attributeName, ref result);
            return result;
        }

        public static string SmartGetPropertyString(this Part part, string attributeName)
        {
            var result = string.Empty;
            part.GetReportProperty(attributeName, ref result);
            return result;
        }

        /// <summary>
        /// Получаем всю арматуру детали.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="getMainPart">Надо ли получать главную деталь сборки.</param>
        /// <returns></returns>
        public static IEnumerable<BaseRebarGroup> GetAllRebars(this Part part)
        {
            return part.GetReinforcements().ToIEnumerable<BaseRebarGroup>();
        }

        /// <summary>
        /// Получаем всю арматуру детали.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="getMainPart">Надо ли получать главную деталь сборки.</param>
        /// <returns></returns>
        public static IEnumerable<RebarSet> GetAllRebarSets(this Part part)
        {
            return part.GetReinforcements().ToIEnumerable<RebarSet>();
        }
    }
}
