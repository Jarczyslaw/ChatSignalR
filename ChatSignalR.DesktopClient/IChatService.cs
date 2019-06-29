using System.Threading.Tasks;

namespace ChatSignalR.DesktopClient
{
    public interface IChatService
    {
        Task Connect(string userName);

        Task SendMessage(string userName, string message);

        void Disconnect();

        event StatusReceived OnStatusReceived;

        event MessageReceived OnMessageReceived;
    }
}