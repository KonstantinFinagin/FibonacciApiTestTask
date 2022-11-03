using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci.Common.Enums
{
    public enum EErrorCode
    {
        Network,
        SeeOther,
        NotFound,
        Unauthorized,
        BadRequest,
        ServerError,
        GatewayTimeout,
        Timeout,
        BadGateway,
        RequestTimeout,
        ServiceUnavailable,
        PreconditionFailed
    }
}
