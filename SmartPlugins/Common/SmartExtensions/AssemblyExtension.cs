using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartExtensions
{
    public static class AssemblyExtension
    {
        /// <summary>
        /// Получаем все детали сборки.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="getMainPart">Надо ли получать главную деталь сборки.</param>
        /// <returns></returns>
        public static IEnumerable<Part> GetAllParts(this Assembly assembly, bool getMainPart)
        {
            var result = new List<Part>();

            if (getMainPart)
                result.Add(assembly.GetMainPart() as Part);

            var secondariesParts = assembly.GetSecondaries().OfType<Part>();
            result.AddRange(secondariesParts);

            return result.Where(part => part != null);
        }
        /// <summary>
        /// Получаем всю арматуру сборки сборки.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="getMainPart">Надо ли получать главную деталь сборки.</param>
        /// <returns></returns>
        public static IEnumerable<Reinforcement> GetAllReinforcements(this Assembly assembly)
        {
            return assembly.GetAllParts(true).SelectMany(part => part.GetAllReinforcements());
        }

        /// <summary>
        /// Проверка на однодетальную марку-сборку
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static bool isSingleAssembly(this Part part)
        {
            return part.GetAssembly().GetSecondaries().Count == 0;
        }

        /// <summary>
        /// Проверка на однодетальную марку-сборку
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static bool isSingleAssembly(this List<Part> parts)
        {
            foreach (var p in parts)
            {
                if (isSingleAssembly(p) == true)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Получение списка однодетальных марок.
        /// </summary>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetSingleAssemblies(this List<Part> parts)
        {
            return parts.Where(a => a.isSingleAssembly()).Select(a => a.GetAssembly());
        }

        /// <summary>
        /// Получение списка сборок из списка деталей,
        /// Сборки не повторяются, если детали принадлежат к одной
        /// Сравнение идет по идентификатору
        /// </summary>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAssemblies(this List<Part> parts)
        {
            List<Assembly> result = new List<Assembly>();
            List<int> asmIDS = new List<int>();
            foreach (var p in parts)
            {
                if (p.isSingleAssembly())
                {
                    result.Add(p.GetAssembly());
                }
                else
                {
                    int id = p.GetAssembly().Identifier.ID;
                    if (asmIDS.Contains(id)) continue;

                    result.Add(p.GetAssembly());
                    asmIDS.Add(id);
                }
            }

            return result.AsEnumerable();
        }
    }
}
