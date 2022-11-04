namespace Fibonacci.Api.Contracts.Responses
{
    public class CalculateCommandAcceptedResponse
    {
        public int SessionId { get; set; }

        public int TaskId { get; set; }

        public bool Accepted { get; set; }
    }
}
