using ChatSignalR.Service;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Topshelf.Logging;

[assembly: OwinStartup(typeof(Startup))]

namespace ChatSignalR.Service
{
    public class Chat : IDisposable
    {
        public static readonly LogWriter Log = HostLogger.Get<Chat>();

        private CancellationTokenSource cancelletionTokenSource;
        private CancellationToken token;
        private Task task;

        public void Dispose()
        {
        }

        public void OnStart(string[] args)
        {
            Log.Info("ChatSignalR: OnStart");
            WebApp.Start("http://localhost:8090");

            cancelletionTokenSource = new CancellationTokenSource();
            token = cancelletionTokenSource.Token;
            task = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(10))
                        .ConfigureAwait(false);
                    MyHub.HubClients.All.addMessage("SERVER", "PING FROM SERVER");
                    Log.Info("Ping sent!");
                }
            }, token);
        }

        public void OnStop()
        {
            Log.Info("ChatSignalR: OnStop");
            cancelletionTokenSource.Cancel();
            task.Wait();
            Log.Info("ChatSignalR: Stopped");
        }
    }
}