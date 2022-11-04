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
    }
}