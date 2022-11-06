using System.Numerics;

namespace Fibonacci.Calculator.Services
{
    public interface IFibonacciCalculatorService
    {
        bool IsFibonacci(BigInteger n);

        bool IsFibonacci(string n);

        BigInteger CalculateNextFibonacci(BigInteger n, BigInteger? prevN = null);

        string CalculateNextFibonacci(string n, string? prevN = null);
    }
}
