using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures;
using TSM = Tekla.Structures.Model;
using TSD = Tekla.Structures.Drawing;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace SmartMacros.ModelMacros
{
    public class MainPartByWeight
    {
        public static void Run()
        {
            var model = new Model();
            if (model.GetConnectionStatus() == false) return;

            var selectAssembly = new TSM.UI.ModelObjectSelector().GetSelectedObjects().ToIEnumerable<Assembly>().ToList();

            var countAssembly = selectAssembly.Count;

            if (countAssembly == 0)
            {
                MessageBox.Show("Не выбрано ни одной сборки!");
                return;
            }

            var countChange = 0;

            foreach(Assembly assembly in selectAssembly)
            {
                var detail = CheckAssembly(assembly);

                if (detail != null)
                {
                    assembly.SetMainPart(detail);
                    assembly.Modify();
                    countChange += 1;
                }
            }

            MessageBox.Show("Изменено главных деталей сборок: " + countChange.ToString());
            model.CommitChanges();
        }

        private static Part CheckAssembly(Assembly assembly)
        {
            Part result = null;

            var mainPart = assembly.GetMainPart();

            var weightMainPart = 0.0;
            mainPart.GetReportProperty("WEIGHT", ref weightMainPart);

            var assemblyDetails = assembly.GetSecondaries();

            foreach(ModelObject modelObject in assemblyDetails)
            {
                if (modelObject is Part part)
                {
                    var weightCurrentDetail = 0.0;
                    part.GetReportProperty("WEIGHT", ref weightCurrentDetail);

                    if (weightCurrentDetail > weightMainPart)
                    {
                        weightMainPart = weightCurrentDetail;
                        result = part;
                    }
                }
            }

            return result;
        }
    }
}
