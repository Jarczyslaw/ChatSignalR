using System.Threading.Tasks;

namespace ChatSignalR.DesktopClient
{
    public interface IChatService
    {
        Task Connect();

        Task<string> Send(string userName, string message);

        void Disconnect();

        event StatusReceived OnStatusReceived;

        event MessageReceived OnMessageReceived;
    }
}