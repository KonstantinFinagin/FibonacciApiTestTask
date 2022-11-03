using Fibonacci.Common.Enums;

namespace Fibonacci.Common.Exceptions;

public class ExceptionResponse
{
    public ExceptionResponse(string error, EErrorCode? errorCode)
    {
        Error = error;
        ErrorCode = (int?)errorCode;
    }

    public virtual string Error { get; set; }

    public virtual int? ErrorCode { get; set; }
}