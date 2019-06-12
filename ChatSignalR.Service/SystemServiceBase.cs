using System;
using Topshelf.Logging;

namespace ChatSignalR.Service
{
    public class SystemServiceBase : ISystemService, IDisposable
    {
        protected static LogWriter log;

        protected string serviceName;

        public SystemServiceBase()
        {
            serviceName = GetType().Name;
            log = HostLogger.Get(serviceName);
        }

        public void Dispose()
        {
            OnStop();
        }

        public void OnContinue()
        {
            log.InfoFormat($"{serviceName} continued");
        }

        public void OnPause()
        {
            log.InfoFormat($"{serviceName} paused");
        }

        public virtual void OnStart()
        {
            log.InfoFormat($"{serviceName} started");
        }

        public virtual void OnStop()
        {
            log.InfoFormat($"{serviceName} stopped");
        }
    }
}