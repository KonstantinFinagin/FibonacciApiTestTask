using System.Collections.Generic;

namespace Fibonacci.Common.Communication
{
    public interface IServiceBaseUrlProvider
    {
        string GetBaseUrl(EServiceType serviceType);

        Dictionary<string, string> GetBaseUrlHeaders();
    }
}
