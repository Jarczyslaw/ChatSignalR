using System;
using System.Windows.Forms;
using Unity;

namespace ChatSignalR.DesktopClient
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var container = new UnityContainer();
            container.RegisterSingleton<IChatService, ChatService>();

            SetupApplication();
            Application.Run(container.Resolve<MainForm>());
        }

        private static void SetupApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => UnhandledExceptionHandler(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => UnhandledExceptionHandler((Exception)e.ExceptionObject);
        }

        private static void UnhandledExceptionHandler(Exception exception)
        {
            MessageBoxUtils.ShowException(exception);
        }
    }
}