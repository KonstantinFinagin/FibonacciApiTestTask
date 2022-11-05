using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Calculator;
using Fibonacci.Client.Bll.Services;
using Fibonacci.Client.Contracts;
using Serilog;

namespace Fibonacci.Client.Bll.Processors
{
    public class MessageProcessor : IMessageProcessor<NextFibonacciCalculatedResultMessage>
    {
        private readonly ILogger _logger;
        private readonly IApiService _apiService;

        public MessageProcessor(ILogger logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task ProcessMessageAsync(NextFibonacciCalculatedResultMessage message)
        {
            // when a message is received from the queue - calculate the next number and do the roundtrip 

            _logger.Debug($"{message.GeneratedOn} <-- TaskId:{message.TaskId}, Value:{message.Value}, PreviousValue:{message.PreviousValue}");

            var newFibonacci = FibonacciCalculator.NextFibonacci(message.Value, message.PreviousValue);

            var request = new CalculateNextFibonacciRequest()
            {
                PreviousValue = message.Value,
                TaskId = message.TaskId,
                Value = newFibonacci,
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
