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
            webApp = WebApp.Start<Startup>("http://localhost:8090");
            periodicTask.Start(TimeSpan.FromSeconds(5), (_) =>
            {
                var serverTime = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
                var usersCount = ChatHub.Connections.Count;
                ChatHub.HubClients.All.setStatus($"Server time: {serverTime}, current users: {usersCount}");
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