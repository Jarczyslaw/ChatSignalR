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

        private string ConnectionId => Context.ConnectionId;

        public override Task OnConnected()
        {
            if (!Connections.ContainsKey(ConnectionId))
            {
                Connections.Add(ConnectionId, string.Empty);
            }
            return base.OnConnected();
        }

        public void Initialize(string name)
        {
            if (Connections.ContainsKey(ConnectionId))
            {
                Connections[ConnectionId] = name;
                SendServerMessage($"{name} connected (connectionID: {ConnectionId})");
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (Connections.TryGetValue(ConnectionId, out string name))
            {
                SendServerMessage($"{name} disconnected (connectionID: {ConnectionId})");
                Connections.Remove(ConnectionId);
            }
            return base.OnDisconnected(stopCalled);
        }

        public void SendMessage(string name, string message)
        {
            Clients.All.sendMessage(name, message);
        }

        public void SetStatus(string status)
        {
            Clients.All.setStatus(status);
        }

        private void SendServerMessage(string message)
        {
            SendMessage("[SERVER]", message);
        }
    }
}