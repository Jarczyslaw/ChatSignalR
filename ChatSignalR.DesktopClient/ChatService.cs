using Microsoft.AspNet.SignalR.Client;
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

        public Task Connect()
        {
            connection = new HubConnection("http://localhost:8090/");
            hubProxy = connection.CreateHubProxy("ChatHub");
            hubProxy.On<string>("setStatus", status => OnStatusReceived?.Invoke(status));
            hubProxy.On<string, string>("addMessage", (userName, message) => OnMessageReceived?.Invoke(userName, message));
            return connection.Start();
        }

        public Task<string> Send(string userName, string message)
        {
            return hubProxy.Invoke<string, string>("addMessage", _ => { }, userName, message);
        }

        public void Disconnect()
        {
            connection?.Dispose();
        }
    }
}