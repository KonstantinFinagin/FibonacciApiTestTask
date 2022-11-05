namespace Fibonacci.Client.Tasks
{
    public interface IListenerService
    {
        Task StartAsync();

        void Stop();
    }
}
