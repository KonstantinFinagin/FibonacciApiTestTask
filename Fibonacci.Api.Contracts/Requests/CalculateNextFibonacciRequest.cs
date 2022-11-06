namespace Fibonacci.Api.Contracts.Requests
{
    public class CalculateNextFibonacciRequest
    {
        public int TaskId { get; set; }

        public string Value { get; set; } = null!;

        public string PreviousValue { get; set; } = null!;
    }
}
