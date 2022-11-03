using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Fibonacci.Common.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly ILogger Logger = Log.ForContext<ExceptionMiddleware>();

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            // TODO catch custom exceptions if needed

            catch (Exception exception)
            {
                await HandleServerError(context, exception);
            }
        }

        private async Task HandleServerError(HttpContext context, Exception exception)
        {
            var errorId = Guid.NewGuid().ToString();

            var correlationId = System.Diagnostics.Activity.Current?.RootId;

            Logger.ForContext("FibonacciCorellationId", correlationId)
                .Error(exception, "Exception {ErrorId}: {ErrorMessage}", errorId, exception.Message);

            SetStatusCode(context, HttpStatusCode.InternalServerError);

            var errorData = new TechnicalErrorExceptionResponse(errorId, null, correlationId);
            await WriteToResponseAsync(context, errorData);
        }

        private static async Task WriteToResponseAsync(HttpContext context, object data)
        {
            var executor = context.RequestServices.GetRequiredService<IActionResultExecutor<ObjectResult>>();
            var ct = new ActionContext(context, context.GetRouteData() ?? new RouteData(), new ActionDescriptor());
            await executor.ExecuteAsync(ct, new ObjectResult(data));
        }

        private static void SetStatusCode(HttpContext context, HttpStatusCode statusCode)
        {
            if (context.Response.HasStarted)
            {
                return;
            }

            // the below code might not be needed because previous check should return true
            try
            {
                context.Response.StatusCode = (int)statusCode;
            }
            catch
            {
                // it might fail because it was already sent, so ignore to avoid exception
                // example: System.InvalidOperationException: StatusCode cannot be set because the response has already started.
            }
        }

    }
}
