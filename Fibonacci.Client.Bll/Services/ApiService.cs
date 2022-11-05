using Fibonacci.Api.Contracts.ExposedApis;
using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Api.Contracts.Responses;
using Microsoft.Extensions.Configuration;
using Refit;
using Serilog;

namespace Fibonacci.Client.Bll.Services;

public class ApiService : IApiService
{
    private readonly ILogger _logger;
    private readonly IFibonacciApi _client;

    public ApiService(IConfiguration configuration, ILogger logger)
    {
        _logger = logger;
        var section = configuration.GetSection("FibonacciApi");
        var host = section["Host"];
        var port = section["Port"];

        var apiUri = $"https://{host}:{port}";
        _client = RestService.For<IFibonacciApi>(apiUri);
    }

    public async Task<CalculateCommandAcceptedResponse> SendCalculationCommandAsync(CalculateNextFibonacciRequest request)
    {
        try
        {
            var response = await _client.CalculateNextFibonacciRpc(request);
            return response;
        }
        catch (Exception ex)
        {
            _logger.Error("Error calling API service", ex);
            throw;
        }
    }
}