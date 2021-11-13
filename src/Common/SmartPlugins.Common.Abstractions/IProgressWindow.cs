using System.Windows.Threading;

namespace SmartPlugins.Common.Abstractions
{
    public interface IProgressWindow
    {
        void Show();
        void Close();
    }
}
