using SmartWPFElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private List<DrawingData> _drawingDatas { get; set; }
        private Timer _timer { get; set; }

        public Tekla.Structures.Drawing.UI.Events Events;

        public TeklaDrawingsObserver()
        {
            _drawingHandler = new DrawingHandler();
            _drawingDatas = new List<DrawingData>();

            Events = new Tekla.Structures.Drawing.UI.Events();

            _timer = new Timer();

            Events.DrawingLoaded += GetCurrentDrawing;
            Events.DrawingEditorClosed += DrawingClosed;
            Events.Register();
        }

        private void GetCurrentDrawing()
        {
            _timer.AutoReset = true;
            _timer.Start();

            _drawingCurrent = _drawingHandler.GetActiveDrawing();
        }

        private void DrawingClosed()
        {
            _timer.Stop();

            if (_drawingCurrent == null)
                return;

            var identifier = DatabaseObjectExtensions.GetIdentifier(_drawingCurrent);

            var search = _drawingDatas.Select(dd => dd.DrawingID).Contains(identifier.ID);

            if (search)
            {
                var drawing = _drawingDatas.Where(dd => dd.DrawingID == identifier.ID).First();

                drawing.WorkTime += _timer.Interval;
            }
            else
            {
                var drawingData = new DrawingData
                {
                    DrawingID = identifier.ID,
                    WorkTime = _timer.Interval
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
