using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatSignalR.Host
{
    public class PingTask
    {
        private CancellationTokenSource cancelletionTokenSource;
        private CancellationToken token;
        private Task task;

        public PingTask()
        {
            cancelletionTokenSource = new CancellationTokenSource();
            token = cancelletionTokenSource.Token;
        }

        public void Start(Action pingAction)
        {
            task = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(10))
                        .ConfigureAwait(false);
                    pingAction();
                }
            }, token);
        }

        public void Stop()
        {
            cancelletionTokenSource.Cancel();
            task.Wait();
        }
    }
}