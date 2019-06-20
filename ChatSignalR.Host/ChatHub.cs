using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatSignalR.Host
{
    public class ChatHub : Hub
    {
        public readonly static IHubConnectionContext<dynamic> HubClients = GlobalHost.ConnectionManager.GetHubContext<ChatHub>().Clients;
        public readonly static HashSet<string> Connections = new HashSet<string>();

        private void SendServerMessage(string message)
        {
            Send("[SERVER]", message);
        }

        public override Task OnConnected()
        {
            Connections.Add(Context.ConnectionId);
            SendServerMessage($"new connection - {Context.ConnectionId}");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Connections.Remove(Context.ConnectionId);
            SendServerMessage($"connection closed - {Context.ConnectionId}");
            return base.OnDisconnected(stopCalled);
        }

        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        public void SendStatus(string message)
        {
            Clients.All.setStatus(message);
        }
    }
}