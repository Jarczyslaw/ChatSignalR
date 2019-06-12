namespace ChatSignalR.Service
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