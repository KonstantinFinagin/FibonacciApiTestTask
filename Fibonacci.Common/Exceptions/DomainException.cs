using Fibonacci.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci.Common.Exceptions
{
    public class DomainException : Exception
    {
        private static readonly EErrorCode DefaultErrorCode = EErrorCode.ServerError;

        public DomainException()
        {
            ErrorCode = DefaultErrorCode;
        }

        public DomainException(string message)
            : base(message)
        {
            ErrorCode = DefaultErrorCode;
        }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = DefaultErrorCode;
        }

        public DomainException(EErrorCode errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public DomainException(EErrorCode errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        public DomainException(EErrorCode errorCode)
            : base()
        {
            ErrorCode = errorCode;
        }

        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorCode = DefaultErrorCode;
        }

        public EErrorCode ErrorCode { get; protected set; }

        public static DomainException BadRequest(string message)
        {
            return GetExceptionOfType(EErrorCode.BadRequest, message);
        }

        public static DomainException NotFound(string message)
        {
            return GetExceptionOfType(EErrorCode.NotFound, message);
        }

        public static DomainException ServerError(string message, Exception innerException = null)
        {
            return GetExceptionOfType(EErrorCode.ServerError, message, innerException);
        }

        public static DomainException Unauthorized(string message)
        {
            return GetExceptionOfType(EErrorCode.Unauthorized, message);
        }

        public override string ToString()
        {
            return $"Error code: {ErrorCode.ToString()}, {base.ToString()}";
        }

        private static DomainException GetExceptionOfType(EErrorCode errorCode, string message, Exception innerException = null)
        {
            var result = new DomainException(message, innerException)
            {
                ErrorCode = errorCode,
            };

            return result;
        }
    }
}
