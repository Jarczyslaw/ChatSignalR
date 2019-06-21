namespace ChatSignalR.DesktopClient
{
    public interface IChatService
    {
        void Connect();

        void Send(string userName, string message);

        void Disconnect();
    }
}