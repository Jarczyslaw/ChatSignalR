using ChatSignalR.Misc;
using TopShelfServices;
using Unity;

namespace ChatSignalR.Host
{
    internal static class Program
    {
        private static void Main()
        {
            var container = RegisterDependencies();
            var serviceRunner = new ServiceRunner();
            serviceRunner.Run(() => container.Resolve<ChatService>());
        }

        private static IUnityContainer RegisterDependencies()
        {
            var container = new UnityContainer();
            container.RegisterType<IPeriodicTask, ObservablePeriodicTask>();
            return container;
        }
    }
}