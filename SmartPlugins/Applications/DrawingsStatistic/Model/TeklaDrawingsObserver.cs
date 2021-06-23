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
        private Drawing _drawingCurrent { get; set; }
        private ObservableCollection<DrawingData> _drawingDatas { get; set; }
        private Stopwatch _stopwatch { get; set; }

        public Tekla.Structures.Drawing.UI.Events Events;

        public TeklaDrawingsObserver(ObservableCollection<DrawingData> drawingDatas)
        {
            _drawingHandler = new DrawingHandler();
            _drawingDatas = drawingDatas;

            Events = new Tekla.Structures.Drawing.UI.Events();

            _stopwatch = new Stopwatch();

            Events.DrawingLoaded += GetCurrentDrawing;
            Events.DrawingEditorClosed += DrawingClosed;
            Events.Register();
        }

        public async Task<bool> StartObserver()
        {
            DrawingClosed();
            return true;
        }

        private void GetCurrentDrawing()
        {
            _stopwatch.Start();

            _drawingCurrent = _drawingHandler.GetActiveDrawing();
        }

        private void DrawingClosed()
        {
            _stopwatch.Stop();

            if (_drawingCurrent == null)
                return;

            var identifier = DatabaseObjectExtensions.GetIdentifier(_drawingCurrent);

            var search = _drawingDatas.Select(dd => dd.DrawingID).Contains(identifier.ID);

            if (search)
            {
                var drawing = _drawingDatas.Where(dd => dd.DrawingID == identifier.ID).First();

                drawing.WorkTime += _stopwatch.Elapsed.TotalSeconds;
            }
            else
            {
                var drawingData = new DrawingData
                {
                    DrawingID = identifier.ID,
                    WorkTime = _stopwatch.Elapsed.TotalSeconds
                };

                _drawingDatas.Add(drawingData);
            }

            _drawingCurrent = null;

        }

        public IEnumerable<DrawingData> DrawingDatas()
        {
            return _drawingDatas;
        }
    }
}
