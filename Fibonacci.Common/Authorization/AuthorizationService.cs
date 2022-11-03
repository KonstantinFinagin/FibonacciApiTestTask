using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Fibonacci.Common.Communication;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Fibonacci.Common.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IServiceBaseUrlProvider _serviceBaseUrlProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger = Log.ForContext<AuthorizationService>();

        public AuthorizationService(IServiceBaseUrlProvider serviceBaseUrlProvider, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _serviceBaseUrlProvider = serviceBaseUrlProvider;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }
        
        public Task<Guid> GetUserId()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.Request.Headers[Constants.UserIdHeader][0];
                return Task.FromResult(Guid.Parse(userId));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error while getting user id from header");
                return Task.FromResult(Guid.Empty);
            }
        }

        public Dictionary<string, string> GetAuthorizationHeaders()
        {
            var result = new Dictionary<string, string>
            {
                { Constants.UserIdHeader, GetUserId().Result.ToString() }
            };

            return result;
        }

        private class ObjectAccessRuleResponse
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("objectId")]
            public int ObjectId { get; set; }

            [JsonPropertyName("objectTypeId")]
            public int ObjectTypeId { get; set; }

            [JsonPropertyName("roleId")]
            public int RoleId { get; set; }
        }

        private class UserGetResponse
        {
            [JsonPropertyName("objectAccessRules")]
            public List<ObjectAccessRuleResponse> ObjectAccessRules { get; set; }
        }
    }
}
