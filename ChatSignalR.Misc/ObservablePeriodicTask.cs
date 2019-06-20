using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChatSignalR.Misc
{
    public class ObservablePeriodicTask : CancellableTask, IPeriodicTask
    {
        private IDisposable subscription;

        public void Start(TimeSpan interval, Action<CancellationToken> action)
        {
            Stop();
            var observable = Observable.Interval(interval);
            subscription = observable.Subscribe(_ => Task.Run(() => action(token), token));
        }

        public void Stop()
        {
            if (subscription != null && cancellationTokenSource != null)
            {
                cancellationTokenSource?.Cancel();
                subscription?.Dispose();
            }
        }
    }
}