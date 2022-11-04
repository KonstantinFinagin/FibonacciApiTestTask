namespace Fibonacci.Api.Contracts.Responses
{
    public class CalculateNextFibonacciResponse
    {
        public int SessionId { get; set; }

        public int TaskId { get; set; }

        public long Result { get; set; }

        public long? Previous { get; set; }
    }
}
