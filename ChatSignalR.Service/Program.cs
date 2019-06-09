using System;
using Topshelf;

namespace ChatSignalR.Service
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            HostFactory.Run(serviceConfig =>
            {
                var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                serviceConfig.SetServiceName(assemblyName);
                serviceConfig.SetDisplayName(assemblyName);
                serviceConfig.SetDescription(assemblyName + " is a simple web chat application.");

                serviceConfig.UseNLog();

                serviceConfig.Service<Chat>(serviceInstance =>
                {
                    serviceInstance.ConstructUsing(
                        () => new Chat());

                    serviceInstance.WhenStarted(
                        execute => execute.OnStart(null));

                    serviceInstance.WhenStopped(
                        execute => execute.OnStop());
                });

                TimeSpan delay = new TimeSpan(0, 0, 0, 60);
                serviceConfig.EnableServiceRecovery(recoveryOption =>
                {
                    recoveryOption.RestartService(delay);
                    recoveryOption.RestartService(delay);
                    recoveryOption.RestartComputer(delay, assemblyName + " computer reboot");
                });

                serviceConfig.StartAutomatically();
            });
        }
    }
}