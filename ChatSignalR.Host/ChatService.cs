using ChatSignalR.Host;
using ChatSignalR.Misc;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using System;
using TopShelfServices;

[assembly: OwinStartup(typeof(Startup))]

namespace ChatSignalR.Host
{
    public class ChatService : SystemServiceBase
    {
        private readonly IPeriodicTask periodicTask;
        private IDisposable webApp;

        public ChatService(IPeriodicTask periodicTask)
        {
            this.periodicTask = periodicTask;
        }

        public override void OnStart()
        {
            base.OnStart();
            webApp = WebApp.Start("http://localhost:8090");
            periodicTask.Start(TimeSpan.FromSeconds(5), (_) =>
            {
                ChatHub.HubClients.All.addMessage("[SERVER]", $"Ping from server at {DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}");
                LogInfo("ping sent!");
            });
        }

        public override void OnStop()
        {
            LogInfo("stopping...");
            webApp.Dispose();
            periodicTask.Stop();
            base.OnStop();
        }
    }
}