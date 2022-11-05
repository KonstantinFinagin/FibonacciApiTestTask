namespace Fibonacci.Client.Contracts
{
    public class NextFibonacciCalculatedResultMessage
    {
        public long Value { get; set; }

        public long? PreviousValue { get; set; }

        public DateTime GeneratedOn { get; set; }

        public int TaskId { get; set; }
    }
}