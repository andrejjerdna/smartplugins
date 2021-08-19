using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.SmartTeklaModel
{
    public class GeneralMethods
    {
        /// <summary>
        /// Метод принимает имя атрибута и записывает его новое значение.
        /// </summary>
        /// <param name="modelObject"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        public static void SerUserAttribute(ModelObject modelObject, string attributeName, string attributeValue)
        {
            if (modelObject == null) return;

            modelObject.SetUserProperty(attributeName, attributeValue);
            modelObject.Modify();
        }
        /// <summary>
        /// Метод принимает имя атрибута, набор объектов и записывает им новое значение.
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        public static void SerUserAttribute(IEnumerable<ModelObject> objects, string attributeName, string attributeValue)
        {
            foreach (ModelObject obj in objects)
            {
                SerUserAttribute(obj, attributeName, attributeValue);
            }
        }
        /// <summary>
        /// Сварка двух дуталей.
        /// </summary>
        /// <param name="modelObject1">Объект модели 1. Главная деталь.</param>
        /// <param name="modelObject2">Объект модели 2.</param>
        /// <param name="kf">Катет шва.</param>
        public static void CreateWeld(ModelObject modelObject1, ModelObject modelObject2, double kf)
        {
            var weld = new Weld();

            weld.MainObject = modelObject1;

            weld.SecondaryObject = modelObject2;

            weld.TypeAbove = BaseWeld.WeldTypeEnum.WELD_TYPE_FILLET;
            weld.SizeAbove = kf;
            weld.SizeBelow = kf;
            weld.ShopWeld = true;
            weld.PrefixAboveLine = "";
            weld.PrefixBelowLine = "";

            weld.Insert();
        }
        public static string GetPlatesProfile(double b, double h)
        {
            var B = Math.Round(b, 1);
            var H = Math.Round(h, 1);

            return B.ToString() + "*" + H.ToString();
        }
        public static string GetRoundProfile(double d)
        {
            var D = Math.Round(d, 1);

            return "D" + D.ToString();
        }
        public static string GetConusProfile(double r1, double r2)
        {
            var R1 = Math.Round(r1, 1);
            var R2 = Math.Round(r2, 1);

            return "ELD" + R1.ToString() + "*" + R1.ToString() + "*" + R2.ToString() + "*" + R2.ToString();
        }
        public static string GetShereProfile(double r1)
        {
            var R1 = Math.Round(r1, 1);

            return "SPHERE" + R1.ToString();
        }
    }
}
