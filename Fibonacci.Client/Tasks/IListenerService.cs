namespace Fibonacci.Client.Tasks
{
    public interface IListenerService
    {
        Task StartAsync(int nParallelRequests);

        void Stop();
    }
}
