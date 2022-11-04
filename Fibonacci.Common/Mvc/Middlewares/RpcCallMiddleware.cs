using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Fibonacci.Common.Mvc.Middlewares
{
    public class RpcCallMiddleware
    {
        private readonly RequestDelegate _next;

        public RpcCallMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            return;
        }
    }
}
