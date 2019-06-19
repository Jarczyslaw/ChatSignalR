using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ChatSignalR.Misc
{
    public class ObservablePeriodicTask : CancellableTask, IPeriodicTask
    {
        private IDisposable subscription;

        public void Start(TimeSpan interval, Action action)
        {
            var observable = Observable.Interval(interval);
            subscription = observable.Subscribe(_ => Task.Run(action, token));
        }

        public void Stop()
        {
            subscription?.Dispose();
        }
    }
}