namespace Fibonacci.Client.Listener
{
    public interface IListenerService
    {
        Task StartAsync();

        void Stop();
    }
}
