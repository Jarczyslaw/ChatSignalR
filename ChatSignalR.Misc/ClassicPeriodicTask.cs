using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatSignalR.Misc
{
    public class ClassicPeriodicTask : CancellableTask, IPeriodicTask
    {
        private Task task;

        public void Start(TimeSpan interval, Action action)
        {
            Stop();

            task = Task.Run(async () =>
            {
                var stamp = DateTime.Now;
                while (!token.IsCancellationRequested)
                {
                    if (DateTime.Now - stamp >= interval)
                    {
                        action();
                        stamp = DateTime.Now;
                    }

                    await Task.Delay(TimeSpan.FromMilliseconds(100))
                        .ConfigureAwait(false);
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