using Fibonacci.Client.Tasks;
using Serilog;
using Exception = System.Exception;

namespace Fibonacci.Client;

public class Application
{
    private readonly IListenerService _listenerService;
    private readonly ILogger _logger;

    public Application(IListenerService listenerService, ILogger logger)
    {
        _listenerService = listenerService;
        _logger = logger;
    }

    public void Run()
    {
        Console.WriteLine("Please pass in number of parallel requests and press enter to start");
        Console.WriteLine("Then press enter again to stop");

        var n = Console.ReadLine();

        int.TryParse(n, out var nParallelRequests);

        try
        {
            _logger.Debug("Starting listener service");
            
            _listenerService.StartAsync().GetAwaiter().GetResult();

            Console.ReadLine();

            _listenerService.Stop();

            _logger.Information("Listener service stopped");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error starting application");
            Console.ReadLine();
        }
    }
}