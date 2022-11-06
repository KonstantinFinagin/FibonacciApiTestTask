namespace Fibonacci.Client.Contracts
{
    public class NextFibonacciCalculationResultMessage
    {
        public string Value { get; set; } = null!;

        public string PreviousValue { get; set; } = null!;

        public DateTime GeneratedOn { get; set; }

        public int TaskId { get; set; }

        public bool CalculationStopped { get; set; }

        public string ExceptionMessage { get; set; } = null!;
    }
}