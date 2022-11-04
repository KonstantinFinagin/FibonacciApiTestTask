namespace Fibonacci.Client.Contracts
{
    public class NextFibonacciCalculatedResultMessage
    {
        public long Result { get; set; }

        public long? Previous { get; set; }

        public DateTime GeneratedOn { get; set; }
        public int SessionId { get; set; }
        public int TaskId { get; set; }
    }
}