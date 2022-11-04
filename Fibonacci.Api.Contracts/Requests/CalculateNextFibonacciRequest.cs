namespace Fibonacci.Api.Contracts.Requests
{
    public class CalculateNextFibonacciRequest
    {
        public int SessionId { get; set; }

        public int TaskId { get; set; }

        public long Value { get; set; }

        public long? PreviousValue { get; set; }
    }
}
