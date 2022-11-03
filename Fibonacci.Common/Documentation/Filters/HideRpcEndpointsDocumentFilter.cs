using System;
using System.Linq;
using Fibonacci.Common.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fibonacci.Common.Documentation.Filters
{
    /// <summary>
    /// Hides RPC endpoints in a swagger document. Schemes for the endpoints will still be shown.
    /// </summary>
    public class HideRpcEndpointsDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var apiDescription in context.ApiDescriptions)
            {
                if (!apiDescription.CustomAttributes().OfType<RpcCallFilterAttribute>().Any())
                {
                    continue;
                }

                var path = swaggerDoc.Paths.First(p => p.Key.Equals($"/{apiDescription.RelativePath}"));
                if (!Enum.TryParse<OperationType>(apiDescription.HttpMethod, true, out var operationType))
                {
                    continue;
                }

                path.Value.Operations.Remove(operationType);
            }
        }
    }
}
