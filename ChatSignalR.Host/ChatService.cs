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
        private readonly PingTask ping = new PingTask();

        public override void OnStart()
        {
            base.OnStart();
            WebApp.Start("http://localhost:8090");
            ping.Start(() =>
            {
                ChatHub.HubClients.All.addMessage("[SERVER]", $"Ping from server at {DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}");
                LogInfo("Ping sent!");
            });
        }

        public override void OnStop()
        {
            LogInfo($"{serviceName} stopping...");
            ping.Stop();
            base.OnStop();
        }
    }
}