using Fibonacci.Api.Contracts;
using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Api.Contracts.Responses;

namespace Fibonacci.Api.Bll
{
    public interface IFibonacciService
    {
        Task<CalculateNextFibonacciResponse> CalculateNextFibonacciNumber(CalculateNextFibonacciRequest request);

        Task<CalculateCommandAcceptedResponse> CalculateNextFibonacciRpc(CalculateNextFibonacciRequest request);
    }
}
