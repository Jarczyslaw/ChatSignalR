using System;
using System.Threading;

namespace ChatSignalR.Misc
{
    public interface IPeriodicTask
    {
        void Start(TimeSpan interval, Action<CancellationToken> action);

        void Stop();
    }
}