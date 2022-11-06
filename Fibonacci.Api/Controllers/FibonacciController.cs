using Fibonacci.Api.Bll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Api.Contracts.Responses;
using Fibonacci.Common.Mvc.Filters;

namespace Fibonacci.Api.Controllers
{   
    /// <summary>
    ///     Basic Fibonacci calculation endpoint    
    /// </summary>
    [Route("api/fibonacci")]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciApiService _apiService;

        public FibonacciController(IFibonacciApiService apiService)
        {
            _apiService = apiService;
        }

        /// <summary>
        ///     Command to calculate next Fibonacci number and notify via rabbitMQ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CalculateNextFibonacciResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CalculateNextFibonacci([FromQuery] CalculateNextFibonacciRequest request)
        {
            var result = await _apiService.CalculateNextFibonacciNumber(request);
            return Ok(result);
        }

        /// <summary>
        ///     Command to calculate next Fibonacci number and notify via rabbitMQ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("rpc")]
        [RpcCallFilter]
        [ProducesResponseType(typeof(CalculateCommandAcceptedResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CalculateNextFibonacciRpc([FromQuery] CalculateNextFibonacciRequest request)
        {
            var result = await _apiService.CalculateNextFibonacciRpc(request);
            return Ok(result);
        }
    }
}
