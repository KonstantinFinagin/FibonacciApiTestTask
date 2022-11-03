using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Fibonacci.Common.Mvc
{
    public class ApiExplorerDescriptionProvider : IApiDescriptionProvider
    {
        public int Order => 0;

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
        }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
            var items = (from apiDescription in context.Results
                    from parameterDescription in apiDescription.ParameterDescriptions
                    let routeInfo = parameterDescription.RouteInfo
                    where routeInfo != null
                    from matchConstraint in routeInfo.Constraints.OfType<MatchRouteConstraint>()
                    select new { apiDescription, parameterDescription, matchConstraint.Value })
                .ToList();

            items.ForEach(it => ProcessMatchConstraint(it.apiDescription, it.parameterDescription, it.Value));
        }

        private void ProcessMatchConstraint(
            ApiDescription apiDescription,
            ApiParameterDescription parameterDescription,
            string matchValue)
        {
            apiDescription.RelativePath =
                apiDescription.RelativePath
                    .Replace($"{{{parameterDescription.Name}}}", matchValue);

            apiDescription.ParameterDescriptions.Remove(parameterDescription);
        }
    }
}
