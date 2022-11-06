namespace Fibonacci.Api.Contracts.Responses
{
    public class CalculateNextFibonacciResponse
    {
        public int TaskId { get; set; }

        public string Value { get; set; } = null!;

        public string PreviousValue { get; set; } = null!;
    }
}
