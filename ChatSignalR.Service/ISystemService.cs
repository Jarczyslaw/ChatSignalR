namespace ChatSignalR.Service
{
    public interface ISystemService
    {
        void OnStart();

        void OnStop();

        void OnPause();

        void OnContinue();
    }
}