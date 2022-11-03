using Fibonacci.Common.Enums;

namespace Fibonacci.Common.Exceptions;

public class TechnicalErrorExceptionResponse : ExceptionResponse
{
    public TechnicalErrorExceptionResponse(string id, EErrorCode? errorCode, string correlationId)
        : base("A technical error occured, please try again later.", errorCode)
    {
        Id = id;
        CorrelationId = correlationId;
    }

    public string Id { get; set; }

    public string CorrelationId { get; set; }
}