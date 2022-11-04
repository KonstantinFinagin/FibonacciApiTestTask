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
<<<<<<< HEAD
        Console.WriteLine("Please pass in number of parallel requests");
        var n = Console.ReadLine();

        int.TryParse(n, out var nParallelRequests);
        _listenerService.StartAsync(nParallelRequests);
=======
>>>>>>> 68be9a5c854e6a9c8c4d015e174c4506935d7a90
    }
}