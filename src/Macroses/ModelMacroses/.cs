using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Model;

namespace SmartMacros
{
    public class RebarSequenceNumbering
    {
        public static void Run()
        {
            var model = new Model();
            if (!model.GetConnectionStatus()) return;

            var modelObjects = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();
            
            if (modelObjects.GetSize() == 0) { MessageBox.Show("Не выбрано ни одного объекта!"); return; }

            var rebarDataList = new List<Tuple<string, string, int>>();

            rebarDataList.Clear();

            while (modelObjects.MoveNext())
            {
                if (modelObjects.Current is Assembly assembly)
                {
                    var detailList = new ArrayList();
                    detailList.Add(assembly.GetMainPart());
                    detailList.AddRange(assembly.GetSecondaries());

                    foreach (Part part in detailList)
                    {
                        var reinforcements = part.GetReinforcements();

                        while (reinforcements.MoveNext())
                        {
                            var rebar = reinforcements.Current;

                            var rebarPrefix = "";
                            rebar.GetReportProperty("PREFIX", ref rebarPrefix);

                            if (rebarPrefix != "Ф")
                            {
                                var rebarPos = "";
                                var castUnitPos = "";

                                rebar.GetReportProperty("REBAR_POS", ref rebarPos);
                                rebar.GetReportProperty("CAST_UNIT_POS", ref castUnitPos);

                                rebarDataList.Add(new Tuple<string, string, int>(castUnitPos, rebarPos, rebar.Identifier.ID));
                            }

                            else
                            {
                                rebar.Select();
                                rebar.SetUserProperty("REBAR_SEQ_NO", 0);
                                rebar.Modify();
                            }
                        }
                    }

                }
            }

            rebarDataList.Sort();
            rebarDataList.Reverse();

            WriteUDA(rebarDataList, model);

            model.CommitChanges();

            MessageBox.Show("Стержни пронумерованы!");
        }

        public static void WriteUDA(List<Tuple<string, string, int>> rebarDataList, Model model)
        {
            var rebarNo = 1;
            var actualRebarNo = "";
            var actualCastUnit = "";

            for (int i = 0; i < rebarDataList.Count; i++)
            {

                if (rebarDataList[i].Item1 != actualCastUnit) //new cast unit
                {
                    actualCastUnit = rebarDataList[i].Item1;
                    actualRebarNo = rebarDataList[i].Item2;
                    rebarNo = 1;
                }
                else if (rebarDataList[i].Item2 != actualRebarNo)//another RebarNo
                {
                    rebarNo++;
                    actualRebarNo = rebarDataList[i].Item2;
                }

                ModelObject rebarObject = model.SelectModelObject(new Identifier(rebarDataList[i].Item3));

                rebarObject.SetUserProperty("REBAR_SEQ_NO", rebarNo);
            }
        }
    }
}
