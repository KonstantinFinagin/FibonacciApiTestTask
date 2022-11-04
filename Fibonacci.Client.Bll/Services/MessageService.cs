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

        private readonly IMessageProcessor<NextFibonacciCalculatedResultMessage> _contactMessageProcessor;

        public MessageService(IMessageProcessor<NextFibonacciCalculatedResultMessage> contactMessageProcessor) // TODO introduce factory with more processors
        {
            _contactMessageProcessor = contactMessageProcessor;
            _logger = Log.ForContext<MessageService>();
            _logger.Debug("Message Service initialized");

            QueueNames = new List<string>()
            {
                QueueNameConstants.FibonacciUpdatesQueue
            };
        }

        public async Task ProcessMessageAsync(byte[] bytes, MessageProperties messageProperties, MessageReceivedInfo messageReceivedInfo)
        {
            var rawMessage = Encoding.UTF8.GetString(bytes);

            var contactMessage = System.Text.Json.JsonSerializer.Deserialize<NextFibonacciCalculatedResultMessage>(rawMessage);
            if (contactMessage != null)
            {
                contactMessage.RawMessage = rawMessage;

                try
                {
                    await _contactMessageProcessor.ProcessMessageAsync(contactMessage);
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
