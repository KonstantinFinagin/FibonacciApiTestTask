using System.Numerics;
using Fibonacci.Calculator.Services;
using Fibonacci.Common.Exceptions;
using Xunit;

namespace Fibonacci.Calculator.Tests
{
    public class FibonacciCalculatorTests
    {
        private readonly FibonacciCalculatorService _service;

        public FibonacciCalculatorTests()
        {
            _service = new FibonacciCalculatorService();
        }

        [Theory]
        [InlineData("0", true)]
        [InlineData("1", true)]
        [InlineData("2", true)]
        [InlineData("3", true)]
        [InlineData("4", false)]
        [InlineData("5", true)]
        [InlineData("13", true)]
        [InlineData("14", false)]
        [InlineData("63245986", true)]
        [InlineData("63245987", false)]

        [InlineData("160500643816367088", true)]
        [InlineData("160500643816367089", false)]

        [InlineData("14691098406862188148944207245954912110548093601382197697835", true)]
        [InlineData("14691098406862188148944207245954912110548093601382197697836", false)]

        [InlineData("222232244629420445529739893461909967206666939096499764990979600", true)]
        [InlineData("222232244629420445529739893461909967206666939096499764990979605", false)]
        public void IsFibonacci(string number, bool isFibonacci)
        {
            BigInteger n = BigInteger.Parse(number);
            Assert.Equal(isFibonacci, _service.IsFibonacci(n));
        }

        [Theory]
        [InlineData("222232244629420445529739893461909967206666939096499764990979605000000000000000000000")]

        public void ExceedsByteCountThrowsException(string number)
        {
            var exception = Assert.Throws<DomainException>(() => _service.IsFibonacci(number));
            Assert.Contains("Not supporting fibonacci length > 30 bytes", exception.Message);
        }

        [Theory]
        [InlineData("222232244629420445529739893461909967206666939096499764990979605000000000000000000000", null)]
        public void NextIsCalculated_ExceptionOnByteCount(string n, string prevN)
        {
            var exception = Assert.Throws<DomainException>(() => _service.CalculateNextFibonacci(n, prevN));
            Assert.Contains("Not supporting fibonacci length > 30 bytes", exception.Message);
        }

        [Theory]
        [InlineData("1", null)]
        public void NextIsCalculated_ExceptionOnAmbiguous1(string n, string prevN)
        {
            var exception = Assert.Throws<DomainException>(() => _service.CalculateNextFibonacci(n, prevN));
            Assert.Contains("For n = 1 previous value (0,1) is required", exception.Message);
        }

        [Theory]
        [InlineData("0", null, "1")]
        [InlineData("1","0", "1")]
        [InlineData("1","1", "2")]
        [InlineData("5", null, "8")]
        [InlineData("137347080577163115432025771710279131845700275212767467264610201", null, "222232244629420445529739893461909967206666939096499764990979600")]
        public void NextIsCalculated(string n, string prevN, string r)
        {
            var result = _service.CalculateNextFibonacci(n, prevN);
            Assert.Equal(r, result);
        }
    }
}