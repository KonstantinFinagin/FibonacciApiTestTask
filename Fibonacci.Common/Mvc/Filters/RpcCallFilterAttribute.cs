using System;
using Microsoft.AspNetCore.Mvc;

namespace Fibonacci.Common.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RpcCallFilterAttribute : MiddlewareFilterAttribute
    {
        public RpcCallFilterAttribute() : base(typeof(RpcCallFilter))
        {
        }
    }
}
