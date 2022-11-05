using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Api.Contracts.Responses;

namespace Fibonacci.Client.Bll.Services
{
    public interface IApiService
    {
        Task<CalculateCommandAcceptedResponse> SendCalculationCommandAsync(CalculateNextFibonacciRequest request);
    }
}
