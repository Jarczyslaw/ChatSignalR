using System;
using Topshelf.Logging;

namespace TopShelfServices
{
    public class SystemServiceBase : ISystemService, IDisposable
    {
        protected string serviceName;
        private static LogWriter log;

        public SystemServiceBase()
        {
            serviceName = GetType().Name;
            log = HostLogger.Get(serviceName);
        }

        public void Dispose()
        {
            OnStop();
        }

        public virtual void OnContinue()
        {
            LogInfo("service continued");
        }

        public virtual void OnPause()
        {
            LogInfo("service paused");
        }

        public virtual void OnStart()
        {
            LogInfo("service started");
        }

        public virtual void OnStop()
        {
            LogInfo("service stopped");
        }

        protected void LogError(string message)
        {
            log.ErrorFormat($"{serviceName} - {message}");
        }

        protected void LogInfo(string message)
        {
            log.InfoFormat($"{serviceName} - {message}");
        }
    }
}