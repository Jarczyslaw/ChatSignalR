using System.Threading;

namespace ChatSignalR.Misc
{
    public abstract class CancellableTask
    {
        protected CancellationTokenSource cancellationTokenSource;
        protected CancellationToken token;

        public CancellableTask()
        {
            cancellationTokenSource = new CancellationTokenSource();
            token = cancellationTokenSource.Token;
        }
    }
}