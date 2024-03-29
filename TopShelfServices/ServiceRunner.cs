﻿using System;
using Topshelf;

namespace TopShelfServices
{
    public class ServiceRunner
    {
        public void Run<T>(Func<T> constructFunc)
            where T : class, ISystemService
        {
            HostFactory.Run(serviceConfig =>
            {
                var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                serviceConfig.SetServiceName(assemblyName);
                serviceConfig.SetDisplayName(assemblyName);
                serviceConfig.SetDescription($"{assemblyName} - {typeof(T).Name}");

                serviceConfig.UseNLog();

                serviceConfig.Service<T>(serviceInstance =>
                {
                    serviceInstance.ConstructUsing(constructFunc);
                    serviceInstance.WhenStarted(e => e.OnStart());
                    serviceInstance.WhenPaused(e => e.OnPause());
                    serviceInstance.WhenContinued(e => e.OnContinue());
                    serviceInstance.WhenStopped(e => e.OnStop());
                });

                serviceConfig.EnableServiceRecovery(recoveryOption =>
                {
                    recoveryOption.OnCrashOnly();
                    recoveryOption.RestartService(TimeSpan.Zero);
                    recoveryOption.RestartService(TimeSpan.FromMinutes(1));
                });

                serviceConfig.StartAutomatically();
            });
        }
    }
}