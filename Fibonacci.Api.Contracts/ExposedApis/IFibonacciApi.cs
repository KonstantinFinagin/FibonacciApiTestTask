using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Api.Contracts.Responses;
using Refit;

namespace Fibonacci.Api.Contracts.ExposedApis
{
    public interface IFibonacciApi
    {
        [Get("/api/fibonacci/rpc")]
        Task<CalculateCommandAcceptedResponse> CalculateNextFibonacciRpc(CalculateNextFibonacciRequest request);
    }
}
