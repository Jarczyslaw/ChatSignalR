using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ChatSignalR.Host
{
    public class ChatHub : Hub
    {
        public readonly static IHubConnectionContext<dynamic> HubClients = GlobalHost.ConnectionManager.GetHubContext<ChatHub>().Clients;

        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
    }
}