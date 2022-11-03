using Fibonacci.Common.Mvc.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Fibonacci.Common.Mvc.Filters
{
    public class RpcCallFilter
    {
        public void Configure(IApplicationBuilder builder)
        {
            builder.UseMiddleware<RpcCallMiddleware>();
        }
    }
}
