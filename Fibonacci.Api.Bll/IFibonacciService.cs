using Fibonacci.Api.Contracts;

namespace Fibonacci.Api.Bll
{
    public interface IFibonacciService
    {
        Task<CalculateResponse> GetNextFibonacciNumber(CalculateNextFibonacciRequest request);
    }
}
