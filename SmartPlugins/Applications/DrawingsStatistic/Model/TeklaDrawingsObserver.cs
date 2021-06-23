using SmartWPFElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tekla.Structures.Drawing;
using Tekla.Structures.DrawingInternal;

namespace DrawingsStatistic.Model
{
    public class TeklaDrawingsObserver
    {
        private DrawingHandler _drawingHandler { get; }
        private Drawing _drawingPrevius { get; set; }
        private ObservableCollection<DrawingData> _drawingDatasForView { get; set; }
        private List<DrawingData> _drawingDatas { get; set; }
        private Stopwatch _stopwatch { get; set; }

        public Tekla.Structures.Drawing.UI.Events Events;

        public TeklaDrawingsObserver(ObservableCollection<DrawingData> drawingDatasForView)
        {
            _drawingHandler = new DrawingHandler();
            _drawingDatasForView = drawingDatasForView;

            Events = new Tekla.Structures.Drawing.UI.Events();

            _drawingDatas = new List<DrawingData>();
            _drawingDatas.AddRange(drawingDatasForView);

            _stopwatch = new Stopwatch();

            Events.DrawingLoaded += GetCurrentDrawing;
            Events.DrawingEditorClosed += GetCurrentDrawing;

            Events.Register();
        }

        private void GetCurrentDrawing()
        {
            GetDrawingPreviusTime(_drawingPrevius);

            var tempDrawing = _drawingHandler.GetActiveDrawing();

            if (tempDrawing == null)
                return;

            if (_drawingPrevius == null)
                    _drawingPrevius = _drawingHandler.GetActiveDrawing();

            if (tempDrawing.GetIdentifier().ID != _drawingPrevius.GetIdentifier().ID)
                _drawingPrevius = _drawingHandler.GetActiveDrawing();

            _stopwatch.Start();
        }

        private void GetDrawingPreviusTime(Drawing drawingPrevius)
        {
            if (drawingPrevius == null)
                return;

            _stopwatch.Stop();

            var search = _drawingDatas.FirstOrDefault(d => d.ID == drawingPrevius.GetIdentifier().ID);

            if (search == null)
            {
                var drawingData = new DrawingData
                {
                    ID = drawingPrevius.GetIdentifier().ID,
                    WorkTime = _stopwatch.Elapsed.TotalSeconds,
                    Name = drawingPrevius.Name
                };

                _drawingDatas.Add(drawingData);
            }
            else
            {
                search.WorkTime += _stopwatch.Elapsed.TotalSeconds;
            }

            _stopwatch.Reset();

            _drawingDatasForView.Clear();

            foreach (var data in _drawingDatas)
                _drawingDatasForView.Add(data);
        }
    }
}
