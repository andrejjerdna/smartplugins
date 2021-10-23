using System.Collections.Generic;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using tsm = Tekla.Structures.Model;

namespace TestForm
{
    public class CheckMarks
    {
        private tsm.Model _model;
        private DrawingHandler _drawinghandler;

        private List<string> _allmarksMK = new List<string>();
        private List<string> _allmarksID = new List<string>();
        private List<string> _planmarksMK = new List<string>();
        private List<string> _planmarksID = new List<string>();

        private new List<Task> _tasks = new List<Task>();

        public CheckMarks()
        {
            _model = new tsm.Model();
            _drawinghandler = new DrawingHandler();
        }

        public void Run()
        {
            if (!_model.GetConnectionStatus() || !_drawinghandler.GetConnectionStatus())
                return;

            var picker = _drawinghandler.GetPicker();

            ViewBase view = null;
            var prompt = "Pick View";
            DrawingObject p = null;
            picker.PickObject(prompt, out p, out view);

            var types = new[] { typeof(Part), typeof(Mark) };

            var drawingObjects = view.GetObjects(types);

            while (drawingObjects.MoveNext())
            {
                if (drawingObjects.Current is Part part)
                {
                    _tasks.Add(Task.Factory.StartNew(() =>
                    {
                        CheckPart(part, _allmarksMK, _allmarksID);
                    }));
                    continue;
                }

                if (drawingObjects.Current is Mark mark)
                {
                    _tasks.Add(Task.Factory.StartNew(() =>
                    {
                        var relatedObjects = mark.GetRelatedObjects(new[] { typeof(Part) });

                        while (relatedObjects.MoveNext())
                        {
                            if (relatedObjects.Current is Part partFromMark)
                                CheckPart(partFromMark, _planmarksMK, _planmarksID);
                        }
                    }));
                }
            }

            Task.WaitAll(_tasks.ToArray());
        }

        private void CheckPart(Part part, List<string> marks, List<string> marksId)
        {
            var partModel = _model.SelectModelObject(part.ModelIdentifier) as tsm.Part;

            if (partModel == null)
                return;

            var assembly = partModel.GetAssembly();
            var assemblyType = assembly.GetAssemblyType();

            if (assemblyType == tsm.Assembly.AssemblyTypeEnum.STEEL_ASSEMBLY)
            {
                var checkPositions = CheckPositions(assembly, partModel);

                if (checkPositions)
                {
                    marks.Add(partModel.GetPartMark());
                    marksId.Add("id" + partModel.Identifier.GUID);
                }
            }
        }

        private bool CheckPositions(tsm.Assembly assembly, tsm.Part partModel)
        {
            var partposition = "";
            partModel.GetReportProperty("PART_POS", ref partposition);
            var assposition = "";
            assembly.GetReportProperty("ASSEMBLY_POS", ref assposition);

            return partposition == assposition;
        }
    }
}
