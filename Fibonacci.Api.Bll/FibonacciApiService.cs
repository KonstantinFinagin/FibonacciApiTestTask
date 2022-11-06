using System.Numerics;
using Fibonacci.Api.Bll.Notification;
using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Api.Contracts.Responses;
using Fibonacci.Calculator;
using Fibonacci.Calculator.Services;
using Fibonacci.Common.Validation;
using FluentValidation;
using Serilog;

namespace Fibonacci.Api.Bll
{
    public class FibonacciService : IFibonacciService
    {
        private readonly IValidatorsFactory _validatorsFactory;
        private readonly INotificationService _notificationService;
        private readonly IFibonacciCalculatorService _calculator;

        public FibonacciService(
            IValidatorsFactory validatorsFactory,
            INotificationService notificationService, 
            IFibonacciCalculatorService calculator)
        {
            _validatorsFactory = validatorsFactory;
            _notificationService = notificationService;
            _calculator = calculator;
        }

        /// <summary>
        ///     This is a regular API call to test via swagger
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CalculateNextFibonacciResponse> CalculateNextFibonacciNumber(CalculateNextFibonacciRequest request)
        {
            await _validatorsFactory.For<CalculateNextFibonacciRequest>().ValidateAndThrowAsync(request);

            var nextFibonacci = _calculator.CalculateNextFibonacci(request.Value, request.PreviousValue);

            var response = new CalculateNextFibonacciResponse()
            {
                PreviousValue = (request.Value == "1" )? request.Value : null!,
                Value = nextFibonacci,
                TaskId = request.TaskId,
            };

            // notify via rabbit
            await _notificationService.NotifyNextFibonacciCalculated(response);

            return response;
        }

        /// <summary>
        ///     This is called via Refit as RPC and pushes the result onto message bus queue
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CalculateCommandAcceptedResponse> CalculateNextFibonacciRpc(CalculateNextFibonacciRequest request)
        {
            var nextFibonacci = _calculator.CalculateNextFibonacci(request.Value, request.PreviousValue);

            var response = new CalculateCommandAcceptedResponse()
            {
                Accepted = true,
                TaskId = request.TaskId
            };

            var messageResponse = new CalculateNextFibonacciResponse()
            {
                PreviousValue = (request.Value == "1") ? request.Value : null!,
                Value = nextFibonacci,
                TaskId = request.TaskId,
            };

            await _notificationService.NotifyNextFibonacciCalculated(messageResponse);
            return response;
        }
    }
}
