using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatSignalR.Misc
{
    public class ClassicPeriodicTask : CancellableTask, IPeriodicTask
    {
        private Task task;

        public void Start(TimeSpan interval, Action<CancellationToken> action)
        {
            Stop();
            task = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(interval)
                        .ConfigureAwait(false);
                    action(token);
                }
            }, token);
        }

        public void Stop()
        {
            if (task != null && cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                task.Wait();
            }
        }
    }
}