using System;
using System.Windows.Forms;

namespace ChatSignalR.DesktopClient
{
    public static class MessageBoxUtils
    {
        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowException(Exception exception)
        {
            MessageBox.Show($"Exception occured: {exception.Message + Environment.NewLine + exception.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfo(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}