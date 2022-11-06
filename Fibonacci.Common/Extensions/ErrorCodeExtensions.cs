using Fibonacci.Common.Enums;
using System.Net;

namespace Fibonacci.Common.Extensions
{
    public static class ErrorCodeExtensions
    {
        public static HttpStatusCode TranslateErrorCodeToStatusCode(this EErrorCode errorCode)
        {
            switch (errorCode)
            {
                case EErrorCode.Unauthorized:
                    return HttpStatusCode.Unauthorized;
                case EErrorCode.NotFound:
                    return HttpStatusCode.NotFound;
                case EErrorCode.BadRequest:
                    return HttpStatusCode.BadRequest;
                case EErrorCode.SeeOther:
                    return HttpStatusCode.SeeOther;
                case EErrorCode.ServerError:
                    return HttpStatusCode.InternalServerError;
                case EErrorCode.RequestTimeout:
                    return HttpStatusCode.RequestTimeout;
                case EErrorCode.BadGateway:
                    return HttpStatusCode.BadGateway;
                case EErrorCode.ServiceUnavailable:
                    return HttpStatusCode.ServiceUnavailable;
                case EErrorCode.GatewayTimeout:
                    return HttpStatusCode.GatewayTimeout;
                case EErrorCode.PreconditionFailed:
                    return HttpStatusCode.PreconditionFailed;
            }

            return HttpStatusCode.InternalServerError;
        }

        public static EErrorCode TranslateStatusCodeToErrorCode(this HttpStatusCode httpStatusCode)
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode.SeeOther:
                    return EErrorCode.SeeOther;

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Conflict:
                case HttpStatusCode.ExpectationFailed:
                case HttpStatusCode.NotAcceptable:
                    return EErrorCode.BadRequest;

                case HttpStatusCode.Forbidden:
                case HttpStatusCode.Unauthorized:
                    return EErrorCode.Unauthorized;

                case HttpStatusCode.NotFound:
                    return EErrorCode.NotFound;

                case HttpStatusCode.GatewayTimeout:
                    return EErrorCode.GatewayTimeout;

                case HttpStatusCode.RequestTimeout:
                    return EErrorCode.RequestTimeout;

                case HttpStatusCode.BadGateway:
                    return EErrorCode.BadGateway;

                case HttpStatusCode.ServiceUnavailable:
                    return EErrorCode.ServiceUnavailable;

                case HttpStatusCode.PreconditionFailed:
                    return EErrorCode.PreconditionFailed;
            }

            return EErrorCode.ServerError;
        }
    }
}
