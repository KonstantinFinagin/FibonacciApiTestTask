using System.Numerics;
using Fibonacci.Api.Bll.Notification;
using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Api.Contracts.Responses;
using Fibonacci.Calculator;
using Fibonacci.Calculator.Services;
using Fibonacci.Common.Exceptions;
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

            return response;
        }

        /// <summary>
        ///     This is called via Refit as RPC and pushes the result onto message bus queue
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CalculateCommandAcceptedResponse> CalculateNextFibonacciRpc(CalculateNextFibonacciRequest request)
        {
            var response = new CalculateCommandAcceptedResponse()
            {
                TaskId = request.TaskId
            };

            try
            {
                var nextFibonacci = _calculator.CalculateNextFibonacci(request.Value, request.PreviousValue);
                var messageResponse = new CalculateNextFibonacciResponse()
                {
                    PreviousValue = (request.Value == "1" || request.Value == "0") ? request.Value : null!,
                    Value = nextFibonacci,
                    TaskId = request.TaskId,
                };

                await _notificationService.NotifyNextFibonacciCalculated(messageResponse);
                response.Accepted = true;
            }
            catch (DomainException ex)
            {
                await _notificationService.NotifyCalculationEnded(request.TaskId, ex);
                response.Accepted = false;
            }
            
            return response;
        }
    }
}
