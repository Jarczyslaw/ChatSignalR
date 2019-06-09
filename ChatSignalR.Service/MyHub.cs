using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;

namespace ChatSignalR.Service
{
    public class MyHub : Hub
    {
        public readonly static IHubConnectionContext<dynamic> HubClients = GlobalHost.ConnectionManager.GetHubContext<MyHub>().Clients;

        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
    }
}