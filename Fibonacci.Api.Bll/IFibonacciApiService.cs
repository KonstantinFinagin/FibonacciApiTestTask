using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Api.Contracts.Responses;

namespace Fibonacci.Api.Bll
{
    public interface IFibonacciApiService
    {
        Task<CalculateNextFibonacciResponse> CalculateNextFibonacciNumber(CalculateNextFibonacciRequest request);

        Task<CalculateCommandAcceptedResponse> CalculateNextFibonacciRpc(CalculateNextFibonacciRequest request);
    }
}
