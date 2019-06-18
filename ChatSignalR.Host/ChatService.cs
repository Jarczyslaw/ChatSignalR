using ChatSignalR.Host;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using System;
using TopShelfServices;

[assembly: OwinStartup(typeof(Startup))]

namespace ChatSignalR.Host
{
    public class ChatService : SystemServiceBase
    {
        private readonly PeriodicTask pingTask = new PeriodicTask();

        public override void OnStart()
        {
            base.OnStart();
            WebApp.Start("http://localhost:8090");
            pingTask.Start(TimeSpan.FromSeconds(5), () =>
            {
                ChatHub.HubClients.All.addMessage("[SERVER]", $"Ping from server at {DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}");
                LogInfo("Ping sent!");
            });
        }

        public override void OnStop()
        {
            LogInfo($"{serviceName} stopping...");
            pingTask.Stop();
            base.OnStop();
        }
    }
}