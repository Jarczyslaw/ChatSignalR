using TopShelfServices;

namespace ChatSignalR.Host
{
    internal static class Program
    {
        private static void Main()
        {
            var serviceRunner = new ServiceRunner<ChatService>();
            serviceRunner.Run();
        }
    }
}