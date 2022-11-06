namespace Fibonacci.Client.Contracts
{
    public class NextFibonacciCalculatedResultMessage
    {
        public string Value { get; set; } = null!;

        public string PreviousValue { get; set; } = null!;

        public DateTime GeneratedOn { get; set; }

        public int TaskId { get; set; }
    }
}