using ChatSignalR.Service;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using System;
using Topshelf.Logging;

[assembly: OwinStartup(typeof(Startup))]

namespace ChatSignalR.Service
{
    public class Chat : IDisposable
    {
        public static readonly LogWriter Log = HostLogger.Get<Chat>();

        public void Dispose()
        {
        }

        public void OnStart(string[] args)
        {
            Log.Info("ChatSignalR: In OnStart");
            WebApp.Start("http://localhost:8090");
        }

        public void OnStop()
        {
            Log.Info("ChatSignalR: In OnStop");
        }
    }
}