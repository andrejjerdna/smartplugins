using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Drawing.UI;
using Tekla.Structures.Model;

namespace TestForm
{
    public class UserClass
    {
        public void Run()
        {
            List<string> allmarksMK = new List<string>();
            List<string> allmarksID = new List<string>();
            List<string> planmarksMK = new List<string>();
            List<string> planmarksID = new List<string>();

            DrawingHandler drawinghandler = new DrawingHandler();
            Picker picker = drawinghandler.GetPicker();
            ViewBase view = null;
            string prompt = "Pick View";
            DrawingObject p = null;
            picker.PickObject(prompt, out p, out view);

            Tekla.Structures.Drawing.View myview = (Tekla.Structures.Drawing.View)p;
            myview.Select();

            var types = new[] { typeof(Tekla.Structures.Drawing.Part), typeof(Tekla.Structures.Drawing.Mark) };

            DrawingObjectEnumerator mo = myview.GetAllObjects(types);
            {
                while (mo.MoveNext())
                {
                    if (mo.Current is Tekla.Structures.Drawing.Part)
                    {
                        //if (progress.Canceled())
                        //    break;

                        Tekla.Structures.Drawing.Part mypart = (Tekla.Structures.Drawing.Part)mo.Current;

                        Tekla.Structures.Identifier myIdentifier = mypart.ModelIdentifier;
                        Tekla.Structures.Model.ModelObject ModelSideObject = new Model().SelectModelObject(myIdentifier);
                        Beam partmodel = new Beam();
                        partmodel.Identifier = myIdentifier;
                        partmodel.Select();

                        //Ищу assembly type и мейн парту, чтобы только они добавлялись в список
                        Assembly assembly = partmodel.GetAssembly();
                        string partposition = "";
                        partmodel.GetReportProperty("PART_POS", ref partposition);
                        string assposition = "";
                        assembly.GetReportProperty("ASSEMBLY_POS", ref assposition);

                        //Добавляю марки в список
                        string myMK = partmodel.GetPartMark();
                        string myID = Convert.ToString(partmodel.Identifier.GUID);
                        if (assembly.GetAssemblyType() == 0 && assposition == partposition)
                        {
                            allmarksMK.Add(myMK);
                            allmarksID.Add("id" + myID);
                        }
                        //progress.SetProgress("Part", 50);

                    }
                    else if (mo.Current is Tekla.Structures.Drawing.Mark)
                    {
                        //if (progress.Canceled())
                        //    break;

                        Mark mymark = (Mark)mo.Current;

                        DrawingObjectEnumerator mo3 = mymark.GetRelatedObjects();
                        while (mo3.MoveNext())
                        {
                            if (mo3.Current is Tekla.Structures.Drawing.Part)
                            {
                                Tekla.Structures.Drawing.Part part2 = (Tekla.Structures.Drawing.Part)mo3.Current;
                                Tekla.Structures.Identifier myIdentifier2 = part2.ModelIdentifier;
                                Tekla.Structures.Model.ModelObject ModelSideObject2 = new Model().SelectModelObject(myIdentifier2);
                                Beam partmodel2 = new Beam();
                                partmodel2.Identifier = myIdentifier2;
                                partmodel2.Select();

                                //Ищу assembly type и мейн парту , чтобы только они добавлялись в список
                                Assembly assembly2 = partmodel2.GetAssembly();
                                string partposition2 = "";
                                partmodel2.GetReportProperty("PART_POS", ref partposition2);
                                string assposition2 = "";
                                assembly2.GetReportProperty("ASSEMBLY_POS", ref assposition2);

                                //Добавляю марку в список
                                string myMK2 = partmodel2.GetPartMark();
                                string myID2 = Convert.ToString(partmodel2.Identifier.GUID);
                                if ((assembly2.GetAssemblyType() == 0 && assposition2 == partposition2) && !mymark.Hideable.IsHidden && mymark.Attributes.Content.Count > 0)
                                {
                                    planmarksMK.Add(myMK2);
                                    planmarksID.Add("id" + myID2);
                                }
                            }
                        }
                        //progress.SetProgress("Mark", 50);
                    }
                }
            }
        }
    }
}
