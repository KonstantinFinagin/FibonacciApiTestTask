namespace Fibonacci.Api.Contracts.Responses
{
    public class CalculateNextFibonacciResponse
    {
        public int TaskId { get; set; }

        public long Value { get; set; }

        public long? PreviousValue { get; set; }
    }
}
