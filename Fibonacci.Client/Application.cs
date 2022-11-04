using Fibonacci.Client.Tasks;

namespace Fibonacci.Client;

public class Application
{
    private readonly IListenerService _listenerService;

    public Application( IListenerService listenerService)
    {
        _listenerService = listenerService;
    }

    public void Run()
    {
        Console.WriteLine("Please pass in number of parallel requests");
        var n = Console.ReadLine();

        int.TryParse(n, out var nParallelRequests);
        _listenerService.StartAsync(nParallelRequests);
    }
}