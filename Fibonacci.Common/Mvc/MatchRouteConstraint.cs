using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Fibonacci.Common.Mvc
{
    public class MatchRouteConstraint : IRouteConstraint
    {
        public const string Label = "match";

        public MatchRouteConstraint(string checkValue)
        {
            Value = checkValue;
        }

        public string Value { get; }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out var value) && value != null)
            {
                return string.Equals(value.ToString(), Value, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}