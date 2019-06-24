using System;
using System.Windows.Forms;

namespace ChatSignalR.DesktopClient
{
    public static class ThreadUtils
    {
        public static void SafeInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}