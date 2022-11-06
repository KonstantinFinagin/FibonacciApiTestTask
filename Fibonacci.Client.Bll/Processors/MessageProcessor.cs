using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Calculator;
using Fibonacci.Calculator.Services;
using Fibonacci.Client.Bll.Services;
using Fibonacci.Client.Contracts;
using Fibonacci.Common.Exceptions;
using Serilog;

namespace Fibonacci.Client.Bll.Processors
{
    public class MessageProcessor : IMessageProcessor<NextFibonacciCalculatedResultMessage>
    {
        private readonly ILogger _logger;
        private readonly IApiService _apiService;
        private readonly IFibonacciCalculatorService _fibonacciCalculatorService;

        public MessageProcessor(ILogger logger, IApiService apiService, IFibonacciCalculatorService fibonacciCalculatorService)
        {
            _logger = logger;
            _apiService = apiService;
            _fibonacciCalculatorService = fibonacciCalculatorService;
        }

        public async Task ProcessMessageAsync(NextFibonacciCalculatedResultMessage message)
        {
            // when a message is received from the queue - calculate the next number and do the roundtrip 

            _logger.Debug($"{message.GeneratedOn} <-- TaskId:{message.TaskId}, Value:{message.Value}, PreviousValue:{message.PreviousValue}");

            string newFibonacci;
            try
            {
                newFibonacci = _fibonacciCalculatorService.CalculateNextFibonacci(message.Value, message.PreviousValue);
            }
            catch (DomainException ex)
            {
                _logger.Debug($"Stopping task {message.TaskId}: {ex.Message}");
                return;
            }

            var request = new CalculateNextFibonacciRequest()
            {
                PreviousValue = (message.Value == "1" || message.Value == "0") ? message.Value : null!,
                Value = newFibonacci,
                TaskId = message.TaskId,
            };
            
            _logger.Debug($"{DateTime.UtcNow} --> TaskId:{request.TaskId}, Value:{request.Value}, PreviousValue:{request.PreviousValue}");

            try
            {
                var result = await _apiService.SendCalculationCommandAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Debug($"API SEND ERROR", ex);
            }
        }
    }
}
