using SmartPlugins.Common.Abstractions.Messages;
using System.Windows;

namespace SmartPlugins.Common.Core.Messages
{
    public class MessagesViewer
    {
        public static void Show(string message, MessageType messageType = MessageType.Info)
        {
            var messageBoxButton = GetMessageBoxButton();
            var messageBoxImage = GetMessageBoxImage(messageType);

            MessageBox.Show(message, "Smart macro", messageBoxButton, messageBoxImage);
        }

        private static MessageBoxButton GetMessageBoxButton() => MessageBoxButton.OK;

        private static MessageBoxImage GetMessageBoxImage(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Info: return MessageBoxImage.Information;
                case MessageType.Done: return MessageBoxImage.Information;
                case MessageType.Error: return MessageBoxImage.Error;
                default : return MessageBoxImage.None;
            }
        }
    }
}
