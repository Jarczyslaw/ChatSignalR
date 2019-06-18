using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatSignalR.Host
{
    public class PeriodicTask
    {
        private CancellationTokenSource cancelletionTokenSource;
        private CancellationToken token;
        private Task task;

        public PeriodicTask()
        {
            cancelletionTokenSource = new CancellationTokenSource();
            token = cancelletionTokenSource.Token;
        }

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
            if (task != null && cancelletionTokenSource != null)
            {
                cancelletionTokenSource.Cancel();
                task.Wait();
            }
        }
    }
}