using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Client.Bll.Services;
using Fibonacci.Client.Tasks;
using Serilog;
using Exception = System.Exception;

namespace Fibonacci.Client;

public class Application
{
    private readonly IListenerService _listenerService;
    private readonly IApiService _apiService;
    private readonly ILogger _logger;

    public Application(IListenerService listenerService, IApiService apiService, ILogger logger)
    {
        _listenerService = listenerService;
        _apiService = apiService;
        _logger = logger;
    }

    public void Run()
    {
        Console.WriteLine("Please pass in number of parallel requests and press enter to start");

        var n = Console.ReadLine();
        int nParallelRequests;

        while (!int.TryParse(n, out nParallelRequests))
        {
        }

        try
        {
            _listenerService.StartAsync().GetAwaiter().GetResult();
            Console.WriteLine($"Press enter to send {nParallelRequests} initial requests and start processing");
            Console.ReadLine();
            Console.WriteLine($"Press enter to stop");

            SendInitialRequests(nParallelRequests).GetAwaiter().GetResult();

            Console.ReadLine();
            _listenerService.Stop();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error starting application");
            Console.ReadLine();
        }
    }

    private async Task SendInitialRequests(int nRequests)
    {
        _logger.Debug($"Sending initial {nRequests}");

        var initialRequestsTasks = new List<Task>();

        for (int taskId = 0; taskId < nRequests; taskId++)
        {
            var request = new CalculateNextFibonacciRequest()
            {
                Value = "0",
                PreviousValue = "0",
                TaskId = taskId
            };

            initialRequestsTasks.Add(_apiService.SendCalculationCommandAsync(request));
            _logger.Debug($"{DateTime.UtcNow} --> {request.TaskId}:{request.Value}");
        }

        await Task.WhenAll(initialRequestsTasks);
    }
}