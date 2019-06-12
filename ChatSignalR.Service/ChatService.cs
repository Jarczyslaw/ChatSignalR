using ChatSignalR.Service;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using System;

[assembly: OwinStartup(typeof(Startup))]

namespace ChatSignalR.Service
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
                log.InfoFormat("Ping sent!");
            });
        }

        public override void OnStop()
        {
            log.InfoFormat($"{serviceName} stopping...");
            ping.Stop();
            base.OnStop();
        }
    }
}