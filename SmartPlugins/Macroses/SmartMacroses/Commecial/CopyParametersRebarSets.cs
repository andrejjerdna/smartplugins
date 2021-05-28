using SmartExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using ModelObjectSelector = Tekla.Structures.Model.UI.ModelObjectSelector;

namespace SmartMacroses
{
    public class CopyParametersRebarSets
    {
        public static void Run()
        {
           var model = new Model();

            var rebarSetOrigin = new Picker().PickObject(Picker.PickObjectEnum.PICK_ONE_REINFORCEMENT) as RebarSet;

            if (rebarSetOrigin == null)
                return;

            DialogResult result = MessageBox.Show("Выбраны сборки для применения свойств?", "Внимание", MessageBoxButtons.YesNoCancel);

            if (result != DialogResult.Yes)
                return;

            var rebarSets = new ModelObjectSelector().GetSelectedObjects().ToIEnumerable<RebarSet>();

            foreach(var rebarSet in rebarSets)
            {
                rebarSet.LayerOrderNumber = rebarSetOrigin.LayerOrderNumber;
                rebarSet.RebarProperties = rebarSetOrigin.RebarProperties;

                var count = 0;

                foreach (var guidLine in rebarSet.Guidelines)
                {
                    try
                    {
                        guidLine.Spacing = rebarSetOrigin.Guidelines[count].Spacing;
                    }
                    catch
                    {

                    }

                    count++;
                }

                rebarSet.Modify();
            }

            model.CommitChanges();

            MessageBox.Show("Выполнено!");
        }
    }
}
