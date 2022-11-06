using System.Text;
using EasyNetQ;
using Fibonacci.Client.Bll.Processors;
using Fibonacci.Client.Contracts;
using Fibonacci.Common.Constants;
using Serilog;

namespace Fibonacci.Client.Bll.Services
{
    public class MessageService : IMessageService
    {
        private readonly ILogger _logger;

        private readonly IMessageProcessor<NextFibonacciCalculationResultMessage> _fibonacciProcessor;

        public MessageService(IMessageProcessor<NextFibonacciCalculationResultMessage> fibonacciProcessor) 
        {
            _fibonacciProcessor = fibonacciProcessor;
            _logger = Log.ForContext<MessageService>();
            _logger.Debug("Message Service initialized");

            QueueNames = new List<string>()
            {
                QueueNameConstants.FibonacciUpdatesQueue
            };
        }

        public async Task ProcessMessageAsync(ReadOnlyMemory<byte> bytes, MessageProperties messageProperties,
            MessageReceivedInfo messageReceivedInfo)
        {
            var rawMessage = Encoding.UTF8.GetString(bytes.ToArray());

            var message = System.Text.Json.JsonSerializer.Deserialize<NextFibonacciCalculationResultMessage>(rawMessage);
            if (message != null)
            {
                try
                {
                    await _fibonacciProcessor.ProcessMessageAsync(message);
                }
                catch (Exception e)
                {
                    _logger.Error(e, "ProcessMessageAsync error");
                }
            }
        }

        public List<string> QueueNames { get; }
    }
}
