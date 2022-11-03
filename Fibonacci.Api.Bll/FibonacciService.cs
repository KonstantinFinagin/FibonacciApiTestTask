using Fibonacci.Api.Contracts;
using Fibonacci.Calculator;
using Fibonacci.Common.Validation;
using FluentValidation;
using Serilog;

namespace Fibonacci.Api.Bll
{
    public class FibonacciService : IFibonacciService
    {
        private readonly IValidatorsFactory _validatorsFactory;
        private readonly ILogger _logger;

        public FibonacciService(IValidatorsFactory validatorsFactory, ILogger logger)
        {
            _validatorsFactory = validatorsFactory;
            _logger = logger;
        }

        public async Task<CalculateResponse> GetNextFibonacciNumber(CalculateNextFibonacciRequest request)
        {
            await _validatorsFactory.For<CalculateNextFibonacciRequest>().ValidateAndThrowAsync(request);

            var nextFibonacci = FibonacciCalculator.NextFibonacci(request.Value, request.PreviousValue);

            var response = new CalculateResponse()
            {
                Previous = request.Value,
                Result = nextFibonacci,
                TaskId = request.TaskId
            };

            // todo rabbitmq notification

            return response;
        }
    }
}
