using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fibonacci.Common.Authorization
{
    public interface IAuthorizationService
    {
        Task<Guid> GetUserId();

        Dictionary<string, string> GetAuthorizationHeaders();
    }
}
