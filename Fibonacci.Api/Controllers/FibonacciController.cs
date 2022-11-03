using Fibonacci.Api.Bll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fibonacci.Api.Contracts;

namespace Fibonacci.Api.Controllers
{   
    /// <summary>
    ///     Basic Fibonacci calculation endpoint    
    /// </summary>
    [Route("api/fibonacci")]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciService _service;

        public FibonacciController(IFibonacciService service)
        {
            _service = service;
        }

        /// <summary>
        ///     Command to calculate next Fibonacci number and notify via rabbitMQ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CalculateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContracts([FromQuery] CalculateNextFibonacciRequest request)
        {
            var result = await _service.GetNextFibonacciNumber(request);
            return Ok(result);
        }

    }
}
