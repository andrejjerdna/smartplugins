using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
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
        /// Получить коробку AABB сборки
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static AABB GetAabb(this Assembly asm)
        {
            var main = (asm.GetMainPart() as Part).GetAABB();
            foreach (var p in asm.GetSecondaries())
            {
                main += (p as Part).GetAABB();
            }
            return main;
        }
    }
}
