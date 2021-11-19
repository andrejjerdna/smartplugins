using Tekla.Structures.Model.Operations;

namespace SmartPlugins.Common.TeklaLibrary.Messages
{
    /// <summary>
    /// Display messages in the status bar
    /// </summary>
    public class StatusBarMessage
    {
        /// <summary>
        /// Show a message in the status bar
        /// </summary>
        /// <param name="message"></param>
        public static void Show(string message)
        {
            Operation.DisplayPrompt(message);
        }
    }
}
