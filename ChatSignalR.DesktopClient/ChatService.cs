using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ChatSignalR.DesktopClient
{
    public class ChatService : IChatService
    {
        public Action<string> OnStatusReceived;

        private HubConnection connection;
        private IHubProxy hubProxy;

        public Task Connect()
        {
            connection = new HubConnection("http://localhost:8090/");
            var connectionTask = connection.Start();
            hubProxy = connection.CreateHubProxy("ChatHub");
            hubProxy.On<string>("setStatus", message => OnStatusReceived?.Invoke(message));
            return connectionTask;
        }

        public Task<string> Send(string userName, string message)
        {
            return hubProxy.Invoke<string, string>("Send", _ => { }, userName, message);
        }

        public void Disconnect()
        {
            connection?.Dispose();
        }
    }
}