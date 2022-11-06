using System;
using System.Numerics;
using Fibonacci.Common.Exceptions;
using Fibonacci.Common.Extensions;

namespace Fibonacci.Calculator.Services
{
    public class FibonacciCalculatorService : IFibonacciCalculatorService
    {
        private readonly Dictionary<BigInteger, BigInteger> _memo;

        // TODO can move to configuration
        private const int MaxBytes = 30;

        public FibonacciCalculatorService()
        {
            _memo = new Dictionary<BigInteger, BigInteger>();
            PrefillMemoTable();
        }

        private void PrefillMemoTable()
        {
            _memo.Add(1, new BigInteger(2));
            _memo.Add(2, new BigInteger(3));

            var current = new BigInteger(2);

            while (true)
            {
                var prev = _memo[current];
                var next = prev + current;

                _memo.Add(prev, next);
                
                current = prev;

                if (prev.GetByteCount() > MaxBytes + 1) break;
            }
        }

        public BigInteger CalculateNextFibonacci(BigInteger n, BigInteger? prevN = null)
        {
            var byteCount = n.GetByteCount();
            if (byteCount > MaxBytes)
            {
                throw new DomainException($"Not supporting fibonacci length > {MaxBytes} bytes");
            }

            if(n == BigInteger.Zero) return BigInteger.One;
            if(n != BigInteger.One) return CalculateFibonacciRecursive(n);

            if(prevN == BigInteger.Zero) return BigInteger.One;
            if(prevN == BigInteger.One) return new BigInteger(2);
            
            throw new DomainException("For n = 1 previous value (0,1) is required");
        }

        public string CalculateNextFibonacci(string n, string? prevN = null)
        {
            var parsedN = BigInteger.Parse(n);
            var parsedPrevN = !string.IsNullOrEmpty(prevN) ? BigInteger.Parse(prevN) : (BigInteger?)null;

            var response = CalculateNextFibonacci(parsedN, parsedPrevN);
            return response.ToString();
        }

        private BigInteger CalculateFibonacciRecursive(BigInteger n)
        {
            if (n < 2)
            {
                _memo.Add(n, BigInteger.One);
                return BigInteger.One;
            }

            if (_memo.ContainsKey(n))
            {
                return _memo[n];
            }

            var v = BigInteger.Add(
                CalculateFibonacciRecursive(n - 1),
                CalculateFibonacciRecursive(n - 2));

            _memo.Add(n, v);

            return v;
        }

        public bool IsFibonacci(BigInteger n)
        {
            var byteCount = n.GetByteCount();
            if (byteCount > MaxBytes)
            {
                throw new DomainException($"Not supporting fibonacci length > {MaxBytes} bytes");
            }

            if (_memo.ContainsKey(n)) return true;

            if (n.CompareTo(BigInteger.Zero) < 0) { return false; }

            var product = BigInteger.Pow(n, 2);
            var four = new BigInteger(4);

            product = BigInteger.Multiply(product, new BigInteger(5));

            return IsSquare(BigInteger.Add(product, four)) || IsSquare(BigInteger.Subtract(product, four));

        }

        public bool IsFibonacci(string n)
        {
            var parsedN = BigInteger.Parse(n);
            return IsFibonacci(parsedN);
        }

        private static bool IsSquare(BigInteger r)
        {
            var squareRoot = r.NewtonPlusSqrt();
            return BigInteger.Pow(squareRoot, 2) == r;
        }
    }
}
