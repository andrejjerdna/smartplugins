using SmartPlugins.Common.Abstractions;
using System.Threading;
using static Tekla.Structures.Model.Operations.Operation;

namespace SmartPlugins.Macroses.Library
{
    public class MacroLogger : IProgressLogger
    {
        public void Write(string message)
        {
            var progress = new ProgressBar();
            progress.Display(1, "title", "message", "cancel..", " ");

            for (var i = 0; i < 1000; i++)
            {
                progress.SetProgress(i.ToString(), i / 100);
                Thread.Sleep(1000);
            }

            progress.Close();
        }
    }
}
