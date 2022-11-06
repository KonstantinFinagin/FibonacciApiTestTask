namespace Fibonacci.Client.Contracts;

public class CalculationExceptionMessage
{
    public string Message { get; set; } = null!;

    public int TaskId { get; set; }
}