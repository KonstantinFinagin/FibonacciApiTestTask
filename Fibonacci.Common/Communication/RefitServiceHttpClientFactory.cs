using System;
using System.Net.Http;
using Fibonacci.Common.Authorization;
using Refit;

namespace Fibonacci.Common.Communication
{
    public class RefitServiceHttpClientFactory : IServiceHttpClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceBaseUrlProvider _baseUrlProvider;
        private readonly IAuthorizationService _authorizationService;
        private readonly CommunicationOptions _options;

        public RefitServiceHttpClientFactory(
            IHttpClientFactory httpClientFactory,
            IServiceBaseUrlProvider baseUrlProvider,
            IAuthorizationService authorizationService,
            CommunicationOptions options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrlProvider = baseUrlProvider;
            _authorizationService = authorizationService;
            _options = options;
        }

        public T CreateClient<T>(EServiceType serviceType)
        {
            var client = _httpClientFactory.CreateClient(serviceType.ToString());

            var uri = _baseUrlProvider.GetBaseUrl(serviceType);
            if (uri == null)
            {
                return default;
            }

            client.BaseAddress = new Uri(uri);

            SetAdditionalHeaders(client);

            return RestService.For<T>(client);
        }

        private void SetAdditionalHeaders(HttpClient client)
        {
            var baseUrlHeaders = _baseUrlProvider.GetBaseUrlHeaders();
            foreach (var header in baseUrlHeaders)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            var authorizationHeaders = _authorizationService.GetAuthorizationHeaders();
            foreach (var header in authorizationHeaders)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            client.DefaultRequestHeaders.Add(Constants.RpcSecretTokenHeader, _options.SecretToken);
        }
    }
}
