using EasyNetQ;
using EasyNetQ.Topology;
using Fibonacci.Client.Bll.Services;
using Fibonacci.Client.Listener;
using Fibonacci.Common.Constants;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Fibonacci.Client.Tasks
{
    public class ListenerService : IListenerService
    {
        private readonly IMessageService _messageService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IAdvancedBus _bus;

        public ListenerService(IMessageService messageService, IConfiguration configuration, ILogger logger, IAdvancedBus bus)
        {
            _messageService = messageService;
            _configuration = configuration;
            _logger = logger;
            _bus = bus;
        }

        public async Task StartAsync()
        {
            try
            {
                _logger.Information("Initialized Listener Service");

                var exchange = await _bus.ExchangeDeclareAsync(ExchangeNameConstants.CrmExchange, ExchangeType.Fanout);

                foreach (var queueName in _messageService.QueueNames)
                {
                    var queue = await _bus.QueueDeclareAsync(queueName);
                    await _bus.BindAsync(exchange, queue, string.Empty);
                    _bus.Consume(queue, (bytes, properties, receivedInfo) => _messageService.ProcessMessageAsync(bytes, properties, receivedInfo));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error starting service");
                throw;
            }
        }

        public void Stop()
        {
            // That's bad, but we're just cleaning up, proper shutdown cases should be considered

            _bus.QueuePurgeAsync(QueueNameConstants.FibonacciUpdatesQueue).GetAwaiter().GetResult();
            _logger.Information("Listener Service has been stopped");
        }
    }
}
