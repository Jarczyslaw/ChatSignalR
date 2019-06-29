using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatSignalR.Host
{
    public class ChatHub : Hub
    {
        public readonly static IHubConnectionContext<dynamic> HubClients = GlobalHost.ConnectionManager.GetHubContext<ChatHub>().Clients;
        public readonly static Dictionary<string, string> Connections = new Dictionary<string, string>();

        private void SendServerMessage(string message)
        {
            SendMessage("[SERVER]", message);
        }

        public override Task OnConnected()
        {
            Connections.Add(Context.ConnectionId, string.Empty);
            SendServerMessage($"new connection - {Context.ConnectionId}");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Connections.Remove(Context.ConnectionId);
            SendServerMessage($"connection closed - {Context.ConnectionId}");
            SendServerMessage($"{Connections[Context.ConnectionId]} disconnected");
            return base.OnDisconnected(stopCalled);
        }

        public void Initialize(string name)
        {
            Connections[Context.ConnectionId] = name;
            SendServerMessage($"{name} connected");
        }

        public void SendMessage(string name, string message)
        {
            Clients.All.sendMessage(name, message);
        }

        public void SetStatus(string status)
        {
            Clients.All.setStatus(status);
        }
    }
}