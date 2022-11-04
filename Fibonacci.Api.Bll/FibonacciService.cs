using Fibonacci.Api.Bll.Notification;
using Fibonacci.Api.Contracts;
using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Api.Contracts.Responses;
using Fibonacci.Calculator;
using Fibonacci.Common.Validation;
using FluentValidation;
using Serilog;

namespace Fibonacci.Api.Bll
{
    public class FibonacciService : IFibonacciService
    {
        private readonly IValidatorsFactory _validatorsFactory;
        private readonly INotificationService _notificationService;
        private readonly ILogger _logger;

        public FibonacciService(IValidatorsFactory validatorsFactory, INotificationService notificationService, ILogger logger)
        {
            _validatorsFactory = validatorsFactory;
            _notificationService = notificationService;
            _logger = logger;
        }

        /// <summary>
        ///     This is a regular API call to test via swagger
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CalculateNextFibonacciResponse> CalculateNextFibonacciNumber(CalculateNextFibonacciRequest request)
        {
            await _validatorsFactory.For<CalculateNextFibonacciRequest>().ValidateAndThrowAsync(request);

            var nextFibonacci = FibonacciCalculator.NextFibonacci(request.Value, request.PreviousValue);

            var response = new CalculateNextFibonacciResponse()
            {
                Previous = request.Value,
                Result = nextFibonacci,
                TaskId = request.TaskId,
                SessionId = request.SessionId
            };

            return response;
        }

        /// <summary>
        ///     This is called via Refit as RPC and pushes the result onto message bus queue
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<CalculateCommandAcceptedResponse> CalculateNextFibonacciNumberRpc(CalculateNextFibonacciRequest request)
        {
            var nextFibonacci = FibonacciCalculator.NextFibonacci(request.Value, request.PreviousValue);

            var response = new CalculateCommandAcceptedResponse()
            {
                Accepted = true,
                SessionId = request.SessionId,
                TaskId = request.TaskId
            };

            return Task.FromResult(response);
        }
    }
}
