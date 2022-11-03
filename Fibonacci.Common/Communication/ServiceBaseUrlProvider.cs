using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Fibonacci.Common.Communication
{
    public class ServiceBaseUrlProvider : IServiceBaseUrlProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger = Log.ForContext<ServiceBaseUrlProvider>();
        private readonly Dictionary<EServiceType, string> _mappings = new Dictionary<EServiceType, string>
        {
            { EServiceType.Fibonacci, "fibonacci" },
        };

        public ServiceBaseUrlProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Dictionary<string, string> GetBaseUrlHeaders()
        {
            var headers = new Dictionary<string, string>();

            foreach (var headerName in _mappings.Values)
            {
                if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey(headerName))
                {
                    headers.Add(headerName, _httpContextAccessor.HttpContext.Request.Headers[headerName][0]);
                }
            }

            return headers;
        }

        public string GetBaseUrl(EServiceType serviceType)
        {
            try
            {
                var headerName = _mappings[serviceType];
                var value = _httpContextAccessor.HttpContext.Request.Headers[headerName][0];
                return value;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Couldn't get base url for {serviceType}");
                return null;
            }
        }
    }
}
