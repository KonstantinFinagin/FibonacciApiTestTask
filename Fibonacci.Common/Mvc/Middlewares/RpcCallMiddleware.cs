using System;
using System.Threading.Tasks;
using Fibonacci.Common.Communication;
using Microsoft.AspNetCore.Http;

namespace Fibonacci.Common.Mvc.Middlewares
{
    public class RpcCallMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CommunicationOptions _options;

        public RpcCallMiddleware(RequestDelegate next, CommunicationOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(Constants.RpcSecretTokenHeader, out var rpcToken))
            {
                if (_options.SecretToken.Equals(rpcToken, StringComparison.Ordinal))
                {
                    await _next(context);
                    return;
                }
            }

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
    }
}
