using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ChatSignalR.DesktopClient
{
    public delegate void StatusReceived(string status);

    public delegate void MessageReceived(string userName, string message);

    public class ChatService : IChatService
    {
        public event StatusReceived OnStatusReceived;

        public event MessageReceived OnMessageReceived;

        private HubConnection connection;
        private IHubProxy hubProxy;

        public async Task Connect(string userName)
        {
            connection = new HubConnection("http://localhost:8090/");
            hubProxy = connection.CreateHubProxy("ChatHub");
            await connection.Start().ConfigureAwait(false);
            await InitializeConnection(userName).ConfigureAwait(false);
        }

        private async Task InitializeConnection(string userName)
        {
            if (connection.State == ConnectionState.Connected)
            {
                AttachCallbacks();
                await hubProxy.Invoke("initialize", userName).ConfigureAwait(false);
            }
        }

        private void AttachCallbacks()
        {
            hubProxy.On<string>("setStatus", status => OnStatusReceived?.Invoke(status));
            hubProxy.On<string, string>("sendMessage", (userName, message) => OnMessageReceived?.Invoke(userName, message));
        }

        public Task SendMessage(string userName, string message)
        {
            return hubProxy.Invoke("sendMessage", userName, message);
        }

        public void Disconnect()
        {
            if (connection?.State == ConnectionState.Connected)
            {
                connection.Stop(TimeSpan.FromSeconds(1));
            }
        }
    }
}