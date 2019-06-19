using System;

namespace ChatSignalR.Misc
{
    internal interface IPeriodicTask
    {
        void Start(TimeSpan interval, Action action);

        void Stop();
    }
}