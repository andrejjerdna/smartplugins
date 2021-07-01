using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tsd = Tekla.Structures.Drawing;
using tsm = Tekla.Structures.Model;

namespace SmartExtensions
{
    public static class DrawingBoltExtensions
    {
        /// <summary>
        /// Получить болты из объектов чертежа
        /// </summary>
        /// <param name="doe"></param>
        /// <returns></returns>
        public static IEnumerable<tsd.Bolt> GetDrawingsBolts(this tsd.DrawingObjectEnumerator doe) 
            => doe.Get<tsd.Bolt>();

        /// <summary>
        /// Конвертация болтов из чертежа в объекты модели
        /// </summary>
        /// <param name="bolts"></param>
        /// <returns></returns>
        public static IEnumerable<tsm.BoltGroup> ToModelsBolts(this IEnumerable<tsd.Bolt> bolts)
        {
            return bolts.Select(bolt => bolt.ModelIdentifier.ToModel<tsm.BoltGroup>());
        }
    }
}
